using System;
using System.Collections.Generic;

/* all commands :
user -      ShowMyTickets
            BuyTicket
            UpdateMyAccount
            DeleteMyAccount
            ShowMyAccount
            Exit
            Log out

assist -    AddMovie
            DeleteMovie
            CancelSession
            GetAllCustomers
            GetAllMovies
            Add hall

admin -     BlockUser
            AddCinemaAssistant 
            DeleteCinemaAssistant            
*/
public static class ConsoleApp{

    public static void Run(AbstrMovieTheater processControl)
    {
        while (true)
        {
            bool found = false;
            Console.WriteLine("Enter command: ");
            string command = Console.ReadLine();
            for (int i = 0; i < processControl.allCommands.Length; i++)
            {
                if (command == processControl.allCommands[i])
                {
                    found = true;
                    processControl.allProcesses[i].Invoke();
                    break;
                }
            }
            if (found == false)
            {
                Console.WriteLine("Incorrect command. Try again!");
            }
        }
    }
}