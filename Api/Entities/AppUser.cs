
namespace Api.Entities;

    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }    // upper case N for ASP.NET Core Identity
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
