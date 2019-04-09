using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Interfaces
{
    public interface IDeleteEntity<T> : IEntity<T>
    {
        DateTime DeletedTime { get; set; }
    }
}
