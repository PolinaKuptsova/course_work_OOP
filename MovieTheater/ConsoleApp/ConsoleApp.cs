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
        LogData log = new LogData(DateTime.Now);
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
                    log.commands.Add(command);
                    processControl.allProcesses[i].Invoke();
                    if(processControl.errorMessage != null){
                    log.errorMessages.Add(processControl.errorMessage); 
                    processControl.errorMessage = null;}
                    break;
                }
            }
            if (found == false)
            {
                Console.WriteLine("Incorrect command. Try again!");
            }
            if (command == "exit")
            {
                log.runDuration = log.launchTime - DateTime.Now;
                log.Export(@"/home/polina/course_work_OOP/Logs/log_" + DateTime.Now.ToString("o"));
                break;
            }
        }
    }
}