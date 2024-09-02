using Microsoft.AspNetCore.Identity;

namespace PeopleApi.Models
{
    public class Usuario : IdentityUser
    {
        // Rever necessidade
        public DateTime DataNascimento { get; set; }
        public Usuario() : base() { }
    }
}
