using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste_Projtc_Slotgame.Data;
using Teste_Projtc_Slotgame.Models;

namespace Teste_Projtc_Slotgame.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context = new();

        public void Cadastrar() { 

            Console.WriteLine("Crie seu nome: ");
            string nome = Console.ReadLine()!;
            Console.WriteLine("\nCrie sua senha:");
            string senha = Console.ReadLine()!;
            Usuario usuario = new Usuario
            {
                Nome = nome,
                Senha = senha, // Defina a senha padrão ou solicite ao usuário
                Moedas = 100, // Defina a quantidade inicial de moedas
                Login = nome // Defina o login como o nome do usuário
            };
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                Console.WriteLine("Usuário cadastrado com sucesso!");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                if (ex.InnerException != null)
                {
                    Console.WriteLine("\nINNER:");
                    Console.WriteLine(ex.InnerException.ToString());
                }

                Console.ReadKey();
            }
        }
        public Usuario? Login() // Este ponto de interrogação indica que o método pode retornar um objeto Usuario ou null, caso não encontre um usuário correspondente.
        {
            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine()!;

            Console.Write("Digite sua senha: ");
            string senha = Console.ReadLine()!;

            Usuario? usuario = _context.Usuarios
                .FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
            // FirstOrDefault é usado para obter o primeiro elemento que corresponde à condição especificada ou um valor padrão (null) se não houver correspondência.
            return usuario;
        }
        public void AtualizarMoedas(Usuario usuario, int quantidade)
        {
            usuario.Moedas += quantidade;
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
    }
}
