using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Mid_Term
{
    public class TextFile
    {
        //READ AND PERFORM BELOW STEPS BEFORE FIRST EXECUTING ANY OF THE METHODS
        //https://stackoverflow.com/questions/6416564/how-to-read-a-text-file-in-projects-root-directory

        /*Normally if we don't use the "GetSpecialFolder" method for a path, 
         * it automatically looks within our project bin folder, but that's not where our text file is. I believe the below will make a copy of 
         * the txt file when the code builds and put that copy in the bin folder. It worked for me. */

        public static void WriteToTxt(string name, string category, string description, double price)
        {
            var textFile = @"productlist.txt";
            List<string> theMenu = File.ReadAllLines(textFile).ToList();
            string newItem = $"{name},{category},{description},{price}";
            theMenu.Add(newItem);
            File.WriteAllLines(textFile, theMenu);

            OutputTxtFile(); //you can remove this if you want and use outside of the method if that's easier
        }

        public static void OutputTxtFile() //USE THIS METHOD TO DISPLAY THE MENU
        {
            var textFile = @"productlist.txt";
            List<Product> theMenu = ReadFromTxt(textFile);

            int itemNumber = 0;

            foreach (var food in theMenu)
            {
                Console.WriteLine($"{itemNumber}: { food.name}, {food.category}, {food.description}, ${food.price}");
                itemNumber += 1;
            }
        }

        public static List<Product> ReadFromTxt(string txtFile) //THIS METHOD WILL RUN IN THE ABOVE METHOD AND ADD TXT ITEMS TO A LIST
        {
            List<Product> menuItems = new List<Product>();
            List<string> lines = File.ReadAllLines(txtFile).ToList();

            foreach (var line in lines)
            {
                string[] menuItem = line.Split(',');

                Product newItem = new Product();

                newItem.name = menuItem[0];
                newItem.category = menuItem[1];
                newItem.description = menuItem[2];
                double.TryParse(menuItem[3], out double price);
                newItem.price = price;

                menuItems.Add(newItem);
            }

            return menuItems;
        }
    }
}
