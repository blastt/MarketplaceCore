using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class Message : BaseEntity<int>
    {
        public string MessageBody { get; set; }
        public bool FromViewed { get; set; }
        public bool ToViewed { get; set; }

        public bool SenderDeleted { get; set; }
        public bool ReceiverDeleted { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public UserProfile Sender { get; set; }
        public UserProfile Receiver { get; set; }

        public int DialogId { get; set; }
        public Dialog Dialog { get; set; }

    }
}
