using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeApp
{
    public interface ITree<T> : IEnumerable<T> where T : IComparable<T>
    {
        void InsertElement(T value);

        void RemoveElement(T value);

        bool FindElement(T value);

    }
}
