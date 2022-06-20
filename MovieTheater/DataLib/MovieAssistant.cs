using System;
using System.Collections.Generic;

public class MovieAssistant : Customer
{
    public delegate void CallbackMovieAdding(Movie movie);
    public delegate void CallbackSessionCanceling(Session session);
    public event CallbackMovieAdding NotifyMovieAdding;
    public event CallbackSessionCanceling NotifySessionCanceling;
    public MovieAssistant()
    {
        // CallbackMovieAdding movieAddingEvent = NotifyMovieAdding;
        // CallbackSessionCanceling cancelSessionEvent = NotifySessionCanceling;
    }

    public MovieAssistant SetMovieAssistant(Customer user)
    {
        return new MovieAssistant{
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


    // add sessions ??
    public void AddMovie(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter movie title: ");
        string title = "newMovie3";//Console.ReadLine();

        List<Movie> allMovies = movieTheaterComponents.movieRepository.GetAll();
        if(allMovies.Count > 0)
        {
            foreach(Movie m in allMovies)
            {
                if(title == m.title)
                {
                    throw new Exception($"Movie with such title {title} has been already added!");
                }
            }
        }

        Console.WriteLine("Enter movie genre: ");
        string genre = "drama";//Console.ReadLine();
        Console.WriteLine("Enter movie director: ");
        string director = "dgsdg";//Console.ReadLine();
        Console.WriteLine("Enter movie duration: ");
        string durationStr = "148";//Console.ReadLine();
        Console.WriteLine("Enter movie premiere: ");
        string premiereStr = "2022/10/09";//Console.ReadLine();
        Console.WriteLine("Enter movie last day on screen: ");
        string lastDayOnScreenStr = "2022/11/19";//Console.ReadLine();
        Console.WriteLine("Enter movie description: ");
        string description = "nfsefs";//Console.ReadLine();
        Console.WriteLine("Enter movie age: ");
        string ageStr = "14";//Console.ReadLine();

        Movie newMovie = new Movie{
            title = title,
            genre = genre,
            director = director,
            duration = int.Parse(durationStr),
            premiere = DateTime.Parse(premiereStr),
            lastDayOnScreen = DateTime.Parse(lastDayOnScreenStr),
            description = description,
            ageRange = int.Parse(ageStr)
        };

        long id = movieTheaterComponents.movieRepository.Insert(newMovie);

        if (this.NotifyMovieAdding != null)
        {
            Console.WriteLine("Notified");
            NotifyMovieAdding.Invoke(newMovie);
        }
    }


    public void DeleteMovie(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        Movie movie = movieTheaterComponents.movieRepository.GetMovieByTitle(title);
        if (movie == null) { throw new Exception($"No such film '{title}'!"); }

        List<Session> sessions = movieTheaterComponents.sessionRepository.GetMovieSessions(movie.id);

        foreach(Session sn in sessions){
            int isDeletedSession = movieTheaterComponents.sessionRepository.DeleteById(sn.id);
        }
        int isDeletedMovie = movieTheaterComponents.movieRepository.DeleteById(movie.id);
        if(isDeletedMovie == 1)
        {
            Console.WriteLine("Deleted succesessfully!");
        }
    }

    public void CancelSession(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        Movie movie = movieTheaterComponents.movieRepository.GetMovieByTitle(title);
        if(movie == null){throw new Exception($"No such film '{title}'!");}

        List<Session> sessions = movieTheaterComponents.sessionRepository.GetMovieSessions(movie.id);

        if (sessions.Count == 0)
        {
            throw new Exception("No sessions yet!");
        }

        foreach (Session sn in sessions)
        {
            Console.WriteLine(sn);
        }
        Console.WriteLine("Enter the number of session:");
        string session_id = Console.ReadLine();

        Session session = movieTheaterComponents.sessionRepository.GetById(int.Parse(session_id));    
        if(session == null){throw new Exception("Incorrect session number!");}

        Console.WriteLine($"If you want to cacncel session (#{session.id}) type 'true', in other way type 'false': ");
        string isCanceled = Console.ReadLine();
        bool res = movieTheaterComponents.sessionRepository.CancelSession(session.id, bool.Parse(isCanceled));
        
        if (this.NotifySessionCanceling != null)
        {
            Console.WriteLine("Notified");
            NotifySessionCanceling(session);
        }
    }

    public void GetAllCustomers(MovieTheaterComponents movieTheaterComponents)
    {
        List<Customer> customers = movieTheaterComponents.userRepository.GetAllByAccessLevel("customer");
        if(customers.Count == 0){throw new System.Exception ("No customers yet!");}
        foreach(Customer c in customers)
        {
            Console.WriteLine(c);
        }
    }

    public void GetAllMovies(MovieTheaterComponents movieTheaterComponents)
    {
        List<Movie> movies = movieTheaterComponents.movieRepository.GetAll();
        if(movies.Count == 0){throw new System.Exception ("No movies yet!");}
        foreach(Movie m in movies)
        {
            Console.WriteLine(m);
        }
    }

}
