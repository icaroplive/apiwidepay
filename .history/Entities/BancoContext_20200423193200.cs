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
        public DbSet<cliente_plano> cliente_plano { get; set; }
        public DbSet<planos> planos { get; set; }
          protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<cad_cliente> ()
                .HasOne (a => a.cliente_plano); /* LEFT OUTER JOIN */
            //.WithMany ()
            //.HasForeignKey (a => a.ChildId);
        }

    }
}