using System;
using System.Collections.Generic;

public class MovieAssistant : Customer
{
    public MovieAssistant(string phoneNumber, int age, string name, string password) : base(phoneNumber, age, name, password)
    {
    }

    public MovieAssistant()
    {
        
    }

    public MovieAssistant SetMovieAssistant(Customer user)
    {
        return new MovieAssistant{
            
        };
    }


    // add sessions ??
    public void AddMovie(MovieTheater movieTheater)
    {
        Console.WriteLine("Enter movie title: ");
        string title = Console.ReadLine();
        Console.WriteLine("Enter movie genre: ");
        string genre = Console.ReadLine();
        Console.WriteLine("Enter movie derector: ");
        string director = Console.ReadLine();
        Console.WriteLine("Enter movie duration: ");
        string durationStr = Console.ReadLine();
        Console.WriteLine("Enter movie premiere: ");
        string premiereStr = Console.ReadLine();
        Console.WriteLine("Enter movie last day on screen: ");
        string lastDayOnScreenStr = Console.ReadLine();
        Console.WriteLine("Enter movie description: ");
        string description = Console.ReadLine();
        Console.WriteLine("Enter movie age: ");
        string ageStr = Console.ReadLine();

        Movie newMovie = new Movie{
            title = title,
            genre = genre,
            director = director,
            duration = double.Parse(durationStr),
            premiere = DateTime.Parse(premiereStr),
            lastDayOnScreen= DateTime.Parse(lastDayOnScreenStr),
            description = description, 
            ageRange = int.Parse(ageStr)
        };
    }

    public void DeleteMovie(MovieTheater movieTheater)
    {
        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        long movie_id = movieTheater.movieRepository.GetMovieByTitle(title);
        if (movie_id == 0) { throw new Exception($"No such film '{title}'!"); }

        List<Session> sessions = movieTheater.sessionRepository.GetMovieSessions(movie_id);

        foreach(Session sn in sessions){
            int isDeletedSession = movieTheater.sessionRepository.DeleteById(sn.id);
        }
        int isDeletedMovie = movieTheater.movieRepository.DeleteById(movie_id);
    }

    public void CancelSession(MovieTheater movieTheater)
    {
        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        long movie_id = movieTheater.movieRepository.GetMovieByTitle(title);
        if(movie_id == 0){throw new Exception($"No such film '{title}'!");}

        List<Session> sessions = movieTheater.sessionRepository.GetMovieSessions(movie_id);
        foreach (Session sn in sessions)
        {
            Console.WriteLine(sn);
        }
        Console.WriteLine("Enter the number of session:");
        string session_id = Console.ReadLine();

        Session session = movieTheater.sessionRepository.GetById(int.Parse(session_id));    
        if(session == null){throw new Exception("Incorrect session number!");}

        Console.WriteLine($"If you want to cacncel session (#{session.id}) type 'true', in other way type 'false': ");
        string isCanceled = Console.ReadLine();
        bool res = movieTheater.sessionRepository.CancelSession(session.id, bool.Parse(isCanceled));
    }    
    
    public void GetAllCustomers(MovieTheater movieTheater)
    {
        List<Customer> customers = movieTheater.userRepository.GetAllCustomers();
        if(customers == null){throw new System.Exception ("No customers yet!");}
        foreach(Customer c in customers)
        {
            Console.WriteLine(c);
        }
    }

    public void GetAllMovies(MovieTheater movieTheater)
    {
        List<Movie> movies = movieTheater.movieRepository.GetAll();
        if(movies == null){throw new System.Exception ("No movies yet!");}
        foreach(Movie m in movies)
        {
            Console.WriteLine(m);
        }
    }

}
