using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Extensions
{
    public static class DateTimeExtensions
    {        
        public static string ToShortMonthName(this DateTime dateTime)
        {
            int result = dateTime.Date.CompareTo(DateTime.Now.Date);
            var s = SecureTransactionPayerViewModel.Seller.GetDisplayName();
            if (result == 0)
            {
                return string.Format($"Today {dateTime.ToShortTimeString()}");
            }            
            return string.Format($"{dateTime.Day} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month)} {dateTime.Year}");
        }
    }
}
