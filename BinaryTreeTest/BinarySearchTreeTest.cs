using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BinaryTree;
using Daybook;
using NUnit.Framework;

namespace BinaryTreeTest
{
    [TestFixture]
    public class BinaryTreeTests
    {
        private BinarySearchTree<Student> tree;

        public BinaryTreeTests()
        {
            tree = new BinarySearchTree<Student>();
        }

        [SetUp]
        public void TestSetup()
        {
            tree = new BinarySearchTree<Student>();
            tree.Add(new Student("Dmytro", "Burlak", "Math", DateTime.Now, 60));
            tree.Add(new Student("Vasul", "Gerashchenko", "Geography", DateTime.Now, 74));
            tree.Add(new Student("Taras", "Panichenko", "English", DateTime.Now, 65));
        }

        [TearDown]
        public void TestTearDown()
        {
            tree = null;
        }

        [Test]
        public void When_add_range_Then_count_is_equal()
        {
            List<Student> studentList = new List<Student>
            {
                new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80),
                new Student("Petro", "Petrov", "History", DateTime.Now, 70),
                new Student("Vasya", "Pupkin", "English", DateTime.Now, 90),
                new Student("Oksana", "Smirnova", "Physics", DateTime.Now, 85)
            };

            tree.Add(studentList);

            Assert.That(tree.Count, Is.EqualTo(7));
        }

        [Test]
        public void When_add_element_Then_tree_contains_it()
        {
            Student student = new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80);

            tree.Add(student);

            CollectionAssert.Contains(tree, student);
        }

        [Test]
        public void When_add_range_Then_tree_contains_it()
        {
            List<Student> studentList = new List<Student>
            {
                new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80),
                new Student("Petro", "Petrov", "History", DateTime.Now, 70),
                new Student("Vasya", "Pupkin", "English", DateTime.Now, 90),
                new Student("Oksana", "Smirnova", "Physics", DateTime.Now, 85)
            };

            tree.Add(studentList);

            CollectionAssert.IsSubsetOf(studentList, tree);
        }

        [Test]
        public void When_delete_element_not_from_tree_Then_throw_exception()
        {
            Student student = new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80);

            Assert.Throws<InvalidOperationException>(() => tree.Remove(student));
        }

        [Test]
        public void When_delete_element_Then_tree_doesnt_contain_it()
        {
            Student student = new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80);
            tree.Add(student);
            tree.Remove(student);

            CollectionAssert.DoesNotContain(tree, student);
        }

        [Test]
        public void When_add_range_Then_enumerator_contains_it()
        {
            List<Student> studentList = new List<Student>
            {
                new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80),
                new Student("Petro", "Petrov", "History", DateTime.Now, 70),
                new Student("Vasya", "Pupkin", "English", DateTime.Now, 90),
                new Student("Oksana", "Smirnova", "Physics", DateTime.Now, 85)
            };

            tree.Add(studentList);

            List<Student> treeList = new List<Student>();
            foreach(var item in tree)
                treeList.Add(item);

            CollectionAssert.IsSubsetOf(studentList, treeList);
        }

        [Test]
        public void When_element_added_Then_event_works()
        {
            var wasCalled = false;
            tree.NodeAdded += (sender) => { wasCalled = true; };

            tree.Add(new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80));

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void When_element_removed_Then_event_works()
        {
            Student student = new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80);
            tree.Add(student);
            var wasCalled = false;
            tree.NodeRemoved += (sender) => { wasCalled = true; };

            tree.Remove(student);

            Assert.IsTrue(wasCalled);
        }
    }
}

