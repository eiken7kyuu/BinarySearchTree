using System.Collections.Generic;
using Xunit;

namespace BinarySearchTree.Tests
{
    public class BinarySearchTreeTest
    {
        private readonly BinarySearchTree bsTree;
        private readonly IEnumerable<int> numbers = new int[] { 6, 8, 3, 11, 9, 2, 20, 17, 19 };

        public BinarySearchTreeTest()
        {
            bsTree = new BinarySearchTree(numbers);
        }

        [Fact]
        public void 存在する要素はTrueを返す()
        {
            Assert.True(bsTree.Search(6));
            Assert.True(bsTree.Search(8));
            Assert.True(bsTree.Search(3));
            Assert.True(bsTree.Search(11));
            Assert.True(bsTree.Search(9));
            Assert.True(bsTree.Search(2));
            Assert.True(bsTree.Search(20));
            Assert.True(bsTree.Search(17));
        }

        [Fact]
        public void 存在しない要素はFalseを返す()
        {
            Assert.False(bsTree.Search(1));
            Assert.False(bsTree.Search(4));
            Assert.False(bsTree.Search(18));
        }

        [Fact]
        public void 新しい要素を追加()
        {
            bsTree.Insert(1);
            bsTree.Insert(99);
            Assert.True(bsTree.Search(1));
            Assert.True(bsTree.Search(99));
        }

        [Fact]
        public void 存在する要素を削除()
        {
            Assert.True(bsTree.Search(11));
            bsTree.Remove(11);
            Assert.False(bsTree.Search(11));
        }
    }
}