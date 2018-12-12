using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

//uses value tuples from C# 7 so you many need to install that package to your visual studio

#region Principle Description
// Using a geneology database example we can define different levels of abstraction 
//
//
//
#endregion

namespace DesignPrinciples
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    //High level
    public class Person
    {
        public string Name;

        //public DateTime DateOfBirth etc..
    }

    //To prevent Low level data from being exposed create an interface that doesn't access the data directly

    public interface IRelationshpBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    //Low level API using an abstraction that is the IRelationship Browser
    public class Relationships: IRelationshpBrowser
    {
        private List<(Person, Relationship, Person)> relations
        = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            foreach (var r in relations.Where(
                x => x.Item1.Name == name &&
                     x.Item2 == Relationship.Parent))
            {
                yield return r.Item3;
            }
        }

        // public List<(Person, Relationship, Person)> Relations => relations; *BAD
    }

    public class DependencyInversionPrinciple
    { 
        //public  Research(Relationships relationships) *BAD
        //{
        //    var relations = relationships.Relations;
        //    foreach (var r in relations.Where(
        //        x => x.Item1.Name == "John" &&
        //             x.Item2 == Relationship.Parent))
        //    {
        //        WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}

        public DependencyInversionPrinciple(IRelationshpBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            WriteLine($"John has a child caller {p.Name}");
        }

        static void Main(string[] args)
        {
            //This way exposes low level data and private data as public and high level (accessible) which is not a good thing
            var parent = new Person { Name = "John"};
            var child1 = new Person { Name = "Betsy" };
            var child2 = new Person { Name = "Bo" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new DependencyInversionPrinciple(relationships);
        }
    }

}