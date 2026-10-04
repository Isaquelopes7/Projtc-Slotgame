using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Text;
using Teste_Projtc_Slotgame.Models;
using Teste_Projtc_Slotgame.Services;


class Program
{
    static void Main()
    {
        // Para os icons aparecerem no terminal
        Console.InputEncoding = Encoding.UTF8; // Configurando a codificação de entrada para UTF-8 
        Console.OutputEncoding = Encoding.UTF8; // Configurando a codificação de saída para UTF-8

        UsuarioService usuarioService = new UsuarioService();
        JogoService jogoSercice = new JogoService(usuarioService);

        while (true)
        {
            Console.Clear();

            Console.WriteLine("\n==== Bem Vindo ao SlotIcons! ====");
            Console.WriteLine("\nEscolha uma das opções:");
            Console.WriteLine("1- Cadastre uma conta para jogar\n2- Logue na sua conta\n3- Sair");

            string? opcao = Console.ReadLine()!; 

            switch (opcao)
            {
                case "1":
                    usuarioService.Cadastrar();
                    break;
                case "2":
                    Usuario? usuario = usuarioService.Login();
                    if (usuario != null)
                    {
                        jogoSercice.Jogar(usuario);
                    }
                    else
                    {
                        Console.WriteLine("Login ou senha incorretos. Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                    break;
                case "3":
                    return;

                default:
                    Console.WriteLine("\nOpção inválida!");
                    Console.ReadKey();
                    break;
            }



        }
    }

    
}
