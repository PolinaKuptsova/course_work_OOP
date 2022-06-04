using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class MovieHallRepository
{
    private SqliteConnection connection;

    public MovieHallRepository(SqliteConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM moviehalls";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<MovieHall> GetAll()
    {
        List<MovieHall> moviehalls = new List<MovieHall>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM moviehalls";
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            moviehalls.Add(Getmoviehall(reader));
        }
        reader.Close();
        return moviehalls;
    }

    public List<MovieHall> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<MovieHall> moviehalls = new List<MovieHall>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM moviehalls LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
        command.Parameters.AddWithValue("$pageSize", pageSize);
        command.Parameters.AddWithValue("$pageNumber", pageNumber);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            MovieHall moviehall = Getmoviehall(reader);
            moviehalls.Add(moviehall);
        }
        reader.Close();
        return moviehalls;
    }

    public bool Update(long id, MovieHall moviehall)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE moviehalls SET has_avalibleSeats = $has_avalibleSeats, 
            typeOfScreen = $typeOfScreen,
         WHERE hall_id = $hall_id";
        command.Parameters.AddWithValue("$hall_id", id);
        command.Parameters.AddWithValue("$typeOfScreen", moviehall.typeOfScreen);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }


    public MovieHall GetById(long id)
    {
        MovieHall moviehall = new MovieHall();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM moviehalls WHERE hall_id = $hall_id";
        command.Parameters.AddWithValue("$hall_id", id);
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            moviehall = Getmoviehall(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return moviehall;
    }

    public int DeleteById(long id)
    {
        MovieHall moviehall = new MovieHall();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM moviehalls WHERE hall_id = $hall_id";
        command.Parameters.AddWithValue("$hall_id", id);
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
    public int Insert(MovieHall moviehall)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO moviehalls (typeOfScreen, has_avalibleSeats) 
            VALUES ($typeOfScreen, $has_avalibleSeats);
            
            SELECT last_insert_rowid();
            ";
        command.Parameters.AddWithValue("$hall_id", moviehall.hall_id);
        command.Parameters.AddWithValue("$typeOfScreen", moviehall.typeOfScreen);
        long newId = (long)command.ExecuteScalar();
        if (newId == 0)
        {
            return 0;
        }
        else
        {
            return (int)newId; ;
        }

    }

    public MovieHall Getmoviehall(SqliteDataReader reader)
    {
        MovieHall moviehall = new MovieHall();
        moviehall.hall_id = long.Parse(reader.GetString(0));
        moviehall.typeOfScreen = reader.GetString(1);

        return moviehall;
    }
}