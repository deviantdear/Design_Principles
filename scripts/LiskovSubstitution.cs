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
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        //Overloaded Constructors
        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        //Formats the return text
        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    //Created below is a Square class that seems workable and will sometimes get the results you want...
    //..but! Because the way that Square inherited from rectangle means you cannot substitute the Rectangle for the Square 
    // without running into errors when you reset just the width or height on the rectangle
    //You must make the properties of the Rectangle above virtual so you can override them in the square class
    public class Square : Rectangle
    {
        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
        public override int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
    }

    #region Description of Principle
    //Liskov Substitution is the "L" in the SOLID principles of OOP attributed to Barbara Liskov of MIT
    // Object Oriented Programming Principle stating that in a computer program that 2 objects in 
    //a subtype can be substituted without altering any type of the desirable properties of the program
    #endregion

    public class LiskovSubstitution
    {
         //Lambda operator returning value of the area

        static public int Area(Rectangle r) => r.Width * r.Height;

        //public static void Main(string[] args)
        //{
        //    Rectangle rc = new Rectangle();
        //    WriteLine($"{rc} has area { Area(rc)}");
        //}
    }
}
