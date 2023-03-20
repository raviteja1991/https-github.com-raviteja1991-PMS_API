namespace PatientManagementAPI.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class JWTTokenResponse
    {
        public string? Token { get; set; }
    }
}
