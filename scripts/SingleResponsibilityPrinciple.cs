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

//Adding too much functionality to a single class can cause problems later
//So adding all these persistance functionalities to the Journal class is not recommended per the Single Responsibilty Principle
        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        
        public static Journal Load(string filename)
        {
            return null;
        }

        public void Load(Uri uri)
        {

        }
    
    }

//So do not add these to the single class, make an additional class dedicated to Persistance like below
//This is called a separation of concerns
    public class Persistance
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }

    }

    public class SingleResponsibilityPrinciple
    {
        //uncomment to run
        //static void Main(string[] args)
        //{
        //    var j = new Journal();
        //    j.AddEntry("It was sunny, and beautiful");
        //    j.AddEntry("Woe is me");
        //    WriteLine(j);

        //    var p = new Persistance();
        //    var filename = @"C\journal.txt";
        //    p.SaveToFile(j, filename, true);
        //    Process.Start(filename);

        //}
    }
}