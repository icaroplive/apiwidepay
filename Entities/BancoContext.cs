using api_widepay.Models.Contas;
using Microsoft.EntityFrameworkCore;

namespace api_widepay.Entities {
    public class BancoContext : DbContext {
        public BancoContext (DbContextOptions<BancoContext> options) : base (options) { }
        public DbSet<cad_cliente> cad_cliente { get; set; }
        public DbSet<fin_movimento> fin_movimento { get; set; }
        public DbSet<cliente_plano> cliente_plano { get; set; }
        public DbSet<planos> planos { get; set; }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
  modelBuilder.Entity<cad_cliente>().HasKey(p => p.idcad_cliente);
    modelBuilder.Entity<cliente_plano>().HasKey(p => p.id);
    modelBuilder.Entity<planos>().HasKey(p => p.id);


    modelBuilder.Entity<cad_cliente>().HasOne<cliente_plano>(s => s.cliente_plano)
        .WithOne().HasForeignKey<cliente_plano>(s => s.idcad_cliente);
    modelBuilder.Entity<cliente_plano>().HasOne<planos>(s => s.planos)
       .WithMany().HasForeignKey(s => s.idplano);
         //   modelBuilder.Entity<cliente_plano> ()
           //     .HasOne (a => a.cad_cliente); /* LEFT OUTER JOIN */
            //.WithMany ()
            //.HasForeignKey (a => a.ChildId);
        }

    }
}