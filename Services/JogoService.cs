using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Teste_Projtc_Slotgame.Data;
using Teste_Projtc_Slotgame.Models;

namespace Teste_Projtc_Slotgame.Services
{
    public class JogoService
    {
        private readonly UsuarioService _usuarioService; // classe de serviço para gerenciar usuários

        // Readonly significa que a variável só pode ser atribuída no construtor da classe e não pode ser alterada posteriormente.
        public JogoService(UsuarioService usuarioService) // injeção de dependência do serviço de usuário
        {
            _usuarioService = usuarioService;
        }   


        private readonly string[] _icons =
        {
           "🍒",
           "🐔",
           "⭐",
           //"🔑",
           "💎"
       };

        private readonly Random _random = new Random(); // Criando uma instância de Random para gerar os icones aleatórios
        //A anderlaine ( _ ) serve para indicar que a variável é privada e não deve ser acessada fora da classe.
        public void Jogar(Usuario usuario)
        {
            Load();
            Console.WriteLine($"\n==== Bem Vindo {usuario.Nome} ao SlotIcons! ====");

            while (true)
            {
                if (usuario.Moedas <= 0)
                {
                    Console.WriteLine("\n================================");
                    Console.WriteLine("Você ficou sem moedas!");
                    Console.WriteLine("================================");
                    Console.WriteLine("\n[D] Receber +100 moedas");
                    Console.WriteLine("[S] Sair");

                    string opcaoSemMoedas = Console.ReadLine()!;

                    if (opcaoSemMoedas.ToUpper() == "D")
                    {
                        _usuarioService.AtualizarMoedas(usuario, 100);

                        Console.WriteLine("\nVocê recebeu 100 moedas!");
                        Console.WriteLine($"Moedas atuais: {usuario.Moedas}");

                        continue; // volta para o início do while
                    }
                    else
                    {
                        break; // sai do jogo
                    }
                }
                    Console.WriteLine("\nPressione ENTER para girar ");
                Console.WriteLine($"\n-> Seu total de moedas iniciais é {usuario.Moedas}");
                Console.WriteLine("\nDigite S para sair do jogo ");

                string opcao = Console.ReadLine()!;
                if (opcao.ToUpper() == "S")
                    break;

                string slot1 = _icons[_random.Next(_icons.Length)];
                string slot2 = _icons[_random.Next(_icons.Length)];
                string slot3 = _icons[_random.Next(_icons.Length)];

                Console.WriteLine("\n 🎰 Girando...");
                Thread.Sleep(2000);

                Console.WriteLine($"Slot 1 -> {slot1}");
                Thread.Sleep(500);
                Console.WriteLine($"Slot 2 -> {slot2}");
                Thread.Sleep(500);
                Console.WriteLine($"Slot 3 -> {slot3}");
                Thread.Sleep(500);

                //Console.WriteLine($"\nResultado: {slot1}| {slot2} | {slot3}\n");

                if (slot1 == slot2 && slot2 == slot3)
                {
                    Console.WriteLine("Parabéns! Você ganhou!");
                    Console.WriteLine("\nMoedas ganhas: 50");
                    _usuarioService.AtualizarMoedas(usuario, 50);
                    Console.WriteLine($"-> Seu total de moedas agora é {usuario.Moedas}");
                }
                else
                {
                    Console.WriteLine("Ops! Tente novamente!");
                    Console.WriteLine("Moedas perdidas: 10");
                    _usuarioService.AtualizarMoedas(usuario, -10);
                    Console.WriteLine($"-> Seu total de moedas agora é {usuario.Moedas}");
                }
            }

            Console.WriteLine("Obrigado por jogar!");


        }

        private void Load()
        {
            Console.Write("Carregando ");

            for (int i = 0; i < 10; i++)
            {
                Console.Write("█");
                Thread.Sleep(150);
            }

        }

    }
}

        