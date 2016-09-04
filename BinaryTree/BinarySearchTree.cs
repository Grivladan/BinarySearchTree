using System;
using System.Collections.Generic;

namespace BinaryTree
{
    public class BinarySearchTree<T> : IEnumerable<T>, ICollection<T> where T : IComparable<T>
    {
        class Node<TNode> where TNode : IComparable<TNode>
        {
            public TNode Value { get; set; }
            public Node<TNode> Left { get; set; }
            public Node<TNode> Right { get; set; }
            public Node<TNode> Parent { get; set; }

            public Node(TNode value)
            {
                Value = value;
            }
        }

        private Node<T> _head;

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public BinarySearchTree()
        {
            _head = null;
            Count = 0;
        }

        public void Add(T value)
        {
            if (value == null)
                throw new NullReferenceException("Argument for insertion is null");
            if (_head == null)
                _head = new Node<T>(value);
            else
            {
                Add(_head, value);
            }
            if (NodeAdded != null)
            {
                NodeAdded("Node was added");
            }
            Count++;
        }

        public void Add(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        private void Add(Node<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    Node<T> tmp = new Node<T>(value);
                    tmp.Parent = node;
                    node.Left = tmp;
                }
                else
                {
                    Add(node.Left, value);
                }
            }
            else
                if (value.CompareTo(node.Value) > 0)
                {
                    if (node.Right == null)
                    {
                        Node<T> tmp = new Node<T>(value);
                        tmp.Parent = node;
                        node.Right = tmp;
                    }
                    else
                    {
                        Add(node.Right, value);
                    }
                }
        }

        public Boolean Contains(T value)
        {
            if (value == null)
                throw new NullReferenceException("Argument for contains is null");
            return Find(value) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        private Node<T> Find(T value)
        {
            Node<T> current = _head;

            while (current != null)
            {
                int comp = value.CompareTo(current.Value);
                if (comp < 0)
                {
                    current = current.Left;
                }
                else if (comp > 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        private Node<T> FindNext(T value)
        {
            Node<T> current = _head;
            Node<T> next = null;
            while (current != null)
            {
                if (current.Value.CompareTo(value) > 0)
                {
                    next = current;
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }
            return next;
        }

        public bool Remove(T value)
        {
            if (value == null)
                throw new NullReferenceException("Argument for deleting is null");
            Node<T> current = Find(value);
            if (current == null)
                throw new InvalidOperationException("You try to delete element that does't belong this tree");
            Node<T> p = current.Parent;
            if (current.Left == null && current.Right == null)
            {
                if (p.Left == current)
                    p.Left = null;
                else if (p.Right == current)
                {
                    p.Right = null;
                }
            }
            else if (current.Left == null || current.Right == null)
            {
                if (current.Left == null)
                {
                    if (p.Left == current)
                        p.Left = current.Right;
                    else if (p.Right == current)
                    {
                        p.Right = current.Right;
                    }
                    current.Right.Parent = p;
                }
                else if (current.Right == null)
                {
                    if (p.Left == current)
                        p.Left = current.Left;
                    else if (p.Right == current)
                        p.Right = current.Left;
                    current.Left.Parent = p;
                }
            }
            else
            {
                Node<T> next = FindNext(value);
                current.Value = next.Value;
                if (next.Parent.Left == next)
                {
                    next.Parent.Left = next.Right;
                    if (next.Right != null)
                        next.Right.Parent = next.Parent;
                }
                else
                {
                    next.Parent.Right = next.Right;
                    if (next.Right != null)
                        next.Right.Parent = next.Parent;
                }
            }
            Count--;
            if (NodeRemoved != null)
            {
                NodeRemoved("Node was removed ");
            }
            return true;
        }

        public T Minimum()
        {
            return Minimum(_head).Value;
        }

        private Node<T> Minimum(Node<T> node)
        {
            if (node.Left == null)
                return node;
            return Minimum(node.Left);
        }

        public T Maximum()
        {
            return Maximum(_head).Value;
        }

        private Node<T> Maximum(Node<T> node)
        {
            if (node.Right == null)
                return node;
            return Maximum(node.Left);
        }

        public IEnumerator<T> InOrderTraversal()
        {
            if (_head != null)
            {
                Stack<Node<T>> stack = new Stack<Node<T>>();

                Node<T> current = _head;
                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, _head);
        }

        private void PostOrderTraversal(Action<T> action, Node<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }

        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, _head);
        }

        private void PreOrderTraversal(Action<T> action, Node<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }

        public void Clear()
        {
            _head = null;
            Count = 0;
        }

        public delegate void SomeActionWithTree(string message);

        public event SomeActionWithTree NodeAdded;
        public event SomeActionWithTree NodeRemoved;

    }
}

