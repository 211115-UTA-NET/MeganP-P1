using System.Data.SqlClient;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Client.UI;
using Client.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Client.UI.Dtos; 
using Client.UI.Logic;


Console.WriteLine("Welcome to the Magic Store!");
Console.WriteLine("Please Enter Your First Name");
string? firstName;
string? lastName;
string? password;
Customer person;
LoadService loadService = new LoadService();
StoreService storeService = new StoreService(); 
CustomerLogic customerLogic = new CustomerLogic();
StoreLogic storeLogic;
try {
    firstName = Console.ReadLine().ToLower();
} catch (NullReferenceException nre) {
    Console.WriteLine("You didn't enter proper input.");
    return;
}
Console.WriteLine("Please Enter Your Last Name");
try {
    lastName = Console.ReadLine().ToLower();
} catch (NullReferenceException nre) {
    Console.WriteLine("You didn't enter proper input.");
    return;
}

if (firstName == "megan" && lastName == "postlewait") {
    Console.WriteLine("Enter Your Password:");
    string passWord = Console.ReadLine();
    if (passWord != "secretPassword") {
        Console.WriteLine("Incorrect Password");
        return;
    }
    await loadService.StoreLoadOrdersAsync();
    return;
}
bool doesExist = false;
try {
    doesExist = await loadService.NewOrReturning(firstName, lastName);
} catch (HttpRequestException hre) {
    Console.WriteLine("Server Connection Refused");
}

if (doesExist) {
    bool isValid = false;
    person = await loadService.CustomerLoadServiceAsync(firstName, lastName);
    while (!isValid) {
        
        Console.WriteLine("Please Enter Password");

        try {
            password = Console.ReadLine();
        } catch (NullReferenceException nre) {
            Console.WriteLine("You didn't enter proper input.");
            return;
        }

        isValid = customerLogic.ValidatePassword(person.Password, password);
        if (!isValid) {
            Console.WriteLine("Incorrect Password; Try Again.");
        }
    }
    Console.WriteLine();
} else {
    Console.WriteLine("Welcome New Shopper");
    Console.WriteLine("Let's Set up Your Account");
    Console.WriteLine("Please Enter The Password You'd Like for your account");

    try {
        password = Console.ReadLine();
    } catch (NullReferenceException nre) {
        Console.WriteLine("You didn't enter proper input.");
        return;
    }

    int storeID = await storeService.GetStoreId();

    await customerLogic.PostNewCustomer(firstName, lastName, password, storeID);
    person = await loadService.CustomerLoadServiceAsync(firstName, lastName);
    
    Console.WriteLine();
}

storeLogic = new StoreLogic(person.Store);

bool areShopping = true;

while (areShopping) {

    Console.WriteLine("Here are the items availible at your store location...");
    Console.WriteLine("Enter the number of the item you want then enter the quantity.");
    storeLogic.PrintInventory();
    Console.WriteLine("Enter 'check out' to Check Out");
    Console.WriteLine("Enter 'order history' to see your comprhensive order history");
    Console.WriteLine("Enter the Item Number you wish to add to you cart.");
    try {
        string option = Console.ReadLine().ToLower();
        if (option == "check out") {
            bool success = await customerLogic.MakePurchase(person.Store.Id, person);
            if (success) {
                Console.WriteLine("Purchase Successful, Come Again Soon. Goodbye.");
                break;
            } else {
                Console.WriteLine("Purchase Unsuccessful, Come Again Soon. Goodbye.");
                break;
            }

        } else if (option == "order history") {
            Console.WriteLine();
            loadService.CustomerLoadOrdersAsync(person.Id);
            Console.WriteLine();
        } else if (int.TryParse(option, out int m)){
            Console.WriteLine("Enter the number that you want of that product.");
            int option2 = Convert.ToInt32(Console.ReadLine());
            int items = customerLogic.AddToCart(person.Store.Inventory[Convert.ToInt32(option)], option2);
            Console.WriteLine();
            if (items > 0) {
                customerLogic.PrintCart();
            }
            Console.WriteLine();
        }
    } catch (NullReferenceException nre) {
        Console.WriteLine("You entered faulty input.");
        return;
    }
}


