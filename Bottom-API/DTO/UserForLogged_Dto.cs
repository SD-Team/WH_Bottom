using System.Collections.Generic;

namespace Bottom_API.DTO
{
    public class UserForLogged_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Nik { get; set; }
        public List<string> Role { get; set; }
    }
}