using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Models;

namespace PeopleApi.Data
{
    public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> opts) : base(opts) { }
    }
}
