using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class SessionRepository
{
    private SqliteConnection connection;

    public SessionRepository(SqliteConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        SqliteCommand command = connection.CreateCommand();
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
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM sessions";
        SqliteDataReader reader = command.ExecuteReader();
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
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM Sessions LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
        command.Parameters.AddWithValue("$pageSize", pageSize);
        command.Parameters.AddWithValue("$pageNumber", pageNumber);
        SqliteDataReader reader = command.ExecuteReader();
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
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE Sessions SET has_avalibleSeats = $has_avalibleSeats, 
            typeOfScreen = $typeOfScreen,
         WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$has_avalibleSeats", Session.has_avalibleSeats == false ? 1 : 0);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }

    public bool CancelSession(long id, bool isCanceled)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE Sessions SET isCanaceled = $isCanceled,
         WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$isCanceled", isCanceled);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }


    public Session GetById(long id)
    {
        Session Session = new Session();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM Sessions WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        SqliteDataReader reader = command.ExecuteReader();
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
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM sessions WHERE movie_id = $movie_id";
        command.Parameters.AddWithValue("$movie_id", movie_id);
        SqliteDataReader reader = command.ExecuteReader();
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
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM Sessions WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
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
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO Sessions (movie_id, hall_id, start, end, has_avalibleSeats) 
            VALUES ($movie_id, $hall_id, $start, $end, $has_avalibleSeats);
            
            SELECT last_insert_rowid();
            ";
        command.Parameters.AddWithValue("$hall_id", Session.hall_id);
        command.Parameters.AddWithValue("$movie_id", Session.movie_id);
        command.Parameters.AddWithValue("$start", Session.start.ToString("0"));
        command.Parameters.AddWithValue("$end", Session.end.ToString("0"));
        command.Parameters.AddWithValue("$has_avalibleSeats", Session.has_avalibleSeats == false ? 1 : 0);
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


    public Session GetSession(SqliteDataReader reader)
    {
        Session Session = new Session();
        Session.id = long.Parse(reader.GetString(0));
        Session.hall_id = long.Parse(reader.GetString(1));
        Session.movie_id = long.Parse(reader.GetString(2));
        Session.start = DateTime.Parse(reader.GetString(1));
        Session.end = DateTime.Parse(reader.GetString(2));
        Session.has_avalibleSeats = reader.GetString(5) == "1" ? false : true;

        return Session;
    }

    public Session GetSessionByTime(DateTime movieTime)
    {
        Session session = new Session();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"Select FROM sessions WHERE start = $movieTime";
        command.Parameters.AddWithValue("$movieTime", movieTime);
        SqliteDataReader reader = command.ExecuteReader();
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

    // needs checking
    public List<Session> GetMovieSessionsOnDay(long movie_id, DateTime chosenDay)
    {
        List<Session> sessions = new List<Session>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM sessions WHERE movie_id = $movie_id 
            AND start = $chosenDay";
        command.Parameters.AddWithValue("$movie_id", movie_id);
        command.Parameters.AddWithValue("$chosenDay", chosenDay);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Session session = GetSession(reader);
            sessions.Add(session);
        }
        reader.Close();
        return sessions;
    }

}