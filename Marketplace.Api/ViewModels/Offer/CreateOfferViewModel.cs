using Marketplace.Api.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.ViewModels.Offer
{
    public class CreateOfferViewModel
    {
        [Required]
        [Display(Name = "Основная игра")]
        public string Game { get; set; }

        public List<SelectListItem> Games { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Заголовок")]
        public string Header { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Логин продаваемого аккаунта")]
        public string AccountLogin { get; set; }


        [Display(Name = "Являестся ли аккаунт вашим основным (личный)?")]
        public bool PersonalAccount { get; set; }

        [Display(Name = "Дата регистрации аккаунта *")]
        public DateTime? CreatedAccountDate { get; set; }

        [Display(Name = "Есль ли бан на аккаунте? *")]
        public bool IsBanned { get; set; }

        [Display(Name = "Ссылка на аккаунт *")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }


        [Display(Name = "Платите ли вы за гаранта?")]
        public SecureTransactionPayerViewModel SecureTransactionPayer { get; set; }

        public string[] filters { get; set; }
    }
}
