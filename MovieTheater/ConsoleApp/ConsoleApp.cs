using System;
using System.Collections.Generic;

/* all commands :
user -      ShowMyTickets
            BuyTicket
            UpdateMyAccount
            DeleteMyAccount
            ShowMyAccount
            Exit

assist -    AddMovie
            DeleteMovie
            CancelSession
            GetAllCustomers
            GetAllMovies

admin -     BlockUser
            AddCinemaAssistant // that exist change their accessLevel
            DeleteCinemaAssistant            
*/
public static class ConsoleApp{

    public static void Run(MovieTheater movieTheater)
    {
        while (true)
        {
            Console.WriteLine("Enter command: ");
            string command = Console.ReadLine();
            for (int i = 0; i < movieTheater.allCommands.Length; i++)
            {
                if (command == movieTheater.allCommands[i])
                {
                    movieTheater.allProcesses[i].Invoke();
                }
                else{
                    Console.WriteLine("Incorrect command. Try again!");
                }
            }
        }
    }
}