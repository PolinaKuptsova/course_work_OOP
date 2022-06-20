using System;
using Npgsql; 
using System.Collections.Generic;

public class SessionRepository
{
    private NpgsqlConnection connection;

    public SessionRepository(NpgsqlConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM sessions";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<Session> GetAll()
    {
        List<Session> sessions = new List<Session>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM sessions";
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            sessions.Add(GetSession(reader));
        }
        reader.Close();
        return sessions;
    }

    public List<Session> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<Session> Sessions = new List<Session>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM Sessions LIMIT @pageSize OFFSET @pageSize * (@pageNumber - 1)";
        command.Parameters.AddWithValue("@pageSize", pageSize);
        command.Parameters.AddWithValue("@pageNumber", pageNumber);
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Session Session = GetSession(reader);
            Sessions.Add(Session);
        }
        reader.Close();
        return Sessions;
    }

    public bool Update(long id, Session Session)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE Sessions SET has_avalibleseats = @has_avalibleseats, 
            type_of_screen = @type_of_screen,
         WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@has_avalibleseats", Session.has_avalibleSeats == false ? 1 : 0);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }

    public bool CancelSession(long id, bool isCanceled)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE Sessions SET is_canceled = @is_canceled
         WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@is_canceled", isCanceled == true ? 1 : 0);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }


    public Session GetById(long id)
    {
        Session Session = new Session();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM Sessions WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            Session = GetSession(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return Session;
    }

    public List<Session> GetMovieSessions(long movie_id)
    {
        List<Session> sessions = new List<Session>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM sessions WHERE movie_id = @movie_id";
        command.Parameters.AddWithValue("@movie_id", movie_id);
        NpgsqlDataReader reader  = command.ExecuteReader();
        while (reader.Read())
        {
            sessions.Add(GetSession(reader));
        }
        reader.Close();
        return sessions;
    }

    public int DeleteById(long id)
    {
        Session Session = new Session();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM Sessions WHERE id = @id";
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


    public int Insert(Session Session)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO Sessions (movie_id, hall_id, start_movie, end, has_avalibleseats, is_canceled) 
            VALUES (@movie_id, @hall_id, @start_movie, @has_avalibleseats, @is_canceled)
            RETURNING id;
            ";
        command.Parameters.AddWithValue("@hall_id", Session.hall_id);
        command.Parameters.AddWithValue("@movie_id", Session.movie_id);
        command.Parameters.AddWithValue("@start_movie", Session.start);
        command.Parameters.AddWithValue("@has_avalibleseats", Session.has_avalibleSeats == false ? 1 : 0);
        int newId = (int)command.ExecuteScalar();
        return newId;

    }

    public Session GetSessionByTime(DateTime movieTime)
    {
        Session session = new Session();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"Select FROM sessions WHERE start_movie = @movie_time";
        command.Parameters.AddWithValue("@movie_time", movieTime);
        NpgsqlDataReader reader  = command.ExecuteReader();
        if (reader.Read())
        {
            session = GetSession(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return session;
    }

    public List<Session> GetActualMovieSessions(long movie_id)
    {
        List<Session> sessions = new List<Session>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM sessions WHERE movie_id = @movie_id 
            AND start_movie >= @chosen_day";
        command.Parameters.AddWithValue("@movie_id", movie_id);
        command.Parameters.AddWithValue("@chosen_day", new DateTime(2022,06,16));
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Session session = GetSession(reader);
            sessions.Add(session);
        }
        reader.Close();
        return sessions;
    }
    public Session GetSession(NpgsqlDataReader reader )
    {
        Session Session = new Session();
        Session.id = reader.GetInt32(0);
        Session.hall_id = reader.GetInt32(2);
        Session.movie_id = reader.GetInt32(1);
        Session.start = reader.GetDateTime(3);
        Session.has_avalibleSeats = reader.GetInt32(4) == 1 ? false : true;
        Session.is_canceled = reader.GetInt32(5) == 1 ? false : true;

        return Session;
    }

}