using System.Security.Cryptography;
using GameOfLifeAPI.Entities;
using SQLite;

namespace GameOfLifeAPI
{
    public class DbConnect : IDisposable
    {
        private SQLiteConnection? _conn;
        private const int MAXIMUM_USERNAME_LENGTH = 20;
        private const int MINIMUM_USERNAME_LENGTH = 6;
        private const int MINIMUM_PASSWORD_LENGTH = 6;
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
            User connected = new User()
            {
                Username = username,
                PasswordUser = password
            };
            VerifyUserFieldsAreValid(connected);
            byte[] hash = GenerateHash(password);
            string hashedPw = System.Text.Encoding.UTF8.GetString(hash);
            connected.PasswordUser = hashedPw;
            string query = "SELECT * FROM User WHERE Username = ? AND PasswordUser = ?";
            connected = _conn.FindWithQuery<User>(query, connected.Username, connected.PasswordUser);
            if (connected is null)
            {
                throw new Exception("Invalid credentials, or the user does not exist.");
            }
            return connected;
        }

        private bool VerifyUserFieldsAreValid(User user)
        {
            if(user.Username is not null)
            {
                if (user.Username.Length < MINIMUM_PASSWORD_LENGTH)
                {
                    throw new Exception("Username must be at least 6 characters long.");
                }
                if (user.Username.Length > MAXIMUM_USERNAME_LENGTH)
                {
                    throw new Exception("Username must be at most 20 characters long.");
                }
            }
            if (user.PasswordUser is not null)
            {
                if (user.PasswordUser.Length < MINIMUM_PASSWORD_LENGTH)
                {
                    throw new Exception("Password must be at least 6 characters long.");
                }
            }
            return true;
        }

        public void Register(string username, string password, int underpopulation, int overpopulation, int reproduction)
        {
            var newUser = new User()
            {
                Username = username,
                PasswordUser = password,
                Underpopulation = (byte)underpopulation,
                Overpopulation = (byte)overpopulation,
                Reproduction = (byte)reproduction
            };
            VerifyUserFieldsAreValid(newUser);
            byte[] hash = GenerateHash(password);
            string hashedPw = System.Text.Encoding.UTF8.GetString(hash);
            newUser.PasswordUser = hashedPw;
            Register(newUser);
        }

        private void VerifyUserExists(string username)
        {
            User user = _conn.Find<User>(u => u.Username == username);
            if (user != null)
            {
                throw new Exception("Username already exists.");
            }
        }

        private void Register(User user)
        {
            VerifyUserExists(user.Username);
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