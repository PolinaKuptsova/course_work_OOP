using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class TicketRepository
{
    private SqliteConnection connection;

    public TicketRepository (SqliteConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM tickets";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<Ticket> GetAll()
    {
        List<Ticket> tickets = new List<Ticket>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM tickets";
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            tickets.Add(GetTicket(reader));
        }
        reader.Close();
        return tickets;
    }

    public List<Ticket> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<Ticket> tickets = new List<Ticket>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM tickets LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
        command.Parameters.AddWithValue("$pageSize", pageSize);
        command.Parameters.AddWithValue("$pageNumber", pageNumber);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Ticket ticket = GetTicket(reader);
            tickets.Add(ticket);
        }
        reader.Close();
        return tickets;
    }

    internal List<Ticket> GetUserTickets(long id)
    {
        throw new NotImplementedException();
    }

    public bool Update(long id, Ticket ticket)
       {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE tickets SET hallNumber = $hallNumber
         WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$movieId", ticket.movieId);
        command.Parameters.AddWithValue("$ticketNumber", ticket.ticketNumber);
        command.Parameters.AddWithValue("$place", ticket.place);
        command.Parameters.AddWithValue("$row", ticket.row);
        command.Parameters.AddWithValue("$startMovie", ticket.startMovie.ToString("0"));
        command.Parameters.AddWithValue("$hallNumber", ticket.hallNumber);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }

    public List<Ticket> GetCustomerTickets(long customer_id)
    {
        List<Ticket> tickets = new List<Ticket>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT tickets.id, tickets.ticketNumber, tickets.place, tickets.row,
            tickets.startMovie, tickets.hallNumber FROM tickets,ticketpurchases WHERE
            ticketpurchases.customer_id = $customer_id AND ticketpurchases.ticket_id = tickets.id"; //??
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            tickets.Add(GetTicket(reader));
        }
        reader.Close();
        return tickets;
    }

    public Ticket GetById(long id)
    {
        Ticket ticket = new Ticket();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM tickets WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            ticket = GetTicket(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return ticket;
    }

    public int DeleteById(long id)
    {
        Ticket ticket = new Ticket();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM tickets WHERE id = $id";
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
    public long Insert(Ticket ticket)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO tickets (movieId, ticketNumber, place, row, startMovie, hallNumber) 
            VALUES ($movieId, $ticketNumber, $place, $row, $startMovie, $hallNumber);
            
            SELECT last_insert_rowid();
            ";
        command.Parameters.AddWithValue("$movieId", ticket.movieId);
        command.Parameters.AddWithValue("$ticketNumber", ticket.ticketNumber);
        command.Parameters.AddWithValue("$place", ticket.place);
        command.Parameters.AddWithValue("$row", ticket.row);
        command.Parameters.AddWithValue("$startMovie", ticket.startMovie.ToString("0"));
        command.Parameters.AddWithValue("$hallNumber", ticket.hallNumber);
        long newId = (long)command.ExecuteScalar();
        if (newId == 0)
        {
            return 0;
        }
        else
        {
            return newId; ;
        }

    }

    public Ticket GetTicket(SqliteDataReader reader)
    {
        Ticket ticket = new Ticket();
        ticket.id = long.Parse(reader.GetString(0));
        ticket.movieId = long.Parse(reader.GetString(1));
        ticket.ticketNumber = reader.GetString(2);
        ticket.place = int.Parse(reader.GetString(3));
        ticket.row = int.Parse(reader.GetString(4));
        ticket.startMovie = DateTime.Parse(reader.GetString(5));
        ticket.hallNumber = int.Parse(reader.GetString(6));

        return ticket;
    }

    public List<Ticket> GetPlacesForSession(Session session)
    {
        List<Ticket> tickets = new List<Ticket>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM sessions WHERE movie_id = $movie_id 
            AND startMovie = $start AND hallNumber = $hall_id";
        command.Parameters.AddWithValue("$movie_id", session.movie_id);
        command.Parameters.AddWithValue("$start", session.start);
        command.Parameters.AddWithValue("$hall_id", session.hall_id);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Ticket ticket = GetTicket(reader);
            tickets.Add(ticket);
        }
        reader.Close();
        return tickets;
    }
}