using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class Dialog : BaseEntity<int>
    {

        public int CreatorId { get; set; }
        public UserProfile Creator { get; set; }

        public int CompanionId { get; set; }
        public UserProfile Companion { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
