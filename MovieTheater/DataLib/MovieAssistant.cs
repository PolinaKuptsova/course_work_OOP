using System;
using System.Collections.Generic;

public class MovieAssistant : Customer
{
    public delegate void CallbackMovieAdding(Movie movie);
    public delegate void CallbackSessionCanceling(Session session);
    public event CallbackMovieAdding NotifyMovieAdding;
    public event CallbackSessionCanceling NotifySessionCanceling;
    public MovieAssistant() { }

    public MovieAssistant SetMovieAssistant(Customer user)
    {
        return new MovieAssistant
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

    public void AddMovieHall(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the type of screen: ");
        string screenType = Console.ReadLine();
        Console.WriteLine("Enter the amount of rows: ");
        string rowAmountStr = Console.ReadLine();
        Console.WriteLine("Enter the amount of places in a row: ");
        string placesAmountStr = Console.ReadLine();

        MovieHall newHall = new MovieHall(screenType, int.Parse(rowAmountStr), int.Parse(placesAmountStr));
        int id = movieTheaterComponents.movieHallRepository.Insert(newHall);
        if (id != 0)
        {
            newHall.hall_id = id;
            Console.WriteLine("Hall was added!\r\nDo you want to improve it/ 'Yes/No'");
            string improving = Console.ReadLine();
            if (improving == "Yes")
            {
                ImproveMovieHall(newHall, movieTheaterComponents);
            }
            return;
        }
    }

    public void ImproveMovieHall(MovieHall hall, MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Do you want to install special lightning? 'Yes/No'");
        string specialLightningResponse = Console.ReadLine();
        if (specialLightningResponse == "Yes")
        {
            Room movieHall = hall;
            Console.WriteLine("Enter the type of lightning: ");
            string specialLightningType = Console.ReadLine();
            Decorator specialLightning = new SpecialLightingDecorator(specialLightningType);
            bool isUpdated = movieTheaterComponents.movieHallRepository.UpdateLightning(hall.hall_id, specialLightningType);
            if (isUpdated)
            {
                specialLightning.Show_Info();
            }
        }
        Console.WriteLine("Do you want to install additional audio system? 'Yes/No'");
        string additionAudioResponse = Console.ReadLine();
        if (additionAudioResponse == "Yes")
        {
            Room movieHall = hall;
            Console.WriteLine("Enter the type of audio system: ");
            string audioType = Console.ReadLine();
            Decorator additionalAudio = new AdditionalAudioSystemDecorator(audioType);
            bool isUpdated = movieTheaterComponents.movieHallRepository.UpdateAudioSystem(hall.hall_id, audioType);
            if (isUpdated)
            {
                additionalAudio.Show_Info();
            }
        }
        Console.WriteLine("Do you want to add additional seats? 'Yes/No'");
        string additionSeatsResponse = Console.ReadLine();
        if (additionAudioResponse == "Yes")
        {
            Room movieHall = hall;
            Console.WriteLine("Enter the amount of additional seats: ");
            string seatsAmount = Console.ReadLine();
            Decorator additionalSeats = new AdditionalSeatsDecorator(int.Parse(seatsAmount));
            bool isUpdated = movieTheaterComponents.movieHallRepository.UpdateAdditionalSeats(hall.hall_id, int.Parse(seatsAmount));
            if (isUpdated)
            {
                additionalSeats.Show_Info();
            }
        }

    }

    // add sessions ??
    public void AddMovie(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter movie title: ");
        string title = Console.ReadLine();

        List<Movie> allMovies = movieTheaterComponents.movieRepository.GetAll();
        if (allMovies.Count > 0)
        {
            foreach (Movie m in allMovies)
            {
                if (title == m.title)
                {
                    throw new Exception($"Movie with such title {title} has been already added!");
                }
            }
        }

        Console.WriteLine("Enter movie genre: ");
        string genre = Console.ReadLine();
        Console.WriteLine("Enter movie director: ");
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

        Movie newMovie = new Movie
        {
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

        if (this.NotifyMovieAdding != null) { NotifyMovieAdding.Invoke(newMovie); }
    }

    public void DeleteMovie(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        Movie movie = movieTheaterComponents.movieRepository.GetMovieByTitle(title);
        if (movie == null) { throw new Exception($"No such film '{title}'!"); }

        List<Session> sessions = movieTheaterComponents.sessionRepository.GetMovieSessions(movie.id);

        foreach (Session sn in sessions)
        {
            int isDeletedSession = movieTheaterComponents.sessionRepository.DeleteById(sn.id);
        }
        int isDeletedMovie = movieTheaterComponents.movieRepository.DeleteById(movie.id);
        if (isDeletedMovie == 1)
        {
            Console.WriteLine("Deleted succesessfully!");
        }
    }

    public void CancelSession(MovieTheaterComponents movieTheaterComponents)
    {
        Console.WriteLine("Enter the movie title:");
        string title = Console.ReadLine();
        Movie movie = movieTheaterComponents.movieRepository.GetMovieByTitle(title);
        if (movie == null) { throw new Exception($"No such film '{title}'!"); }

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
        if (session == null) { throw new Exception("Incorrect session number!"); }

        Console.WriteLine($"If you want to cacncel session (#{session.id}) type 'true', in other way type 'false': ");
        string isCanceled = Console.ReadLine();
        bool res = movieTheaterComponents.sessionRepository.CancelSession(session.id, bool.Parse(isCanceled));

        // GetSubscribersForSessionCanceling(session, movieTheaterComponents);
        // if (this.NotifySessionCanceling != null) { NotifySessionCanceling.Invoke(session); }
    }

    public void GetAllCustomers(MovieTheaterComponents movieTheaterComponents)
    {
        List<Customer> customers = movieTheaterComponents.userRepository.GetAllByAccessLevel("customer");
        if (customers.Count == 0) { throw new System.Exception("No customers yet!"); }
        foreach (Customer c in customers)
        {
            Console.WriteLine(c);
        }
    }

    public void GetAllMovies(MovieTheaterComponents movieTheaterComponents)
    {
        List<Movie> movies = movieTheaterComponents.movieRepository.GetAll();
        if (movies.Count == 0) { throw new System.Exception("No movies yet!"); }
        foreach (Movie m in movies)
        {
            Console.WriteLine(m);
        }
    }

}
