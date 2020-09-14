using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public class BinarySearchTree
    {
        private Node _rootNode;

        public BinarySearchTree() { }

        public BinarySearchTree(IEnumerable<int> numbers)
        {
            foreach (var num in numbers)
            {
                Insert(num);
            }
        }

        public void Insert(int value)
        {
            _rootNode = InsertRec(value, _rootNode);
        }

        private Node InsertRec(int value, Node node)
        {
            if (node == null) return new Node(value);

            if (node.Value > value)
            {
                node.Left = InsertRec(value, node.Left);
            }
            else if (node.Value < value)
            {
                node.Right = InsertRec(value, node.Right);
            }

            return node;
        }

        public bool Search(int value) => SearchRec(value, _rootNode);

        private bool SearchRec(int value, Node node) => node switch
        {
            null => false,
            Node n when n.Value == value => true,
            Node n when n.Value > value => SearchRec(value, n.Left),
            Node n when n.Value < value => SearchRec(value, n.Right),
            _ => throw new Exception("Search Error")
        };

        public void Remove(int value)
        {
            _rootNode = RemoveRec(value, _rootNode);
        }

        private Node RemoveRec(int value, Node node)
        {
            if (node.Value == value)
            {
                // 削除対象ノードが子を２つ持つ場合
                // 1. 削除対象ノードの右の子から最小値を取得する
                // 2. 1で取得したノードを削除対象ノードと置き換える。その後、削除対象ノードを削除する。
                //    このとき置き換えたノードの右ノードを1で取得したノードの位置に置く
                Func<Node, Node> twoChildren = node =>
                {
                    var rightMinNode = MinNode(node.Right);
                    node.Value = rightMinNode.Value;
                    node.Right = RemoveRec(rightMinNode.Value, node.Right);
                    return node;
                };

                return (node.Left, node.Right) switch
                {
                    // 削除対象ノードの子が1つだけ: 削除対象ノードを子ノードと置き換える
                    ({ }, null) => node.Left,
                    (null, { }) => node.Right,
                    ({ }, { }) => twoChildren(node), // 削除対象ノードが子を２つ持つ
                    (null, null) => null,            // 削除対象ノードが子を持たない: nullを返す(そのまま削除)
                };
            }

            if (node == null) return node;
            if (node.Value > value)
            {
                node.Left = RemoveRec(value, node.Left);
            }
            else if (node.Value < value)
            {
                node.Right = RemoveRec(value, node.Right);
            }

            return node;
        }

        private Node MinNode(Node node)
        {
            return node.Left == null
                ? node
                : MinNode(node.Left);
        }
    }
}