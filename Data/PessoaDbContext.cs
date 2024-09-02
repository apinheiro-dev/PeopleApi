using Microsoft.EntityFrameworkCore;
using PeopleApi.Models;

namespace PeopleApi.Data
{
    public class PessoaDbContext : DbContext
    {
        public PessoaDbContext(DbContextOptions<PessoaDbContext> opts) : base(opts) { }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
