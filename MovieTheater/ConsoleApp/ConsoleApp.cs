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

    public static void Run(AbstrMovieTheater processControl)
    {
        string [] c = new string []{"log in", "add movie"};

        //while (true)
        for(int y = 0 ; y < c.Length; y++)
        {
            bool found = false;
            Console.WriteLine("Enter command: ");
            string command = c[y];//Console.ReadLine();
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