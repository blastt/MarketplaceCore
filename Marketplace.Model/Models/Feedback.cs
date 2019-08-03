using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    

    public class Feedback : BaseEntity<int>
    {
        public FeedbackGrade Grade { get; set; }
        public string Comment { get; set; }

        public Order Order { get; set; }

        public int UserToId { get; set; }
        public UserProfile UserTo { get; set; }

        public int UserFromId { get; set; }
        public UserProfile UserFrom { get; set; }

    }
}
