using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPrinciples
{
    #region Description of Principle
    //Every class should have a single responsibility over a part of the functionality provided by the software it is designed for
    #endregion

    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}:{text}");
            return count; //memento pattern
        }

        //not a stable way to remove entries because it removes the other indices
        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        //public static 
    }
    public class SingleResponsibilityPrinciple
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("It was sunny, and beautiful");
            j.AddEntry("Woe is me");

            WriteLine(j);
        }
        public SingleResponsibilityPrinciple()
        {
        }
    }
}