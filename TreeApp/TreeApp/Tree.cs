using System;
using System.Collections;
using System.Collections.Generic;

namespace TreeApp
{
    public class Tree<T> : ITree<T> where T : IComparable<T>
    {
        private class TreeElement
        {
            public T Value { get; set; }
            public TreeElement RightChild { get; set; }
            public TreeElement LeftChild { get; set; }
            public TreeElement Parent { get; set; }

            public TreeElement(T value)
            {
                Value = value;
                RightChild = null;
                LeftChild = null;
                Parent = null;
            }
        }

        private TreeElement treeHead = null;

        public void InsertElement(T value)
        {
            if (treeHead == null)
            {
                treeHead = new TreeElement(value);
                treeHead.Parent = treeHead;
                return;
            }
            var newElement = new TreeElement(value);
            var tempElement = treeHead;
            while (tempElement != null)
            {
                if (newElement.Value.CompareTo(tempElement.Value) == 0)
                {
                    return;
                }
                if (newElement.Value.CompareTo(tempElement.Value) > 0)
                {
                    if (tempElement.RightChild == null)
                    {
                        tempElement.RightChild = newElement;
                        tempElement.RightChild.Parent = tempElement;
                        return;
                    }
                    tempElement = tempElement.RightChild;
                }
                else
                {
                    if (tempElement.LeftChild == null)
                    {
                        tempElement.LeftChild = newElement;
                        tempElement.LeftChild.Parent = tempElement;
                        return;
                    }
                    tempElement = tempElement.LeftChild;
                }
            }
            if (newElement.Value.CompareTo(treeHead.Value) <= 0)
            {
                treeHead.LeftChild = newElement;
            }
            else
            {
                treeHead.RightChild = newElement;
            }
        }

        public void RemoveElement(T value)
        {
            if (FindElement(value))
            {
                var tempElement = treeHead;
                var parentElement = tempElement;
                while (tempElement.Value.CompareTo(value) != 0)
                {
                    if (value.CompareTo(tempElement.Value) > 0)
                    {
                        parentElement = tempElement;
                        tempElement = tempElement.RightChild;
                    }
                    else if (value.CompareTo(tempElement.Value) < 0)
                    {
                        parentElement = tempElement;
                        tempElement = tempElement.LeftChild;
                    }
                }

                if ((tempElement.LeftChild != null) && (tempElement.RightChild != null))
                {
                    var delElement = tempElement.RightChild;
                    if (delElement.LeftChild != null)
                    {
                        while (delElement.LeftChild.LeftChild != null)
                        {
                            delElement = delElement.LeftChild;
                        }
                        tempElement.Value = delElement.LeftChild.Value;
                        if (delElement.LeftChild.RightChild != null)
                        {
                            delElement.LeftChild.Value = delElement.LeftChild.RightChild.Value;
                            delElement.LeftChild.RightChild = null;
                        }
                        else
                        {
                            delElement.LeftChild = null;
                        }
                    }
                    else
                    {
                        tempElement.LeftChild.Parent = tempElement.RightChild;
                        tempElement.RightChild.LeftChild = tempElement.LeftChild;
                    }
                }
                else if ((tempElement.LeftChild != null) && (tempElement.RightChild == null))
                {
                    if (parentElement == tempElement)
                    {
                        treeHead = tempElement.LeftChild;
                        return;
                    }
                    if (parentElement.LeftChild.Value.CompareTo(tempElement.Value) == 0)
                    {
                        parentElement.LeftChild = tempElement.LeftChild;
                        tempElement.LeftChild.Parent = parentElement;
                    }
                    else
                    {
                        parentElement.RightChild = tempElement.LeftChild;
                        tempElement.LeftChild.Parent = parentElement;
                    }
                }
                else if ((tempElement.RightChild != null) && (tempElement.LeftChild == null))
                {
                    if (parentElement == tempElement)
                    {
                        treeHead = tempElement.RightChild;
                        return;
                    }
                    if (parentElement.RightChild.Value.CompareTo(tempElement.Value) == 0)
                    {
                        parentElement.RightChild = tempElement.RightChild;
                        tempElement.RightChild.Parent = parentElement;
                    }
                    else
                    {
                        parentElement.LeftChild = tempElement.RightChild;
                        tempElement.RightChild.Parent = parentElement;
                    }
                }
                else
                {
                    if (parentElement == tempElement)
                    {
                        treeHead = null;
                        return;
                    }
                    if (parentElement.LeftChild.Value.CompareTo(tempElement.Value) == 0)
                    {
                        parentElement.LeftChild = null;
                    }
                    else
                    {
                        parentElement.RightChild = null;
                    }
                }
            }
        }

        public bool FindElement(T value)
        {
            if (treeHead == null)
            {
                return false;
            }
            else
            {
                var tempElement = treeHead;
                while (tempElement != null)
                {
                    if (value.CompareTo(tempElement.Value) > 0)
                    {
                        tempElement = tempElement.RightChild;
                    }
                    else if (value.CompareTo(tempElement.Value) < 0)
                    {
                        tempElement = tempElement.LeftChild;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Enumerator : IEnumerator<T>
        {
            private Tree<T> tree;
            private List<T> treeList;
            private int position;

            public Enumerator(Tree<T> tree)
            {
                this.tree = tree;
                treeList = new List<T>();
                position = -1;
                MakeTreeList(tree.treeHead);
            }

            private void MakeTreeList(TreeElement element)
            {
                if (element != null)
                {
                    if (element.RightChild != null)
                    {
                        MakeTreeList(element.RightChild);
                    }
                    if (element.LeftChild != null)
                    {
                        MakeTreeList(element.LeftChild);
                    }
                    treeList.Add(element.Value);
                }
            }

            public object Current
            {
                get
                {
                    return Current;
                }
            }

            T IEnumerator<T>.Current
            {
                get
                {
                    return treeList[position];
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (position < treeList.Count - 1)
                {
                    position++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}

