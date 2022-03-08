using System;
public static class ConsoleApp
{
    private static void ShowInfoForCustomer()
    {
        Console.WriteLine("Welcome to the Movie Theater!\r\nPlease choose an operation:\r\n-log in\r\n-show billboard\r\n-exit\r\nFor more,please, log in!");
    }

    public static void Run(MovieTheater movieTheater)
    {
        ShowInfoForCustomer();
        Customer customer = null;
        while (true)
        {
            try
            {
                string command = Console.ReadLine();
                if (command.Trim() == "log in")
                {
                    ProcessLogIn(movieTheater.customerRepository);
                }
                else if (command.Trim() == "show billboard")
                {
                    ProcessShowBillboard();
                }
                else if (command.Trim() == "registrate")
                {
                    int result = ProcessRegistrate(movieTheater.customerRepository);
                    if (result != 0)
                    {
                        Console.WriteLine("You have been successufully added!");
                    }

                }
                if (customer != null)
                {
                    if (command.Trim() == "buy ticket")
                    {
                        break;
                    }
                    else if (command.Trim() == "show my ticket")
                    {
                        
                    }
                    else if (command.Trim() == "log out")
                    {
                        break;
                    }
                    else if (command.Trim() == "show my account")
                    {
                        break;
                    }
                    else if (command.Trim() == "update my account")
                    {
                        break;
                    }
                    else if (command.Trim() == "delete my account")
                    {
                        break;
                    }
                    else if (command.Trim() == "exit")
                    {
                        break;
                    }
                }
                else
                {
                    throw new Exception("Incorrect command! Try again!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    private static int ProcessRegistrate(CustomerRepository customerRepository)
    {
        Console.WriteLine("Please, enter info about yourself:");
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Customer customer = Authentication.ValidateUserName(name, customerRepository);
        if (customer != null)
        {
            throw new Exception("Such username '{name}' is already exists!");
        }

        Console.Write("\r\nPassword: ");
        string password = Console.ReadLine();

        Console.Write("\r\nPhone number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("\r\nAge: ");
        string ageInput = Console.ReadLine();

        if (!Int32.TryParse(ageInput, out int i))
        {
            throw new Exception($"Incorrect age '{ageInput}'. Please try again!");
        }
        int age = int.Parse(ageInput);
        if (age < 0 || ageInput.StartsWith('-'))
        {
            throw new Exception($"Incorrect age '{ageInput}'. Please try again!");
        }

        Customer newCustomer = new Customer(phoneNumber, age, name, password);
        return customerRepository.Insert(newCustomer);

    }

    private static void ProcessShowBillboard()
    {
        throw new NotImplementedException();
    }

    public static Customer ProcessLogIn(CustomerRepository customerRepository)
    {
        Console.WriteLine("Enter your name");
        string name = Console.ReadLine();
        Customer customer = Authentication.ValidateUserName(name, customerRepository);
        if (customer == null)
        {
            throw new Exception($"No such user with the username {name}");
        }

        Console.WriteLine("Enter your password");
        string password = Console.ReadLine();
        bool hashVerified = Authentication.VerifyHash(password, customer.Password);
        if (hashVerified)
        {
            return customer;
        }
        throw new Exception("Incorrect password. Please try again!");
    }


}
