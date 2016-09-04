using System;
using System.Collections.Generic;
using BinaryTree;


namespace Daybook
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<Student> bt = new BinarySearchTree<Student>();
            bt.NodeAdded += Info;
            bt.NodeRemoved += Info;
            List<Student> studentList = new List<Student>();
            try
            {
                studentList.Add(new Student("Ivan", "Ivanov", "Math", DateTime.Now, 80));
                studentList.Add(new Student("Petro", "Petrov", "History", DateTime.Now, 70));
                studentList.Add(new Student("Vasya", "Pupkin", "English", DateTime.Now, 90));
                studentList.Add(new Student("Oksana", "Smirnova", "Physics", DateTime.Now, 85));
                studentList.Add(new Student("Dmytro", "Burlak", "Math", DateTime.Now, 60));
                studentList.Add(new Student("Vasul", "Gerashchenko", "Geography", DateTime.Now, 74));
                studentList.Add(new Student("Taras", "Panichenko", "English", DateTime.Now, 65));
                bt.Add(studentList);
                Console.WriteLine(bt.Contains(studentList[2]));
                foreach (var item in bt)
                    Console.WriteLine(item);
                bt.Remove(studentList[4]);
                Console.WriteLine();
                Console.WriteLine("After removing:");
                foreach (var item in bt)
                    Console.WriteLine(item);
                bt.Remove(new Student("Igor", "Gerasimenko", "English", DateTime.Now, 95));
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Info(string message)
        {
            Console.WriteLine(message);
        }
    }

}
