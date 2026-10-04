using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teste_Projtc_Slotgame.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Inicializa a propriedade Nome com uma string vazia
        public string Senha { get; set; } = string.Empty; 
        public int Moedas { get; set; } = 100;

        [Column("usuario")]  // Porque la no banco a coluna é chamada "usuario" e não "Login"
        public string Login { get; set; } = string.Empty;
    }
}
