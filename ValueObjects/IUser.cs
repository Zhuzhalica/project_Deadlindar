namespace ValueObjects
{
    public interface IUser
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Login { get; set; }
    }
}