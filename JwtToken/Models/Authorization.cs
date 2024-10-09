namespace JwtToken.Models
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }
        public const string default_username = "berkankr";
        public const string default_email = "berkankr@mail.com";
        public const string default_password = "Berkan123*";
        public const Roles default_role = Roles.User;
    }
}

