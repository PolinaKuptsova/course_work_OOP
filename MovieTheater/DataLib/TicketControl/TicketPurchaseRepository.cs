using System;
using Npgsql;
using System.Collections.Generic;

public class TicketPurchaseRepository
{
    private NpgsqlConnection connection;

    public TicketPurchaseRepository(NpgsqlConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM ticketpurchases";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<TicketPurchase> GetAll()
    {
        List<TicketPurchase> ticketPurchases = new List<TicketPurchase>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM ticketpurchases";
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            ticketPurchases.Add(GetTicketPurchase(reader));
        }
        reader.Close();
        return ticketPurchases;
    }

    public List<TicketPurchase> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<TicketPurchase> ticketPurchases = new List<TicketPurchase>();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM ticketpurchases LIMIT @pageSize OFFSET @pageSize * (@pageNumber - 1)";
        command.Parameters.AddWithValue("@pageSize", pageSize);
        command.Parameters.AddWithValue("@pageNumber", pageNumber);
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            TicketPurchase ticketPurchase = GetTicketPurchase(reader);
            ticketPurchases.Add(ticketPurchase);
        }
        reader.Close();
        return ticketPurchases;
    }

    public bool Update(long id, bool isCanceled)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE ticketpurchases SET is_canceled = @is_cnceled, 
             WHERE ticket_id = @ticket_id";
        command.Parameters.AddWithValue("@ticket_id", id);
        command.Parameters.AddWithValue("@is_canceled", isCanceled);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }


    public TicketPurchase GetById(long id)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM ticketpurchases WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            ticketPurchase = GetTicketPurchase(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return ticketPurchase;
    }

    public int DeleteById(long id)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM ticketpurchases WHERE hall_id = @hall_id";
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

    public int Insert(TicketPurchase ticketPurchase)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO ticketPurchases (ticket_id, created_at, price, customer_id, payment_way, is_canceled,session_id) 
            VALUES (@ticket_id, @created_at, @price, @customer_id, @payment_way, @is_canceled, @session_id)
            RETURNING id
            ";
        command.Parameters.AddWithValue("@ticket_id", ticketPurchase.ticket_id);
        command.Parameters.AddWithValue("@created_at", ticketPurchase.createdAt);
        command.Parameters.AddWithValue("@price", ticketPurchase.price);
        command.Parameters.AddWithValue("@customer_id", ticketPurchase.customer_id);
        command.Parameters.AddWithValue("@payment_way", ticketPurchase.payment_way);
        command.Parameters.AddWithValue("@is_canceled", ticketPurchase.isCanceled == true ? 1 : 0);
        command.Parameters.AddWithValue("@session_id", ticketPurchase.session_id);
        int newId = (int)command.ExecuteScalar();
        return newId;

    }

    public TicketPurchase GetTicketPurchase(NpgsqlDataReader reader)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        ticketPurchase.id = reader.GetInt64(0);
        ticketPurchase.ticket_id = reader.GetInt64(1);
        ticketPurchase.createdAt = reader.GetDateTime(2);
        ticketPurchase.price = reader.GetDouble(3);
        ticketPurchase.customer_id = reader.GetInt64(4);
        ticketPurchase.payment_way = reader.GetString(5);
        ticketPurchase.isCanceled = reader.GetInt32(6) == 1 ? true : false;
        ticketPurchase.session_id = reader.GetInt32(1);

        return ticketPurchase;
    }
}