using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeApp;

namespace TreeApp.Test
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void AddIntAndFindTest()
        {
            var tree = new Tree<int>();
            tree.InsertElement(0);
            Assert.IsTrue(tree.FindElement(0));
        }

        [TestMethod]
        public void AddStringAndFindTest()
        {
            var tree = new Tree<string>();
            tree.InsertElement("Jack");
            Assert.IsTrue(tree.FindElement("Jack"));
        }

        [TestMethod]
        public void RemoveElementTest()
        {
            var tree = new Tree<int>();
            tree.InsertElement(0);
            tree.InsertElement(1);
            Assert.IsTrue(tree.FindElement(0));
            Assert.IsTrue(tree.FindElement(1));
            tree.RemoveElement(0);
            Assert.IsFalse(tree.FindElement(0));
            Assert.IsTrue(tree.FindElement(1));
            tree.RemoveElement(1);
            Assert.IsFalse(tree.FindElement(1));
        }

        [TestMethod]
        public void EnumTest()
        {
            var tree = new Tree<int>();
            var numberOfElements = 5;
            for (var i = 0; i < numberOfElements; i++)
            {
                tree.InsertElement(i);
            }
            var tempNumber = 0;
            foreach(var treeElement in tree)
            {
                Assert.IsTrue(tree.FindElement(tempNumber++)); 
            }
            Assert.AreEqual(tempNumber, numberOfElements);
        }
    }
}
