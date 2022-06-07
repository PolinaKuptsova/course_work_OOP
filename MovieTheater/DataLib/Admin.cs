using System;
public sealed class Admin : MovieAssistant
{
    public Admin(string phoneNumber, int age, string name, string password) : base(phoneNumber, age, name, password)
    {
    }

    public Admin()
    {
    }

    public Admin SetAdmin (Customer user)
    {
        // to do
        return new Admin{
            
        };
    }

    public void AddMovieAssistant(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the name of a user: ");
        string userName = Console.ReadLine();
        long user_id = movieTheaterComponents.userRepository.GetUserByName(userName);
        if(user_id == 0){throw new Exception($"No such user '{userName}'");}

        movieTheaterComponents.userRepository.UpdateUserAccessLevel(user_id, "moderator");
    }

    public void BlockUser(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the name of a customer you want to block/unblock");
        string userName = Console.ReadLine();
        long user_id = movieTheaterComponents.userRepository.GetUserByName(userName);

        Console.WriteLine("Enter 'true' if you want to block and 'false' if you want to unblock the user:");
        string newStatus = Console.ReadLine();
        bool user_status = bool.Parse(newStatus);

        if (user_id != 0)
        {
            bool res = movieTheaterComponents.userRepository.UpdateUserStatus(user_id, user_status);
            return;
        }
        throw new Exception("Incorrect name '{userName} or new status '{newStatus}'.'");
    }

    public void DeleteMovieAssistant(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the name of a movieassistant you want to delete");
        string assistName = Console.ReadLine();
        long assist_id = movieTheaterComponents.userRepository.GetUserByName(assistName);

        if (assist_id != 0)
        {
            int res = movieTheaterComponents.userRepository.DeleteById(assist_id);
            return;
        }
        throw new Exception("$Incorrect name '{assistName}.'");

    }
}
