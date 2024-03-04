using GameOfLifeAPI;

namespace GameOfLifeTest
{
    [TestClass]
    public class UnitTestDb
    {
        private const string DB_PATH = @".\GameOfLifeDB.db";
        private const string USERNAME = "testUser";
        private const string PASSWORD = "testPassword";

        [TestInitialize]
        public void Init()
        {

        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(DB_PATH))
            {
                File.Delete(DB_PATH);
            }
        }


        [TestMethod]
        public void RegisterAndLogin()
        {
            Register();
            Login();
        }

        private void Register()
        {
            using (var db = new DbConnect(DB_PATH))
            {
                db.Register(USERNAME, PASSWORD, 2, 3, 3);
            }
        }

        private void Login()
        {
            using (var db = new DbConnect(DB_PATH))
            {
                User user = db.Login(USERNAME, PASSWORD);
                Assert.AreEqual(USERNAME, user.Username);
                Assert.AreEqual(2, user.Underpopulation);
                Assert.AreEqual(3, user.Overpopulation);
                Assert.AreEqual(3, user.Reproduction);
            }
        }

        [TestMethod]
        public void RegisterFailNullPassword()
        {
            using (var db = new DbConnect(DB_PATH))
            {
                Assert.ThrowsException<Exception>(() => db.Register("testFailRegister", "", 2, 3, 3));
            }
        }

        [TestMethod]
        public void RegisterFailNullUsername()
        {
            using (var db = new DbConnect(DB_PATH))
            {
                Assert.ThrowsException<Exception>(() => db.Register("", "testFailRegister", 2, 3, 3));
            }
        }

        [TestMethod]
        public void RegisterFailDuplicateUsername()
        {
            string registerDuplicateUsername = "duplicateUsername";
            using (var db = new DbConnect(DB_PATH))
            {
                db.Register(registerDuplicateUsername, PASSWORD, 2, 3, 3);
                Assert.ThrowsException<Exception>(() => db.Register(registerDuplicateUsername, PASSWORD, 2, 3, 3));
            }
        }

        [TestMethod]
        public void LoginFailInvalidUsername()
        {
            using (var db = new DbConnect(DB_PATH))
            {
                Assert.ThrowsException<Exception>(() => db.Login("invalidUsername", PASSWORD));
            }
        }

        [TestMethod]
        public void LoginFailInvalidPassword()
        {
            using (var db = new DbConnect(DB_PATH))
            {
                db.Register("testUser", "testPassword", 2, 3, 3);
                Assert.ThrowsException<Exception>(() => db.Login("testUser", "invalidPassword"));
            }
        }
    }
}
