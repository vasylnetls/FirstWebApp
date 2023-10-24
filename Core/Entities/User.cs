using Core.Interfaces;

namespace Core.Entities
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Address? Address { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Password { get; set; }
    }
}
