using System.Security.Cryptography;
using SQLiteNetExtensions.Extensions;
using GameOfLifeAPI.Entities;
using SQLite;

namespace GameOfLifeAPI
{
    public class DbConnect : IDisposable
    {
        private SQLiteConnection? _conn;
        public DbConnect(string dbPath)
        {
            Connect(dbPath);
        }

        private void Connect(string dbPath)
        {
            if (_conn == null)
            {
                SQLiteConnectionString connectionString = new SQLiteConnectionString(dbPath, true);
                _conn = new SQLiteConnection(connectionString);
            }
            CreateAllTables();
        }

        public void Dispose()
        {
            _conn?.Close();
        }

        internal int CreateTable<T>() where T : IEntity
        {
            var result = _conn.CreateTable<T>();
            if (result == CreateTableResult.Created)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void CreateAllTables()
        {
            CreateTable<User>();
            //var baseEntity = typeof(IEntity);
            //var types = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(s => s.GetTypes())
            //    .Where(p => baseEntity.IsAssignableFrom(p) && p != baseEntity);

            //foreach (Type t in types)
            //{
            //    _conn.CreateTable(t);
            //}
        }

        public User Login(string username, string password)
        {
            byte[] hash = GenerateHash(password);
            string hashedPw = System.Text.Encoding.UTF8.GetString(hash);
            User connected = new User()
            {
                Username = username,
                PasswordUser = hashedPw
            };
            string query = "SELECT * FROM User WHERE Username = ? AND PasswordUser = ?";
            connected = _conn.FindWithQuery<User>(query, connected.Username, connected.PasswordUser);
            if (connected is null)
            {
                throw new Exception("Invalid credentials, or the user does not exist.");
            }
            return connected;
        }

        public void Register(string username, string password, int underpopulation, int overpopulation, int reproduction)
        {
            byte[] hash = GenerateHash(password);
            string hashedPw = System.Text.Encoding.UTF8.GetString(hash);
            var newUser = new User()
            {
                Username = username,
                PasswordUser = hashedPw,
                Underpopulation = (byte)underpopulation,
                Overpopulation = (byte)overpopulation,
                Reproduction = (byte)reproduction
            };
            Register(newUser);
        }

        private void Register(User user)
        {
            _conn.Insert(user);
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