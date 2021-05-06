using System;
using System.Collections.Generic;

namespace TreeApp
{
    /// <summary>
    /// binary tree interface
    /// </summary>
    /// <typeparam name="T">tree elements type</typeparam>
    public interface ITree<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// insert element in tree
        /// </summary>
        /// <param name="value">value on inserted element</param>
        void InsertElement(T value);

        /// <summary>
        /// remove element from tree
        /// </summary>
        /// <param name="value">value of deleted element</param>
        void RemoveElement(T value);

        /// <summary>
        /// find tree element
        /// </summary>
        /// <param name="value">value of finding element</param>
        /// <returns></returns>
        bool FindElement(T value);

    }
}
