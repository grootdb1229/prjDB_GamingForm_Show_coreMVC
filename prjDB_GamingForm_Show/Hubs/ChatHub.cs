//using Microsoft.AspNetCore.SignalR;

//namespace prjDB_GamingForm_Show.Hubs
//{
//    public class ChatHub : Hub
//    {
//        public async Task SendMessage(string user, string message)
//        {
//            await Clients.All.SendAsync("ReceiveMessage", user, message);
//        }
//    }
//}
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using prjDB_GamingForm_Show.Models.Entities;
using System.ComponentModel.DataAnnotations;
using prjDB_GamingForm_Show.Models;

namespace prjDB_GamingForm_Show.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DbGamingFormTestContext _db;
        public ChatHub(DbGamingFormTestContext db)
        {
            _db = db;
        }
        public class UserConnection
        {
            public string ConnectionId { get; set; }
            public string UserName { get; set; }
            public bool IsOnline { get; set; }
        }
        // 用戶連線 ID 列表        
        private static List<UserConnection> ConnectedUsers = new List<UserConnection>();
        private List<string> GetUserNames()
        {
            return ConnectedUsers.Select(u => u.UserName).ToList();
        }
        /// <summary>
        /// 連線事件
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext().Session.GetString(CDictionary.SK_管理者名稱);
            var connectionId = Context.ConnectionId;

            var userConnection = new UserConnection { ConnectionId = connectionId, UserName = userName, IsOnline = true };
            ConnectedUsers.Add(userConnection);

            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(GetUserNames());
            await Clients.All.SendAsync("UpdList", jsonString);

            // 更新個人 ID
            await Clients.Client(Context.ConnectionId).SendAsync("UpdSelfID", userName);

            // 更新管理者線上狀態
            await UpdateAdminOnlineStatus(userName, true);

            // 更新聊天內容
            await Clients.All.SendAsync("UpdContent", "新連線 ID: " + userName);

            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 離線事件
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var disconnectedUser = ConnectedUsers.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
            if (disconnectedUser != null)
            {
                ConnectedUsers.Remove(disconnectedUser);
            }

            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(GetUserNames());
            await Clients.All.SendAsync("UpdList", jsonString);

            // 更新管理者線上狀態
            await UpdateAdminOnlineStatus(disconnectedUser?.UserName, false);

            // 更新聊天內容
            await Clients.All.SendAsync("UpdContent", "已離線 ID: " + disconnectedUser?.UserName);

            await base.OnDisconnectedAsync(ex);
        }

        /// <summary>
        /// 傳遞訊息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task SendMessage(string selfID, string message, string sendToID, string sendToName)
        {
            var senderUserName = ConnectedUsers.FirstOrDefault(u => u.UserName == selfID)?.UserName;
            var selfSid = ConnectedUsers.FirstOrDefault(u => u.UserName == selfID)?.ConnectionId;
            if (string.IsNullOrEmpty(sendToID))
            {
                await Clients.All.SendAsync("UpdContent", senderUserName + " 說: " + message);
            }
            else
            {
                // 接收人
                await Clients.Client(sendToID).SendAsync("UpdContent", senderUserName + " 私訊向你說: " + message);

                // 發送人
                await Clients.Client(selfSid).SendAsync("UpdContent", "你向 " + ConnectedUsers.FirstOrDefault(u => u.ConnectionId == sendToID)?.UserName + " 私訊說: " + message);
            }

            var senderAdminId = _db.Admins.FirstOrDefault(a => a.Name == selfID).AdminId;
            var receiveAdminId = _db.Admins.FirstOrDefault(a => a.Name == sendToName).AdminId;
            if (senderAdminId != null && receiveAdminId != null)
            {
                Chat chat = new Chat();
                chat.SenderAdmin = senderAdminId;
                chat.ReceiveAdmin = receiveAdminId;
                chat.ChatContent = message;
                chat.ModefiedDate = DateTime.UtcNow.ToLocalTime().ToString("yyyy/MM/dd/HH/mm/ss");
                
                _db.Chats.Add(chat);
                await _db.SaveChangesAsync();
            }
        }
        public string GetConnectionIdByUserName(string userName)
        {
            var user = ConnectedUsers.FirstOrDefault(u => u.UserName == userName);
            return user?.ConnectionId;
        }
        public async Task UpdateAdminOnlineStatus(string adminName, bool isOnline)
        {
            // 更新管理者在線狀態
            await Clients.All.SendAsync("UpdAdminOnlineStatus", adminName, isOnline);
        }
        public bool GetAdminOnlineStatus(string adminName)
        {            
            var admin = ConnectedUsers.FirstOrDefault(u => u.UserName == adminName);
            return admin != null && admin.IsOnline;
        }
    }
}
