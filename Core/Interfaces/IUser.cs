namespace Core.Interfaces
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        int Age { get; set; }
        string Description { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        public string Password { get; set; }
    }
}