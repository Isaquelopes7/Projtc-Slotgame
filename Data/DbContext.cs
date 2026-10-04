using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teste_Projtc_Slotgame.Models;

namespace Teste_Projtc_Slotgame.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            string connection = "server=localhost;database=game_slot;user=root;password=basico123";
            optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection)); // Configura o provedor MySQL e detecta automaticamente a versão do servidor
        }

    }
}
