using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
