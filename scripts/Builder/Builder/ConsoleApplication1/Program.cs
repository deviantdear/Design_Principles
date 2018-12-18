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
    //Fluent Builder Inheritance with Recursive Generics

    public class Person
    {
        public string Name;
        public string Position;

        public class Builder: PersonJobBuilder<Builder>
        {
            //This class makes the Person Job builder accessible
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    //
    public class PersonInfoBuilder<SELF>
        : PersonBuilder
        where SELF:PersonInfoBuilder<SELF>
    {  
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }
    }

    //Don't inherit from fluent builders and this is why...

    public class PersonJobBuilder<SELF>
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF: PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var me = Person.New
                .Called("Valarie")
                .WorkAsA("Developer")
                .Build();
            Console.WriteLine(me);
           
            // You can't use the containing type as the return type when you inherit from a fluent interface
            //so what you need to do is implement recursive generics 

        }
    }
}
