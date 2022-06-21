using System;
using Npgsql;
using System.Collections.Generic;

public class TicketRepository
{
    private NpgsqlConnection connection;

    public TicketRepository(NpgsqlConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        NpgsqlCommand command = connection.CreateCommand();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM tickets";
        NpgsqlDataReader reader = command.ExecuteReader();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM tickets LIMIT @pageSize OFFSET @pageSize * (@pageNumber - 1)";
        command.Parameters.AddWithValue("@pageSize", pageSize);
        command.Parameters.AddWithValue("@pageNumber", pageNumber);
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Ticket ticket = GetTicket(reader);
            tickets.Add(ticket);
        }
        reader.Close();
        return tickets;
    }


    public bool Update(long id, Ticket ticket)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE tickets SET hall_number = @hall_number
         WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@movie_id", ticket.movieId);
        command.Parameters.AddWithValue("@ticket_number", ticket.ticketNumber);
        command.Parameters.AddWithValue("@place", ticket.place);
        command.Parameters.AddWithValue("@row", ticket.row);
        command.Parameters.AddWithValue("@start_movie", ticket.startMovie);
        command.Parameters.AddWithValue("@hall_number", ticket.hallNumber);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }

    public List<Ticket> GetCustomerTickets(long customer_id)
    {
        List<Ticket> tickets = new List<Ticket>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT tickets.id, tickets.movie_id, tickets.ticket_number, tickets.place,
            tickets.row, tickets.start_movie, tickets.hall_number FROM tickets,ticketpurchases 
            WHERE ticketpurchases.customer_id = @customer_id AND ticketpurchases.ticket_id = tickets.id";
        command.Parameters.AddWithValue("@customer_id", customer_id);
        NpgsqlDataReader reader = command.ExecuteReader();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM tickets WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        NpgsqlDataReader reader = command.ExecuteReader();
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
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM tickets WHERE id = @id";
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
    public int Insert(Ticket ticket)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO tickets (movie_id, ticket_number, place, row, start_movie, hall_number) 
            VALUES (@movie_id, @ticket_number, @place, @row, @start_movie, @hall_number)
            RETURNING id
            ";
        command.Parameters.AddWithValue("@movie_id", ticket.movieId);
        command.Parameters.AddWithValue("@ticket_number", ticket.ticketNumber);
        command.Parameters.AddWithValue("@place", ticket.place);
        command.Parameters.AddWithValue("@row", ticket.row);
        command.Parameters.AddWithValue("@start_movie", ticket.startMovie);
        command.Parameters.AddWithValue("@hall_number", ticket.hallNumber);

        int newId = (int)command.ExecuteScalar();
        return newId;

    }

    public List<Ticket> GetPlacesForSession(Session session)
    {
        List<Ticket> tickets = new List<Ticket>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM tickets WHERE movie_id = @movie_id 
            AND start_movie = @start_movie AND hall_number = @hall_id";
        command.Parameters.AddWithValue("@movie_id", session.movie_id);
        command.Parameters.AddWithValue("@start_movie", session.start);
        command.Parameters.AddWithValue("@hall_id", session.hall_id);
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Ticket ticket = GetTicket(reader);
            tickets.Add(ticket);
        }
        reader.Close();
        return tickets;
    }

    public Ticket GetTicket(NpgsqlDataReader reader)
    {
        Ticket ticket = new Ticket();
        ticket.id = reader.GetInt32(0);
        ticket.movieId = reader.GetInt32(1);
        ticket.ticketNumber = reader.GetString(2);
        ticket.place = reader.GetInt32(3);
        ticket.row = reader.GetInt32(4);
        ticket.startMovie = reader.GetDateTime(5);
        ticket.hallNumber = reader.GetInt32(6);

        return ticket;
    }
}