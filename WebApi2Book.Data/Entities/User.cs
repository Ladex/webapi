namespace WebApi2Book.Data.Entities
{
    public class User : IVersionedEntity
    {
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public byte[] Version { get; set; }
    }
}