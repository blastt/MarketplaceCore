using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace Marketplace.Web
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attributes =
                (DisplayAttribute[])fi.GetCustomAttributes(
                typeof(DisplayAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].GetName();
            else
                return enumValue.ToString();

        }
    }
}
