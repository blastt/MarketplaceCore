using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Marketplace.Model
{
    public class Enums_SecureTransactionPayer
    {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;

        static Enums_SecureTransactionPayer()
        {
            
            resourceMan = new global::System.Resources.ResourceManager("Resources.Resources", Assembly.GetExecutingAssembly());
            resourceCulture = CultureInfo.CurrentCulture;
        }

        public static string Seller
        {
            get
            {
                
                return resourceMan.GetString("Seller", CultureInfo.CurrentCulture);
            }
        }

        public static string Buyer
        {
            get
            {
                
                return resourceMan.GetString("Buyer", CultureInfo.CurrentCulture);
            }
        }

        public static string InTwain
        {
            get
            {
                return resourceMan.GetString("InTwain", CultureInfo.CurrentCulture);
            }
        }
    }
}
