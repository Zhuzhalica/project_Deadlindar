namespace WebAPI.Server
{
    public class LoginResponse
    {
        // public LoginResponse(int id, string name, string surname, string login,  int role, string cookie)
        // {
        //     Id = id;
        //     Name = name;
        //     Surname = surname;
        //     Login = login;
        //     Role = role;
        //     Cookie = cookie;
        // }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public int Role { get; set; }
        public string Cookie { get; set; }
    }
}