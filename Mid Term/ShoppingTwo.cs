using Mid_Term;
using System;
using System.Collections.Generic;

class ShoppingTwo
{

    //This is the User Input Method
    public static void GetItemFromUser(List<Product> shoppingMenu)
    {
        bool invalidInput;
        bool continueAddingProducts;
        double grandTotal = 0;
        Dictionary<Product, double> userCheckoutList = new Dictionary<Product, double>();
        do
        {
            double lineTotal = 0;
            do
            {
                TextFile.OutputTxtFile();
                Console.WriteLine("Please select the product you wish to buy by typing in the position in which the product is in the list (1,2,3,4.. etc) or by typing in the first letter of the item name");
                var userFoodSelection = char.Parse(Console.ReadLine());


                Console.WriteLine($"Please type in the amount of {userFoodSelection} you wish to purchase");
                var userAmountSelection = Console.ReadLine();

                if (ValidateFoodInput(shoppingMenu, userFoodSelection, userAmountSelection) && ValidateQuantityInput(userAmountSelection))
                {
                    invalidInput = false;
                    lineTotal = GetLineTotal(shoppingMenu, userFoodSelection, userAmountSelection);
                    userCheckoutList.Add(GenerateUserPickAsProduct(shoppingMenu, userFoodSelection), lineTotal);
                }
                else
                {
                    Console.WriteLine("Please Try Again");
                    invalidInput = true;
                }
            } while (invalidInput);

            grandTotal = grandTotal + lineTotal;

            continueAddingProducts = AskUserToContinueAddingProducts();
        } while (continueAddingProducts);


        //Call The Checkout Method
        CheckoutCartForUser(userCheckoutList, grandTotal);


        //Call The Receipt Method using the same input variables as the checkout method.
        //(userCheckoutList, grandTotal)
    }

    public static void CheckoutCartForUser(Dictionary<Product, double> userCheckoutList, double grandTotal)
    {
        bool keepLooping = true;
        bool isCashCorrect = false;
        do
        {
            do
            {
                Console.WriteLine("How Would You Like To Pay? \n Cash \n Credit \n Check\n");
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
        if (double.TryParse(userCash, out double userCashDouble) && (userCashDouble >= grandTotal))
        {
            return false;
        }
        else
        {
            Console.WriteLine("This Does Not Work, Please Try Again");
            return true;
        }
    }

    public static Product GenerateUserPickAsProduct(List<Product> shoppingMenu, char userFoodSelection)
    {
        if (int.TryParse(userFoodSelection.ToString(), out int intFoodSelection))
        {
            return shoppingMenu[intFoodSelection];
        }
        else
        {
            foreach (Product food in shoppingMenu)
            {
                if (food.name.StartsWith(userFoodSelection))
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

    public static bool ValidateFoodInput(List<Product> shoppingMenu, char userFoodSelection, string userAmountSelection)
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
            foreach (Product food in shoppingMenu)
            {
                if (food.name.StartsWith(userFoodSelection))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

    }


    public static double GetLineTotal(List<Product> shoppingMenu, char userFoodSelection, string userAmountSelection)
    {
        double lineTotal = 0;

        if (int.TryParse(userFoodSelection.ToString(), out int intFoodSelection))
        {
            lineTotal = (shoppingMenu[userFoodSelection].price * int.Parse(userAmountSelection));
            return lineTotal;
        }
        else
        {
            foreach (Product food in shoppingMenu)
            {
                if (food.name.StartsWith(userFoodSelection))
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
            Console.WriteLine("Would you like to Add More Items? (y/n");
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
}
