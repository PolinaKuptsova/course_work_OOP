using System;
using System.Collections.Generic;

public sealed class Admin : MovieAssistant
{

    public Admin()
    {
    }

    public Admin SetAdmin(Customer user)
    {
        return new Admin
        {
            id = user.id,
            Name = user.Name,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            isBlocked = user.isBlocked,
            accessLevel = user.accessLevel,
            age = user.age,
            Balance = user.Balance,
            CustomerState = this.CustomerState 
        };
    }

    public void AddMovieAssistant(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the name of a user: ");
        string userName = Console.ReadLine();
        List<Customer> users = movieTheaterComponents.userRepository.GetAll();
        Customer user = new Customer();
        if(users.Count > 0)
        {
            foreach(Customer us in users)
            {
                if(userName == us.Name)
                {
                    user = us;
                }
            }
        }
        if(user.id == 0){throw new Exception($"No such user '{userName}' Registrate first!");}

        bool isAdded = movieTheaterComponents.userRepository.UpdateUserAccessLevel(user.id, "moderator");
        if(isAdded)
        {
            Console.WriteLine($"New assistant {userName} has been added!");
        }
    }

    public void BlockUser(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the name of a customer you want to block/unblock");
        string userName = Console.ReadLine();
        List<Customer> users = movieTheaterComponents.userRepository.GetAll();
        Customer user = new Customer();
        if(users.Count > 0)
        {
            foreach(Customer us in users)
            {
                if(userName == us.Name)
                {
                    user = us;
                }
            }
        }

        Console.WriteLine("Enter 'true' if you want to block and 'false' if you want to unblock the user:");
        string newStatus = Console.ReadLine();
        bool user_status = bool.Parse(newStatus);

        if (user.id != 0)
        {
            bool res = movieTheaterComponents.userRepository.UpdateUserStatus(user.id, user_status);
            return;
        }
        throw new Exception("Incorrect name '{userName} or new status '{newStatus}'.'");
    }

    public void DeleteMovieAssistant(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the name of a movieassistant you want to delete");
        string assistName = Console.ReadLine();
        List<Customer> users = movieTheaterComponents.userRepository.GetAll();
        Customer assist = new Customer();
        if(users.Count > 0)
        {
            foreach(Customer us in users)
            {
                if(assistName == us.Name)
                {
                    assist = us;
                }
            }
        }

        if (assist.id != 0)
        {
            bool isAdded = movieTheaterComponents.userRepository.UpdateUserAccessLevel(assist.id, "customer");
            Console.WriteLine($"Assistant {assistName} has been deleted");
            return;
        }
        throw new Exception("$Incorrect name '{assistName}.'");

    }
}
