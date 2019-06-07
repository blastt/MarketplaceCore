using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.SignalRHubs
{
    public class MessageHub : Hub, IMessageHub
    {
        readonly IHubContext<MessageHub> context;
        public MessageHub(IHubContext<MessageHub> context)
        {
            this.context = context;
        }
        public void UpdateMessage(int messagesCounter, string userId)
        {
            context.Clients.User(userId).SendAsync("updateMessage", messagesCounter, userId);
        }

        public void UpdateMessageInDialog(int messagesCounter, string lastMessage, string date, int dialogId, string userName, string companionAvatar, string companionName)
        {
            context.Clients.User(userName).SendAsync("updateMessageInDialog", companionAvatar, companionName, messagesCounter, lastMessage, date, dialogId, userName);
        }

        public void AddMessage(string receiverName, string senderName, string messageBody, string date, string senderImage)
        {
            context.Clients.User(senderName).SendAsync("addMessage", receiverName, senderName, messageBody, date, senderImage, senderName);
            context.Clients.User(receiverName).SendAsync("addMessage", receiverName, senderName, messageBody, date, senderImage, receiverName);

        }

        public void AddDialog(string userName, int dialogId, string companionAvatar, string companionName)
        {
            context.Clients.User(userName).SendAsync("addDialog", dialogId, companionAvatar, companionName, userName);
        }
    }
}
