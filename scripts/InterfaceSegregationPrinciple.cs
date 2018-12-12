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

    #region Principle Description
    //The Interface Segregation Principle is a design to avoid building interfaces that are too large
    //Interfaces that are too large create problems when you provide more functions to a user that they may never use/need
    //In this example we have 2 printers one that is a multifunction printer and can scan, fax and print and another that can only print
    //If you make a single interface to serve both printers then there will be useless functions for the printer that doesn't have all features
    #endregion

    public class Document
    {

    }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
           //Yes it can Fax
        }

        public void Print(Document d)
        {
           //Yes it can Print
        }

        public void Scan(Document d)
        {
           //Yes it can Scan
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        //can't use all the functionality
        public void Fax(Document d)
        {
            throw new NotImplementedException(); //Useless
        }

        public void Print(Document d)
        {
            //Can Only Print :(
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException(); //Useless
        }
    }

    // So here we will make many smaller interfaces that segregate the different printer functions

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    //Here this Photocopier inherits the members of each interface selected
    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    //You can also create an interface that inherits from other interfaces to implement in a class like so
    public interface IMultiFunctionDevice: IScanner, IPrinter //...
    {

    }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
        }

        //Decorator Pattern below delegates the calls to the inner printer and scanner varaibles

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }

    public class InterfaceSegregationPrinciple
    {
	    public InterfaceSegregationPrinciple()
	    {
	    }
    }

}