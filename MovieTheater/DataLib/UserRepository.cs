using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class UserRepository
{
    private SqliteConnection connection;

    public UserRepository(SqliteConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM users";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public long GetUserByName(string userName)
    {
        throw new NotImplementedException();
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<Customer> GetAll()
    {
        List<Customer> users = new List<Customer>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM users";
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(GetUser(reader));
        }
        reader.Close();
        return users;
    }

    public List<Customer> GetAllByAccessLevel(string accessLevel)
    {
        List<Customer> users = new List<Customer>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM users WHERE accessLevel = $accessLevel" ;
        command.Parameters.AddWithValue("$accessLevel", accessLevel);   
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(GetUser(reader));
        }
        reader.Close();
        return users;
    }

    public List<Customer> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<Customer> users = new List<Customer>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM users LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
        command.Parameters.AddWithValue("$pageSize", pageSize);
        command.Parameters.AddWithValue("$pageNumber", pageNumber);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Customer user = GetUser(reader);
            users.Add(user);
        }
        reader.Close();
        return users;
    }

    public bool UpdateUserAccount(long id, Customer user)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE users SET name = $name, password = $password,
        phoneNumber = $phoneNumber, age = $age WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$name", user.Name);
        command.Parameters.AddWithValue("$password", user.Password);
        command.Parameters.AddWithValue("$phoneNumber", user.PhoneNumber);
        command.Parameters.AddWithValue("$age", user.age);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }

    public bool UpdateUserBalance(long id, double newBalance)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE users SET balance = $balance WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("balance", newBalance);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }

    public bool UpdateUserStatus(long id, bool isBlocked)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE users SET isBlockes = $isBlocked WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("isBlocked", isBlocked);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }

    public bool UpdateUserAccessLevel(long id, string accessLevel)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE users SET accessLevel = $accessLevel WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("accessLevel", accessLevel);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }

    public bool UpdateUserAge(long id, Customer user)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE users SET age = $age WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("age", user.age);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;
    }

    public Customer GetById(long id)
    {
        Customer user = new Customer();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM users WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            user = GetUser(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return user;
    }

    public int DeleteById(long id)
    {
        Customer user = new Customer();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM users WHERE id = $id";
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
    public int Insert(Customer user)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO users (name, password, phoneNumber, age, isBlocked, accessLevel, balance) 
            VALUES ($name, $password, $phoneNumber, $age, $isBlocked, $accessLevel, $balance);
            
            SELECT last_insert_rowid();
            ";
        string hash = Authentication.GetHash(user.Password);
        command.Parameters.AddWithValue("$name", user.Name);
        command.Parameters.AddWithValue("$password", hash);
        command.Parameters.AddWithValue("$phoneNumber", user.PhoneNumber);
        command.Parameters.AddWithValue("$age", user.age);
        command.Parameters.AddWithValue("$isBlocked", user.isBlocked);
        command.Parameters.AddWithValue("$accessLevel", user.accessLevel);
        command.Parameters.AddWithValue("$balance", user.Balance);
        
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

    public Customer GetUser(SqliteDataReader reader)
    {
        Customer user = new Customer();;
        user.id = long.Parse(reader.GetString(0));
        user.Name = reader.GetString(1);
        user.Password = reader.GetString(2);
        user.PhoneNumber = reader.GetString(3);
        user.age = int.Parse(reader.GetString(4));
        user.isBlocked = reader.GetString(5) == "0" ? false : true;
        user.accessLevel = reader.GetString(6);
        user.Balance = double.Parse(reader.GetString(7));

        return user;
    }
}