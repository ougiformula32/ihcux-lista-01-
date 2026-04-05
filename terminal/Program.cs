using System;
using System.Net;

class Program
{
    static void Main()
    {
        // Define o título da janela do console
        Console.Title = "Console de Diagnóstico de Rede";

        // Variável de controle para manter o sistema rodando
        bool executando = true;

        // Loop principal do sistema
        while (executando)
        {
            // Exibe a tela principal com o menu
            // Heurística #6 - Reconhecimento em vez de recordação:
            // o usuário vê os comandos disponíveis na tela,
            // sem precisar decorar tudo.
            DesenharTela();

            Console.Write("\nDigite um comando: ");
            string comando = Console.ReadLine()?.Trim().ToLower() ?? "";

            // Estrutura que identifica o comando digitado
            switch (comando)
            {
                case "1":
                case "ping":
                    // O sistema aceita número e palavra.
                    // Heurística #6:
                    // isso facilita o reconhecimento e reduz esforço de memória.
                    ExecutarPing();
                    break;

                case "2":
                case "reiniciar":
                    ExecutarReiniciarServidor();
                    break;

                case "3":
                case "formatar":
                    ExecutarFormatarUnidade();
                    break;

                case "help":
                case "?":
                    // Heurística #10 - Ajuda e documentação:
                    // o usuário pode digitar help ou ? para receber explicações.
                    MostrarAjuda();
                    break;

                case "4":
                case "sair":
                    executando = false;
                    Console.WriteLine("\nEncerrando o sistema...");
                    break;

                default:
                    // Mensagem clara para erro de comando
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nComando inválido.");
                    Console.ResetColor();

                    // Em vez de deixar o usuário perdido,
                    // o sistema sugere consultar a ajuda.
                    Console.WriteLine("Digite 'help' ou '?' para ver os comandos disponíveis.");

                    Pausar();
                    break;
            }
        }
    }

    static void DesenharTela()
    {
        // Limpa a tela para redesenhar o menu principal
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================================");
        Console.WriteLine("      CONSOLE DE DIAGNÓSTICO DE REDE");
        Console.WriteLine("==============================================");
        Console.ResetColor();

        // Heurística #6 - Reconhecimento em vez de recordação:
        // menu visível com comandos rápidos.
        Console.WriteLine("Menu de Comandos Rápidos:");
        Console.WriteLine("[1] Pingar IP");
        Console.WriteLine("[2] Reiniciar Servidor");
        Console.WriteLine("[3] Formatar Unidade");
        Console.WriteLine("[4] Sair");

        // O sistema também lembra que existe ajuda disponível
        Console.WriteLine("\nDigite também: help ou ? para ajuda");

        // Heurística #6:
        // legenda fixa com os comandos no rodapé da tela.
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n----------------------------------------------");
        Console.WriteLine("Legenda fixa: ping | reiniciar | formatar | help | sair");
        Console.WriteLine("----------------------------------------------");
        Console.ResetColor();
    }

    static void ExecutarPing()
    {
        Console.Write("\nDigite o IP que deseja testar: ");
        string ip = Console.ReadLine()?.Trim() ?? "";

        // Validação do IP digitado
        if (ValidarIP(ip))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nPingando o IP {ip}...");
            Console.WriteLine("Resposta recebida com sucesso. Latência: 12ms");
            Console.ResetColor();
        }
        else
        {
            // Quando o IP é inválido, o sistema não dá erro genérico.
            // Ele explica o problema e sugere o formato correto.
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nIP inválido.");
            Console.WriteLine("Use o formato correto: xxx.xxx.xxx.xxx");
            Console.ResetColor();
        }

        Pausar();
    }

    static bool ValidarIP(string ip)
    {
        // Tenta validar se o texto digitado é um IP válido
        return IPAddress.TryParse(ip, out _);
    }

    static void ExecutarReiniciarServidor()
    {
        // Heurística #5 - Prevenção de erros:
        // antes de executar uma ação crítica,
        // o sistema alerta o usuário.
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nATENÇÃO: Reiniciar o servidor pode interromper serviços em execução.");
        Console.ResetColor();

        // Confirmação extra antes da ação
        Console.Write("Deseja realmente reiniciar o servidor? (S/N): ");
        string confirmacao = Console.ReadLine()?.Trim().ToLower() ?? "";

        if (confirmacao == "s" || confirmacao == "sim")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nServidor reiniciado com sucesso.");
            Console.ResetColor();
        }
        else
        {
            // Caso o usuário desista, a ação é cancelada
            Console.WriteLine("\nOperação cancelada pelo usuário.");
        }

        Pausar();
    }

    static void ExecutarFormatarUnidade()
    {
        // Heurística #5 - Prevenção de erros:
        // ação altamente perigosa entra em modo de alerta visual.
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n** MODO DE ALERTA **");
        Console.WriteLine("Você está prestes a FORMATAR uma unidade.");
        Console.WriteLine("Todos os dados serão apagados permanentemente.");
        Console.ResetColor();

        // O sistema exige uma confirmação mais forte,
        // reduzindo risco de execução acidental.
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nDigite EXATAMENTE 'FORMATAR' para confirmar: ");
        Console.ResetColor();

        string confirmacao = Console.ReadLine()?.Trim() ?? "";

        if (confirmacao == "FORMATAR")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nUnidade formatada com sucesso. (Simulação)");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("\nFormatação cancelada. Confirmação inválida.");
        }

        Pausar();
    }

    static void MostrarAjuda()
    {
        // Heurística #10 - Ajuda e documentação:
        // apresenta explicação breve de cada função
        // sem sair da tela atual do programa.
        Console.WriteLine("\n========== AJUDA ==========");
        Console.WriteLine("ping ou 1       -> Testa a conectividade com um endereço IP.");
        Console.WriteLine("reiniciar ou 2  -> Reinicia o servidor após confirmação.");
        Console.WriteLine("formatar ou 3   -> Simula a formatação de uma unidade com alerta crítico.");
        Console.WriteLine("help ou ?       -> Exibe esta ajuda sem sair da tela.");
        Console.WriteLine("sair ou 4       -> Fecha o sistema.");
        Console.WriteLine("===========================\n");

        Pausar();
    }

    static void Pausar()
    {
        // Dá tempo para o usuário ler a mensagem antes da tela ser redesenhada
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
