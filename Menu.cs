namespace Biblioteca_Escolar
{
    class Menu
    {
        public static string LeEntrada() => Console.ReadLine() ?? "";

        public static int LerInteiro(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    return valor;
                }
                Console.WriteLine("Entrada inválida! Digite um número inteiro.");
            }
        }

        public static void PausarELimpar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
        }

        public static int ExibirMenu()
        {
            Console.WriteLine("\n********************** MENU *********************");
            Console.WriteLine("1 - Cadastrar usuário\t\t6 - Realizar empréstimo");
            Console.WriteLine("2 - Cadastrar material\t\t7 - Registrar devolução");
            Console.WriteLine("3 - Listar usuários\t\t8 - Exibir empréstimos ativos");
            Console.WriteLine("4 - Listar materiais\t\t9 - Exibir relatório");
            Console.WriteLine("5 - Consultar por código\t0 - Sair da Execução");
            Console.WriteLine("*************************************************");
            return LerInteiro("Digite sua opção: ");
        }
    }

}