namespace NZWalks.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }    
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
        //Navigation properties
        public IEnumerable<User_Role> UserRoles { get; set; }
    }
}
