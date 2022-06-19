using System;
using Npgsql; 
using System.Collections.Generic;

public class MovieRepository
{
    private NpgsqlConnection connection;

    public MovieRepository(NpgsqlConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM movies";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<Movie> GetAll()
    {
        List<Movie> movies = new List<Movie>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM movies";
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            movies.Add(GetMovie(reader));
        }
        reader.Close();
        return movies;
    }
    

    public List<Movie> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<Movie> movies = new List<Movie>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM movies LIMIT @pageSize OFFSET @pageSize * (@pageNumber - 1)";
        command.Parameters.AddWithValue("@pageSize", pageSize);
        command.Parameters.AddWithValue("@pageNumber", pageNumber);
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Movie Movie = GetMovie(reader);
            movies.Add(Movie);
        }
        reader.Close();
        return movies;
    }

    public bool Update(long id, Movie movie)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE movies SET title = @title, genre = @genre, director = @director, 
        duration = @duration, premiere = @premiere, last_day_on_screen = @last_day_on_screen, description = @description,
        age_range = @age_range
         WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@title", movie.title);
        command.Parameters.AddWithValue("@genre", movie.genre);
        command.Parameters.AddWithValue("@director", movie.director);
        command.Parameters.AddWithValue("@duration", movie.duration);
        command.Parameters.AddWithValue("@premiere", movie.premiere);
        command.Parameters.AddWithValue("@last_day_on_screen", movie.lastDayOnScreen);
        command.Parameters.AddWithValue("@description", movie.description);
        command.Parameters.AddWithValue("@age_range", movie.ageRange);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }

    public Movie GetById(long id)
    {
        Movie Movie = new Movie();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM movies WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            Movie = GetMovie(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return Movie;
    }

    public Movie GetMovieByTitle(string title)
    {
        Movie movie = new Movie();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM movies WHERE title = @title";
        command.Parameters.AddWithValue("@title", title);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            movie = GetMovie(reader);
        }
        reader.Close();
        return movie;
    }
    
    public int DeleteById(long id)
    {
        Movie Movie = new Movie();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM movies WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        int nChanged = command.ExecuteNonQuery();
        if (nChanged == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Insert(Movie movie)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO movies (title, genre, director, 
            premiere, last_day_on_screen, description, duration, age_range) 
            VALUES (@title, @genre, @director,
            @premiere, @last_day_on_screen, @description, @duration, @age_range)
            RETURNING id;
            ";
        command.Parameters.AddWithValue("@title", movie.title);
        command.Parameters.AddWithValue("@genre", movie.genre);
        command.Parameters.AddWithValue("@director", movie.director);
        command.Parameters.AddWithValue("@duration", movie.duration);
        command.Parameters.AddWithValue("@premiere", movie.premiere);
        command.Parameters.AddWithValue("@last_day_on_screen", movie.lastDayOnScreen);
        command.Parameters.AddWithValue("@description", movie.description);
        command.Parameters.AddWithValue("@age_range", movie.ageRange);

        
        int newId = (int)command.ExecuteScalar();
        return newId;
        
    }

    public List<Movie> GetAllAvalibleMovies()
    {
        List<Movie> movies = new List<Movie>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM movies WHERE @day BETWEEN premiere AND last_day_on_screen";
        command.Parameters.AddWithValue("@day", DateTime.Now);
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            movies.Add(GetMovie(reader));
        }
        reader.Close();
        return movies;
    }

    public Movie GetMovie(NpgsqlDataReader reader)
    {
        Movie movie = new Movie();
        movie.id = reader.GetInt32(0);
        movie.title = reader.GetString(1);
        movie.genre = reader.GetString(2);
        movie.director = reader.GetString(3);
        movie.duration = reader.GetDouble(7);
        movie.premiere = reader.GetDateTime(4);
        movie.lastDayOnScreen = reader.GetDateTime(5);
        movie.description = reader.GetString(6);
        movie.ageRange = reader.GetInt32(8);

        return movie;
    }
}