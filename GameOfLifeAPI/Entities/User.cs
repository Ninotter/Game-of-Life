using SQLite;

namespace GameOfLifeAPI.Entities
{
    /// <summary>
    /// Represents a user in the database, with its username, password and the rules of the game
    /// </summary>
    [Table("User")]
    public record User : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; init; }

        [NotNull]
        public string Username { get; init; }

        [NotNull]
        public string PasswordUser { get; set; }

        [NotNull]
        public byte Overpopulation { get; init; } = 3;

        [NotNull]
        public byte Underpopulation { get; init; } = 2;

        [NotNull]
        public byte Reproduction { get; init; } = 3;

        public User()
        {

        }
    }
}
