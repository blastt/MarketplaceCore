using Marketplace.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Abstracts
{
    public class BaseUserEntity<T> : IdentityUser<T>, IEntity<T> where T : IEquatable<T>
    {
        public DateTime CreatedDate { get; set; }
    }
}
