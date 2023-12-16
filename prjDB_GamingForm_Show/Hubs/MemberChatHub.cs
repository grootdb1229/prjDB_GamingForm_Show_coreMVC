﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using prjDB_GamingForm_Show.Models.Entities;
using System.ComponentModel.DataAnnotations;
using prjDB_GamingForm_Show.Models;
using static prjDB_GamingForm_Show.Hubs.ChatHub;

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

        public async Task SendMessage(string senderName, string message, string receiverConnectionId, string receiverName)
        {
            var senderMemberName = _ConnectedMember.FirstOrDefault(u => u.MemberName == senderName)?.MemberName;
            var senderConnectionId = _ConnectedMember.FirstOrDefault(u => u.MemberName == senderName)?.ConnectionId;
            var senderMemberId = _ConnectedMember.FirstOrDefault(m => m.MemberName == senderName).MemberId;
            var receiveMemberId = _ConnectedMember.FirstOrDefault(m => m.MemberName == receiverName).MemberId;
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
            await Clients.All.SendAsync("UpdAdminOnlineStatus", memberName, isOnline);
        }

        public bool GetMemberOnlineStatus(string userName)
        {
            var admin = _ConnectedMember.FirstOrDefault(u => u.MemberName == userName);
            return admin != null && admin.IsOnline;
        }
    }
}
