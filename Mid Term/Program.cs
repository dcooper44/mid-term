using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Mid_Term
{
    class Program
    {
        static void Main(string[] args)
        {

            ShoppingTwo.GetItemFromUser(TextFile.ReadFromTxt(@"productlist.txt"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            ShoppingTwo.UpdateMenu();
            
        }
    }
}
