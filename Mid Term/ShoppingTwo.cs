using Mid_Term;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

class ShoppingTwo
{

    //This is the User Input Method
    public static void GetItemFromUser(List<Product> shoppingMenu)
    {
        bool invalidInput;
        bool continueAddingProducts;
        double grandTotal = 0;
        Dictionary<Product, double> userCheckoutList = new Dictionary<Product, double>(); //keep track of quantity instead of line total on this dictionary
        do
        {
            double lineTotal = 0;
            do
            {
                Console.WriteLine();
                TextFile.OutputTxtFile();
                Console.WriteLine("\nPlease select the product you wish to buy by typing in the position in which the product is in the list (1,2,3,4.. etc) or by typing in the first letter of the item name");
                var userFoodSelection = Console.ReadLine();
                
                
                Console.WriteLine($"\nPlease type in how many you'd like");
                var userAmountSelection = Console.ReadLine();

                if (ValidateFoodInput(shoppingMenu, userFoodSelection) && ValidateQuantityInput(userAmountSelection))
                {
                    try
                    {
                        invalidInput = false;
                        lineTotal = GetLineTotal(shoppingMenu, userFoodSelection, userAmountSelection);
                        userCheckoutList.Add(GenerateUserPickAsProduct(shoppingMenu, userFoodSelection), lineTotal);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("You Have Aleady Wiped us out of this Item!");                        

                        break;
                        //Console.WriteLine("Please Select A Different Item");                        
                        //invalidInput = false;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Please Try Again");
                    invalidInput = true;
                }
               
                
               
            } while (invalidInput);

            grandTotal = Math.Round(grandTotal + lineTotal, 2);

            continueAddingProducts = AskUserToContinueAddingProducts();
        } while (continueAddingProducts);


        //Call The Checkout Method
        var userPaymentType = CheckoutCartForUser(userCheckoutList, grandTotal);


        //Call The Receipt Method using the same input variables as the checkout method.
        GenerateReceiptForUser(userCheckoutList, grandTotal);

    }

    public static double GenerateLineTotal(Product food, int itemQuantity)
    {
        double lineTotal;

        lineTotal = food.price * itemQuantity;

        return lineTotal;

    }

    public static void GenerateReceiptForUser(Dictionary<Product, double> userCheckoutList, double subTotal)
    {
        var taxRate = 0.06;
        foreach (var keyValuePair in userCheckoutList)
        {
            Console.WriteLine($"{keyValuePair.Key.name}-----${keyValuePair.Value}");
        }
        var taxTotal = Math.Round(taxRate * subTotal, 2);
        var grandTotal = Math.Round(taxTotal + subTotal, 2);
        Console.WriteLine($"\nYour sub total is ${subTotal}");
        Console.WriteLine($"Your tax total is ${taxTotal}");
        Console.WriteLine($"Your grand total is ${grandTotal}");        
    }

    public static string CheckoutCartForUser(Dictionary<Product, double> userCheckoutList, double grandTotal)
    {
        bool keepLooping = true;
        bool isCashCorrect = false;
        string userInput;
        do
        {
            do
            {
                Console.WriteLine("\nHow Would You Like To Pay? \n Cash \n Credit \n Check\n");
                userInput = Console.ReadLine();

                if (userInput.Equals("cash", StringComparison.OrdinalIgnoreCase))
                {
                    keepLooping = false;
                    Console.WriteLine("\nPlease Enter the Amount of Cash you will be paying with");
                    var userCash = Console.ReadLine();
                    isCashCorrect = ValidateCashEntered(userCash, grandTotal);

                }
                else if (userInput.Equals("credit", StringComparison.OrdinalIgnoreCase))
                {
                    keepLooping = false;
                    Console.WriteLine("\nPlease Enter the Credit Card Number:");
                    var userCardNumber = Console.ReadLine();
                    Console.WriteLine("\nPlease Enter the Expiration Date of your card");
                    var userExpirationDate = Console.ReadLine();
                    Console.WriteLine("\nPlease Enter the CW of your credit card");
                    var userCW = Console.ReadLine();


                }
                else if (userInput.Equals("check", StringComparison.OrdinalIgnoreCase))
                {
                    keepLooping = false;
                    Console.WriteLine("\nPlease Enter the Check Number");
                    var userCheckNumber = Console.ReadLine();

                }
                else
                {
                    Console.WriteLine("\nThat is not a form of payment, please try again");
                    keepLooping = true;

                }
            } while (keepLooping);
        } while (isCashCorrect);
        return userInput;

    }

    public static bool ValidateCashEntered(string userCash, double grandTotal)
    {
        if (double.TryParse(userCash, out double userCashDouble) && (userCashDouble >= grandTotal))
        {
            return false;
        }
        else
        {
            Console.WriteLine("\nThis Does Not Work, Please Try Again");
            return true;
        }
    }

    public static Product GenerateUserPickAsProduct(List<Product> shoppingMenu, string userFoodSelection)
    {
        if (int.TryParse(userFoodSelection.ToString(), out int intFoodSelection))
        {
            return shoppingMenu[intFoodSelection];
        }
        else
        {
            char.TryParse(userFoodSelection, out char charFoodSelection);

            foreach (Product food in shoppingMenu)
            {
                if (food.name.StartsWith(charFoodSelection))
                {
                    return food;
                }
            }

        }
        return default;
    }

    public static bool ValidateQuantityInput(string userQuantityInput)
    {
        if (int.TryParse(userQuantityInput, out int userIntInput))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidateFoodInput(List<Product> shoppingMenu, string userFoodSelection)
    {
        if (int.TryParse(userFoodSelection.ToString(), out int intFoodSelection))
        {
            if ((intFoodSelection <= shoppingMenu.Count - 1) && intFoodSelection >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(char.TryParse(userFoodSelection, out char charFoodSelection))
            {
                foreach (Product food in shoppingMenu)
                {
                    if (food.name.StartsWith(userFoodSelection))
                    {
                        return true;
                    }                    
                }
            }
            else
            {
                return false;
            }            
           
            return false;
        }

    }


    public static double GetLineTotal(List<Product> shoppingMenu, string userFoodSelection, string userAmountSelection)
    {
        double lineTotal = 0;

        if (int.TryParse(userFoodSelection.ToString(), out int intFoodSelection))
        {
            lineTotal = (shoppingMenu[intFoodSelection].price * int.Parse(userAmountSelection));
            return lineTotal;
        }
        else
        {
            char.TryParse(userFoodSelection, out char charFoodSelection);

            foreach (Product food in shoppingMenu)
            {
                if (food.name.StartsWith(charFoodSelection))
                {
                    lineTotal = food.price * int.Parse(userAmountSelection);
                }
            }
            return lineTotal;

        }
    }

    public static bool AskUserToContinueAddingProducts()
    {
        bool keepLooping;
        do
        {
            Console.WriteLine("\nWould you like to Add More Items? (y/n)");
            var userInput = Console.ReadLine();

            if (userInput.Equals("y", StringComparison.OrdinalIgnoreCase) || userInput.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                keepLooping = false;
                return true;
            }
            else if (userInput.Equals("n", StringComparison.OrdinalIgnoreCase) || userInput.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                keepLooping = false;
                return false;
            }
            else
            {
                Console.WriteLine("Please Enter a Valid Response");
                keepLooping = true;
            }
        } while (keepLooping);
        return false;
    }

    public static void UpdateMenu()
    {
        var addItems = AskUserToAddMenuItems();
        bool keepGoing;
        
        do
        {            
            if (addItems)
            {
                Console.WriteLine("Please enter an item");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Please enter a category");
                string category = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Please enter a description");
                string description = Console.ReadLine();
                Console.WriteLine();

                string price;

                do
                {
                    Console.WriteLine("Please enter a price");
                    price = Console.ReadLine();

                    bool checkDub = Double.TryParse(price, out double doublePrice);

                    if (!checkDub)
                    {
                        Console.WriteLine("Not a valid entry. Try again.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("NEW LIST: ");
                        TextFile.WriteToTxt(name, category, description, doublePrice);
                        Console.WriteLine();

                        keepGoing = AskUserToContinueAddingProducts();
                        break;
                    }

                } while (true);

            }            
            else
            {
               break;
            }
        } while (keepGoing);
        Console.WriteLine("Thank you, goodbye");
    }


    public static bool AskUserToAddMenuItems()
    {
        bool keepGoing;
        do
        {
            Console.WriteLine("Would you like to add an item to our menu (y/n)?");
            string userChoice = Console.ReadLine();
            Console.WriteLine();

            if (userChoice.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else if (userChoice.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("That is not a valid entry, please try again");
                keepGoing = true;
            }
        } while (keepGoing);
        return false;

    }
}
