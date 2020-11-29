using System;
using System.Collections.Generic;

namespace Mid_Term
{
    public class Shopping
    {
        //take in text file method, read from file (list)
        public static void StartShopping(List<Product> listOfFood)
        {
            do
            {
                List<Product> userShoppingCart = new List <Product>();
                //the entire process to start over with the user ..
                TextFile.OutputTxtFile();
                double grandTotal = 0;
                // line total varible

                int count = 0;
                do
                {
                    

                    double lineTotal = 0;
                    Console.WriteLine("Please choose a number from our menu    or the first letter of the item ");
                    string userInput = Console.ReadLine();
                    // if user enters a number 
                    if (int.TryParse(userInput, out int foodNumber))
                    {
                        userShoppingCart.Add(listOfFood[foodNumber]);
                    }
                    //if user enters a letter
                    else
                    {
                        // is a product of listOfFood
                        foreach (var product in listOfFood)
                        {
                            if (userInput.Equals(product.name[0]))
                            {
                                userShoppingCart.Add(product);
                            }
                        }
                    }

                    Console.WriteLine("How many?");
                    string userQuantity = Console.ReadLine();
                    int.TryParse(userQuantity, out int quantity);
                    // ref:  int count = 0;
                    lineTotal = quantity * userShoppingCart[count].price;
                    count += 1;


                    // update grandTotal what is added to the list
                    grandTotal += lineTotal;
                } while (true);

                // call the checkout Method
                //call display receipt Method
                //call update storeMenu Method


            } while (true);


            //Call The Checkout Method
            //Need To Bring grandTotal outside of current scope, we need to define this higher up.
            // Due to how the loops are set up above, we will never break out of the loops and reach this code
            // The Other Validation methods are built but Adryenne and I will have to work together on Monday to make them fit in the Method Above
            CheckoutCartForUser(listOfFood, grandTotal);


        }

        public static void CheckoutCartForUser(Dictionary<Product, double> userCheckoutList, double grandTotal)
        {
            bool keepLooping = true;
            bool isCashCorrect = false;
            do
            {
                do
                {
                    Console.WriteLine("How Would You Like To Pay? (Enter a Number Below)\n 1) Cash \n 2) Credit \n 3) Check\n");
                    var userInput = Console.ReadLine();

                    if (userInput.Equals("cash", StringComparison.OrdinalIgnoreCase))
                    {
                        keepLooping = false;
                        Console.WriteLine("Please Enter the Amount of Cash you will be paying with");
                        var userCash = Console.ReadLine();
                        isCashCorrect = ValidateCashEntered(userCash, grandTotal);

                    }
                    else if (userInput.Equals("credit", StringComparison.OrdinalIgnoreCase))
                    {
                        keepLooping = false;
                        Console.WriteLine("Please Enter the Credit Card Number:");
                        var userCardNumber = Console.ReadLine();
                        Console.WriteLine("Please Enter the Expiration Date of your card");
                        var userExpirationDate = Console.ReadLine();
                        Console.WriteLine("Please Enter the CW of your credit card");
                        var userCW = Console.ReadLine();


                    }
                    else if (userInput.Equals("check", StringComparison.OrdinalIgnoreCase))
                    {
                        keepLooping = false;
                        Console.WriteLine("Please Enter the Check Number");
                        var userCheckNumber = Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("That is not a form of payment, please try again");
                        keepLooping = true;

                    }
                } while (keepLooping);
            } while (isCashCorrect);


        }

        public static bool ValidateCashEntered(string userCash, double grandTotal)
        {
            if (double.TryParse(userCash, out double userCashDouble) && (userCashDouble > grandTotal))
            {
                return false;
            }
            else
            {
                Console.WriteLine("This Does Not Work, Please Try Again");
                return true;
            }
        }

    }
}
