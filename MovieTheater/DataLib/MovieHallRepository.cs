using System;
using Npgsql; 
using System.Collections.Generic;

public class MovieHallRepository
{
    private NpgsqlConnection connection;

    public MovieHallRepository(NpgsqlConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        NpgsqlCommand command = connection.CreateCommand();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM moviehalls";
        NpgsqlDataReader reader = command.ExecuteReader();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM moviehalls LIMIT @pageSize OFFSET @pageSize * (@pageNumber - 1)";
        command.Parameters.AddWithValue("@pageSize", pageSize);
        command.Parameters.AddWithValue("@pageNumber", pageNumber);
        NpgsqlDataReader reader = command.ExecuteReader();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE moviehalls SET type_of_screen = @type_of_screen, row_amount = @row_amount,
         places_in_row_amount = @places_in_row_amount
         WHERE hall_id = @hall_id";
        command.Parameters.AddWithValue("@hall_id", id);
        command.Parameters.AddWithValue("@type_of_screen", moviehall.typeOfScreen);
        command.Parameters.AddWithValue("@row_amount", moviehall.rowAmount);
        command.Parameters.AddWithValue("@places_in_row_amount", moviehall.placesInRowAmount);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }


    public MovieHall GetById(long id)
    {
        MovieHall moviehall = new MovieHall();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM moviehalls WHERE hall_id = @hall_id";
        command.Parameters.AddWithValue("@hall_id", id);
        NpgsqlDataReader reader = command.ExecuteReader();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM moviehalls WHERE hall_id = @hall_id";
        command.Parameters.AddWithValue("@hall_id", id);
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO moviehalls (type_of_screen, row_amount, places_in_row_amount) 
            VALUES (@type_of_screen, @row_amount, @places_in_row_amount)
            RETURNING id";
        command.Parameters.AddWithValue("@hall_id", moviehall.hall_id);
        command.Parameters.AddWithValue("@type_of_screen", moviehall.typeOfScreen);
        command.Parameters.AddWithValue("@row_amount", moviehall.rowAmount);
        command.Parameters.AddWithValue("@places_in_row_amount", moviehall.placesInRowAmount);
        int newId = (int)command.ExecuteScalar();
        return newId;

    }

    public MovieHall Getmoviehall(NpgsqlDataReader reader)
    {
        MovieHall moviehall = new MovieHall();
        moviehall.hall_id = reader.GetInt32(0);
        moviehall.typeOfScreen = reader.GetString(1);
        moviehall.rowAmount = reader.GetInt32(2);
        moviehall.placesInRowAmount = reader.GetInt32(3);

        return moviehall;
    }
}