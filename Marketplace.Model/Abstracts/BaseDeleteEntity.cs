using Marketplace.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Marketplace.Model.Abstracts
{
    public abstract class BaseDeleteEntity<T> : IDeleteEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime DeletedTime { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
