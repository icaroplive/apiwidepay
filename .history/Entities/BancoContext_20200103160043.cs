using api_widepay.Models.Contas;
using Microsoft.EntityFrameworkCore;

namespace api_widepay.Entities
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options): base(options)
        { }
        public DbSet<cad_cliente> cad_cliente { get; set; }
        public DbSet<fin_movimento> fin_movimento { get; set; }
    }
}