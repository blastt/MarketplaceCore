using Marketplace.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Marketplace.Model.Abstracts
{
    public abstract class BaseEntity<T> : IEntity<T> where T : IEquatable<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
