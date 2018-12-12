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
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small,Medium, Large, Huge
    }

    public class Product
    {
        //remember it is illegal to have public fields in .Net but for the sake of example these will be public
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    //Filter only specific criteria

    public class ProductFilter
    {
        //Filtering by just Size
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }
        //Filtering by just color
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }

        //Oh no! Filitering by color and size. Ew. Gross. This violates the Open/Close principle because you want to be able to 
        //Extend the product filter class but closed for modification so you don't have to keep adding new specifications

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        { 
            //don't do this see note above and below
            foreach (var p in products)
                if (p.Size == size && p.Color == color)
                    yield return p;
        }

    }

//use inheritance instead! We will also implement a pattern called the Specification pattern

    public interface ISpecification<T>
    {
        //Checks to see if the specification of type t is satisfied
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        //filters based on any type T under the ISpecification
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification: ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        //here is the inherited member of the ISpecification
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification: ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    //Now to create a Combinator to check multiple specifications

    public class AndSpecification<T>: ISpecification<T>
    {
        ISpecification<T> first, second;

        //Constructor
        public AndSpecification(ISpecification <T> first, ISpecification <T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    //Now we will implement an even better filter

    public class BetterFilter: IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if(spec.IsSatisfied(i))
                    yield return i;
        }
    }

    public class OpenClosedPrinciple
    {
	    static void Main(String [] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

//Implementing the wrong way to implement filtering

            var pf = new ProductFilter();
            WriteLine("Green products (old):");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                WriteLine($"- {p.Name} is green");
            }

//Implementing the correct way to filter with the Open Closed Principle

            var bf = new BetterFilter();
            WriteLine("Green products (new):");
            foreach(var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($"- {p.Name} is green");
            }

//They both produce identical results
//Below you can see how you can create an iSpecification to filter out multiple requirements

            WriteLine("Large Blue items");
            foreach(var p in bf.Filter(
                products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large)
                    )))
            {
                WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}


