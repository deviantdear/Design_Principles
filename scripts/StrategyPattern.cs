using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatterns { 

    public enum OutputFormat
    {
        Markdown,
        Html
    }

    // <ul><li>foo<//li><ul>
    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);

    }

    public class HTMLListStrategy: IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }

    public class StrategyPattern
    {
        static void Main(string[] args)
        {

        }

	    public StrategyPattern()
	    {

	    }


    }// end class

} //end namespace