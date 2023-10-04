namespace hara_anargya_test.Models
{
    public class Authentication
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int IsActive { get; set; }
        public int LoggedIn { get; set; }
    }
}
