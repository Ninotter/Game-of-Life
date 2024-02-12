using Microsoft.Data.Sqlite;
using System.Security.Cryptography;

namespace GameOfLifeAPI
{
    public class DbConnect : IDisposable
    {
        private SqliteConnection _conn;
        public DbConnect(string dbPath)
        {
            _conn = new SqliteConnection($"Data Source={dbPath}");
            _conn.Open();
        }

        public void Dispose()
        {
            _conn.Close();
        }

        public User Login(string username, string password)
        {
            byte[] hash = GenerateHash(password);
            var command = _conn.CreateCommand();
            command.CommandText = "SELECT username, underpopulation, overpopulation, reproduction FROM user WHERE username = @username AND password = @password";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", hash);
            using (var reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    throw new Exception("Invalid username or password");
                }
                else
                {
                    return new User
                    {
                        Username = reader.GetString(0),
                        Underpopulation = reader.GetByte(1),
                        Overpopulation = reader.GetByte(2),
                        Reproduction = reader.GetByte(3)
                    };
                }
            }
        }

        public void Register(string username, string password, int underpopulation, int overpopulation, int reproduction)
        {
            byte[] hash = GenerateHash(password);
            var command = _conn.CreateCommand();
            command.CommandText = "INSERT INTO user (username, password, underpopulation, overpopulation, reproduction) VALUES (@username, @password, @underpopulation, @overpopulation, @reproduction)";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", hash);
            command.Parameters.AddWithValue("@underpopulation", underpopulation);
            command.Parameters.AddWithValue("@overpopulation", overpopulation);
            command.Parameters.AddWithValue("@reproduction", reproduction);
            int result = command.ExecuteNonQuery();
            if (result < 0)
            {
                throw new System.Exception("Error registering user");
            }
        }

        public void Register(User user)
        {
            var command = _conn.CreateCommand();
            command.CommandText = "INSERT INTO user (username, underpopulation, overpopulation, reproduction) VALUES (@username, @underpopulation, @overpopulation, @reproduction)";
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@underpopulation", user.Underpopulation);
            command.Parameters.AddWithValue("@overpopulation", user.Overpopulation);
            command.Parameters.AddWithValue("@reproduction", user.Reproduction);
            int result = command.ExecuteNonQuery();
            if (result < 0)
            {
                throw new System.Exception("Error registering user");
            }
        }

        private byte[] GenerateHash(string password)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return byteHash;
        }
    }
}