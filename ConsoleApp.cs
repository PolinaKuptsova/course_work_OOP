using System;
public static class ConsoleApp
{
    private static void ShowInfoForCustomer()
    {
        Console.WriteLine("Welcome to the Movie Theater!\r\nPlease choose an operation:\r\n-log in\r\n-show billboard\r\n-show my ticket\r\nFor more,please, log in!");
    }

    public static void Run()
    {
        ShowInfoForCustomer();
        Customer customer = null;
        while(true)
        {
            string command = Console.ReadLine();
            switch(command)
            {
               case "log in":
               {
                   // DoLogIn();
                   break;
               } 
               case "show billboard":
               {
                   break;
               }
               case "show my ticket":
               {
                   break;
               }


            }

        }

    }
}
