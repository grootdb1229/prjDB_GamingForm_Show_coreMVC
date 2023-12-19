using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using prjDB_GamingForm_Show.Models.Entities;
using System.ComponentModel.DataAnnotations;
using prjDB_GamingForm_Show.Models;
using static prjDB_GamingForm_Show.Hubs.ChatHub;
using Org.BouncyCastle.Tls;
using System;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using prjDB_GamingForm_Show.Models.Member;

namespace prjDB_GamingForm_Show.Hubs
{
    public class MemberChatHub : Hub
    {
        private readonly DbGamingFormTestContext _db;
        public MemberChatHub(DbGamingFormTestContext db)
        {
            _db = db;
        }

        public class MemberConnection
        {
            public string ConnectionId { get; set; }
            public int MemberId { get; set; }
            public string MemberName { get; set; }
            public bool IsOnline { get; set; }
        }

        private static List<MemberConnection> _ConnectedMember = new List<MemberConnection>();

        private List<string> GetMemberName()
        {
            try
            {
                return _ConnectedMember.Select(u => u.MemberName).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public override async Task OnConnectedAsync()
        {
            var memberName = Context.GetHttpContext().Session.GetString(CDictionary.SK_UserName);
            var memberId = Context.GetHttpContext().Session.GetInt32(CDictionary.SK_UserID);
            var connectionId = Context.ConnectionId;

            var memberConnection = new MemberConnection { ConnectionId = connectionId, MemberName = memberName, MemberId = (int)memberId, IsOnline = true };
            _ConnectedMember.Add(memberConnection);

            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(GetMemberName());
            await Clients.All.SendAsync("UpdList", jsonString);

            // 更新個人 ID
            await Clients.Client(Context.ConnectionId).SendAsync("UpdSelfID", memberName);

            // 更新線上狀態
            await UpdateMemberOnlineStatus(memberName, true);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var disconnectedUser = _ConnectedMember.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
            if (disconnectedUser != null)
            {
                _ConnectedMember.Remove(disconnectedUser);
            }

            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(GetMemberName());
            await Clients.All.SendAsync("UpdList", jsonString);

            // 更新線上狀態
            await UpdateMemberOnlineStatus(disconnectedUser?.MemberName, false);

            await base.OnDisconnectedAsync(ex);
        }                

        public async Task SendMessageToAll(string senderName, string message)
        {
            var senderId = _db.Members.FirstOrDefault(m => m.Name == senderName).MemberId;

            await Clients.All.SendAsync("UpdContent", message, senderName);

            if(senderId != null)
            {
                PublicChat chat = new PublicChat();
                chat.SenderId = senderId;
                chat.ChatContent = message;
                chat.Modifiedate = DateTime.UtcNow.ToLocalTime().ToString("yyyy/MM/dd HH:mm");

                _db.PublicChats.Add(chat);
                await _db.SaveChangesAsync();
            }
        }

        public async Task SystemMessage(string which ,string message, string receiverConnectionId, string receiverName)
        {
            var systemName = "";
            var receiveMemberId = _db.Members.FirstOrDefault(m => m.Name == receiverName).MemberId;
            var sendTime = DateTime.UtcNow.ToLocalTime().ToString("yyyy/MM/dd HH:mm");
            int sendSystem = 0;

            if(which == "委託") {  
                sendSystem = 176;
                systemName = "委託系統通知";
            }
            else if(which == "商城") { 
                sendSystem = 179;
                systemName = "商城系統通知";
            }

            if (receiverConnectionId != null)
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiverUpdContent", message, systemName, 0, "", sendTime);
            }
            //else 
            //{
            //    CEmail email = new CEmail();
            //    List<string> Emails = new List<string>();
            //    //Emails.Add("alan90306@gmail.com");
            //    Emails.Add("kakuc0e0ig@gmail.com");
            //    //Emails.Add("iamau3vm0@gmail.com");
            //    email.Emails = Emails;
            //    string html = "<p>測試</p>";
            //    foreach (string Address in email.Emails)
            //    {
            //        var emailmessage = new MimeMessage();
            //        emailmessage.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
            //        emailmessage.To.Add(new MailboxAddress("會員", Address));
            //        emailmessage.Subject = "測試";
            //        emailmessage.Body = new TextPart("text")
            //        {
            //            Text = "測試"
            //        };

            //        using (var client = new SmtpClient())
            //        {
            //            client.Connect("smtp.gmail.com", 587, false);
            //            client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
            //            client.Send(emailmessage);
            //            client.Disconnect(true);
            //        }
            //    }
            //}

            if(receiveMemberId != null)
            {
                MemberChat memberChat = new MemberChat()
                {
                    SenderMember = sendSystem,
                    ReceiveMember = receiveMemberId,
                    ChatContent = message,
                    ModefiedDate = DateTime.UtcNow.ToLocalTime().ToString("yyyy/MM/dd HH:mm"),
                    IsCheck = false
                };
                _db.MemberChats.Add(memberChat);
                await _db.SaveChangesAsync();
            }
        }
        public async Task SendMessage(string senderName, string message, string receiverConnectionId, string receiverName)
        {
            var senderMemberName = _ConnectedMember.FirstOrDefault(u => u.MemberName == senderName)?.MemberName;
            var senderConnectionId = _ConnectedMember.FirstOrDefault(u => u.MemberName == senderName)?.ConnectionId;
            var senderMemberId = _db.Members.FirstOrDefault(m => m.Name == senderName).MemberId;
            var receiveMemberId = _db.Members.FirstOrDefault(m => m.Name == receiverName).MemberId;
            var senderimg = _db.Members.FirstOrDefault(a => a.Name == senderName).FImagePath;
            var sendTime = DateTime.UtcNow.ToLocalTime().ToString("yyyy/MM/dd HH:mm");
            
            // 接收人
            if (receiverConnectionId != null)
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiverUpdContent", message, senderName, senderMemberId, senderimg, sendTime);
            }

            // 發送人
            await Clients.Client(senderConnectionId).SendAsync("SenderUpdContent", message);

            if (senderMemberId != null && receiveMemberId != null)
            {
                MemberChat chat = new MemberChat();
                chat.SenderMember = senderMemberId;
                chat.ReceiveMember = receiveMemberId;
                chat.ChatContent = message;
                chat.ModefiedDate = DateTime.UtcNow.ToLocalTime().ToString("yyyy/MM/dd HH:mm");
                chat.IsCheck = false;

                _db.MemberChats.Add(chat);
                await _db.SaveChangesAsync();
            }
        }

        public string GetConnectionIdByUserName(string userName)
        {
            var user = _ConnectedMember.FirstOrDefault(u => u.MemberName == userName);
            return user?.ConnectionId;
        }

        public async Task UpdateMemberOnlineStatus(string memberName, bool isOnline)
        {
            // 更新在線狀態
            await Clients.All.SendAsync("UpdMemberOnlineStatus", memberName, isOnline);
        }

        public bool GetMemberOnlineStatus(string userName)
        {
            var admin = _ConnectedMember.FirstOrDefault(u => u.MemberName == userName);
            return admin != null && admin.IsOnline;
        }
    }
}
