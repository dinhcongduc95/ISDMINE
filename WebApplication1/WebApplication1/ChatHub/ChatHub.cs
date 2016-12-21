using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using WebApplication5.ChatHub.Models;
using WebApplication1.Models;

namespace WebApplication5.ChatHub
{
    public class ChatHub:Hub
    {

        #region Data Members

        static List<UserDetail> ConnectedAdmins = new List<UserDetail>();
        static List<UserDetail> ConnectedCustomers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        #endregion

        #region Methods

        public void Connect(string userName, string role)
        {
            var id = Context.ConnectionId;

            if (ConnectedAdmins.Count(x => x.ConnectionId == id) == 0 && ConnectedCustomers.Count(x => x.ConnectionId == id) == 0)
            {
                if (role.Equals("Admin"))
                {
                    ConnectedAdmins.Add(new UserDetail { ConnectionId = id, UserName = userName });
                    // send to caller
                    Clients.Caller.onConnected(id, userName, ConnectedCustomers, CurrentMessage);

                    // send to all except caller client
                    Clients.AllExcept(id).onNewUserConnected(id, userName, role);
                }

                if (role.Equals("Customer"))
                {                    
                    ConnectedCustomers.Add(new UserDetail { ConnectionId = id, UserName = userName });
                    // send to caller
                    Clients.Caller.onConnected(id, userName, ConnectedAdmins, CurrentMessage);

                    // send to all except caller client
                    Clients.AllExcept(id).onNewUserConnected(id, userName, role);
                }
                    
                

            }

        }

        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            AddMessageinCache(userName, message);

            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.ConnectionId;
            var toUser = new UserDetail();
            var fromUser = new UserDetail();
            if (ConnectedAdmins.Any(m => m.ConnectionId == toUserId))
            {
                toUser = ConnectedAdmins.FirstOrDefault(x => x.ConnectionId == toUserId);
            }
            if (ConnectedCustomers.Any(m => m.ConnectionId == toUserId))
            {
                toUser = ConnectedCustomers.FirstOrDefault(x => x.ConnectionId == toUserId);
            }

            if (ConnectedAdmins.Any(m => m.ConnectionId == fromUserId))
            {
                fromUser = ConnectedAdmins.FirstOrDefault(x => x.ConnectionId == fromUserId);
            }
            if (ConnectedCustomers.Any(m => m.ConnectionId == fromUserId))
            {
                fromUser = ConnectedCustomers.FirstOrDefault(x => x.ConnectionId == fromUserId);
            }

            if (toUser != null && fromUser != null)
            {
                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }

        }

        public override System.Threading.Tasks.Task OnDisconnected(bool chatCaller)
        {

            var item = ConnectedCustomers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            var item2 = ConnectedAdmins.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedCustomers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }
            if (item2 != null)
            {
                ConnectedAdmins.Remove(item2);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item2.UserName);

            }

            return base.OnDisconnected(chatCaller);
        }


        #endregion

        #region private Messages

        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion
    }
}