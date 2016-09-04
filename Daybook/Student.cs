using System;

namespace Daybook
{
    public class Student : IComparable<Student>
    {
        private static int id = 0;
        public int RecordId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Test { get; set; }
        public DateTime Date { get; set; }
        public int Mark { get; set; }

        public Student(string firstName, string secondName, string test, DateTime date, int mark)
        {
            RecordId = id++;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Test = test;
            this.Date = date;
            this.Mark = mark;
        }

        public int CompareTo(Student student)
        {
            return Mark - student.Mark;
        }

        public override string ToString()
        {
            string str = "\n" + FirstName + " " + SecondName + "\n";
            str += Test + " " + Date + " " + Mark;
            return str;
        }
    }

}
