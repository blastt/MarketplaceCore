using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя или почту")]
        [Display(Name = "Имя пользователя или почта")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
