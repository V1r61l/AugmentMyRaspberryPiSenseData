using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core.Domain.Contracts
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Store(T item);
        void Delete(T item);
        void Update(T item);
        void BatchStore(IEnumerable<T> items);
        IEnumerable<T> GetAll();
        T GetItem(string key);
    }
}
