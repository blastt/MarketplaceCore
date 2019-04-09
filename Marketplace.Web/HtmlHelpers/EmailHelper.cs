using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.HtmlHelpers
{
    public static class EmailHelper
    {
        public static HtmlString ActivateForm(this IHtmlHelper html, string text, string linkText, string hrefUrl)
        {
            TagBuilder div = new TagBuilder("div");
            TagBuilder p = new TagBuilder("p");
            TagBuilder a = new TagBuilder("a");
            TagBuilder strong = new TagBuilder("strong");
            div.MergeAttribute("style", "text-align: center");
            p.InnerHtml.Append(text);
            a.MergeAttribute("style", "background-color: #168de2; padding: 10px 30px; text-decoration:none ;font-size:14px; color:#ffffff;");
            a.MergeAttribute("href", hrefUrl);
            strong.InnerHtml.Append(linkText);
            a.InnerHtml.AppendHtml(strong);
            div.InnerHtml.AppendHtml(p);
            div.InnerHtml.AppendHtml(a);


            return new HtmlString(div.ToString());
        }

        public static HtmlString AccountData(this IHtmlHelper html, string login, string password, string email, string emailPassword, string additionalInfo)
        {
            TagBuilder div = new TagBuilder("div");
            TagBuilder labelLogin = new TagBuilder("label");
            TagBuilder spanLogin = new TagBuilder("span");
            spanLogin.MergeAttribute("style", "width: 100%");
            TagBuilder labelPassword = new TagBuilder("label");
            TagBuilder spanPassword = new TagBuilder("span");
            spanPassword.MergeAttribute("style", "width: 100%");
            TagBuilder labelEmail = new TagBuilder("label");
            TagBuilder spanEmail = new TagBuilder("span");
            spanEmail.MergeAttribute("style", "width: 100%");
            TagBuilder labelEmailPass = new TagBuilder("label");
            TagBuilder spanEmailPass = new TagBuilder("span");
            spanEmailPass.MergeAttribute("style", "width: 100%");
            TagBuilder labelAdd = new TagBuilder("label");
            TagBuilder spanAdd = new TagBuilder("span");
            spanAdd.MergeAttribute("style", "width: 100%");
            TagBuilder div1 = new TagBuilder("div");
            TagBuilder div2 = new TagBuilder("div");
            TagBuilder div3 = new TagBuilder("div");
            TagBuilder div4 = new TagBuilder("div");
            TagBuilder div5 = new TagBuilder("div");

            div1.MergeAttribute("style", "width: 100%");
            div2.MergeAttribute("style", "width: 100%");
            div3.MergeAttribute("style", "width: 100%");
            div4.MergeAttribute("style", "width: 100%");
            div5.MergeAttribute("style", "width: 100%");


            labelLogin.InnerHtml.Append("Логин аккаунта:");
            spanLogin.InnerHtml.Append(login);
            div1.InnerHtml.AppendHtml(labelLogin);
            div1.InnerHtml.AppendHtml(spanLogin);
            labelPassword.InnerHtml.Append("пароль аккаунта:");
            spanPassword.InnerHtml.Append(password);
            div2.InnerHtml.AppendHtml(labelPassword);
            div2.InnerHtml.AppendHtml(spanPassword);
            labelEmail.InnerHtml.Append("Почта:");
            spanEmail.InnerHtml.Append(email);
            div3.InnerHtml.AppendHtml(labelEmail);
            div3.InnerHtml.AppendHtml(spanEmail);
            labelEmailPass.InnerHtml.Append("Пароль почты:");
            spanEmailPass.InnerHtml.Append(emailPassword);
            div4.InnerHtml.AppendHtml(labelEmailPass);
            div4.InnerHtml.AppendHtml(spanEmailPass);
            labelAdd.InnerHtml.Append("Дополнительная информация:");
            spanAdd.InnerHtml.Append(additionalInfo);
            div5.InnerHtml.AppendHtml(labelAdd);
            div5.InnerHtml.AppendHtml(spanAdd);

            div.InnerHtml.AppendHtml(div1);
            div.InnerHtml.AppendHtml(div2);
            div.InnerHtml.AppendHtml(div3);
            div.InnerHtml.AppendHtml(div4);
            div.InnerHtml.AppendHtml(div5);



            return new HtmlString(div.ToString());
        }
    }
}
