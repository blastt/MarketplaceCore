﻿using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public enum OfferState
    {
        active,
        inactive,
        closed
    }

    public class Offer : BaseEntity<int>
    {
        public string Header { get; set; }

        public string Discription { get; set; }
        public string AccountLogin { get; set; }

        public bool PersonalAccount { get; set; }
        public DateTime? CreatedAccountDate { get; set; }
        public bool IsBanned { get; set; }
        public string Url { get; set; }

        public string JobId { get; set; } // id задачи

        public OfferState State { get; set; }

        public decimal Price { get; set; }

        public int Views { get; set; }

        public bool SellerPaysMiddleman { get; set; }

        public virtual IList<Screenshot> Screenshots { get; set; } = new List<Screenshot>();

        public decimal? MiddlemanPrice
        {
            get
            {
                decimal middlemanPrice = 0;

                if (Price < 3000)
                {
                    middlemanPrice = 300;

                }
                else if (Price < 15000)
                {
                    middlemanPrice = Price * Convert.ToDecimal(0.1);
                }
                else
                {
                    middlemanPrice = 1500;
                }

                return middlemanPrice;
            }

            private set
            {

            }
        }

        public DateTime? DateDeleted { get; set; }


        public int GameId { get; set; }
        public Game Game { get; set; }

        public Order Order { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        //public IList<FilterItem> FilterItems { get; set; }
        //public IList<FilterItem> FilterItems { get; set; }

    }
}
