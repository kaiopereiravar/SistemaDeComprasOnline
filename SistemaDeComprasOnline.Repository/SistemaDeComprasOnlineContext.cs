using Microsoft.EntityFrameworkCore;
using SistemaDeComprasOnline.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeComprasOnline.Repository
{
    public class SistemaDeComprasOnlineContext : DbContext
    {
        public SistemaDeComprasOnlineContext(DbContextOptions <SistemaDeComprasOnlineContext> options) : base(options) { }
    
        public DbSet<TabCartao> tabCartao { get; set; }
        public DbSet<TabContaCorrente> TabContaCorrente { get; set; }
        public DbSet<TabSaldo> TabSaldo { get; set; }
        public DbSet<TabHistorico_transacao> TabHistorico_transacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabCartao>().ToTable("TabCartao");
            modelBuilder.Entity<TabContaCorrente>().ToTable("TabContaCorrente");
            modelBuilder.Entity<TabSaldo>().ToTable("TabSaldo");
            modelBuilder.Entity<TabHistorico_transacao>().ToTable("TabHistorico_transacao");
        }
    }
}
