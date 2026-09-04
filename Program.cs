
namespace Biblioteca_Escolar
{
    class Program
    {
        static Biblioteca biblioteca = new Biblioteca();
        static string LeEntrada() => Console.ReadLine() ?? "";
        static int LerInteiro(string mensagem)
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

        static void PausarELimpar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static int Menu()
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
        static void Main()
        {
            do
            {
                int opcao = Menu();
                switch (opcao)
                {
                    case 0:
                        Console.Write("Deseja realmente sair? (S/N): ");
                        string confirma = LeEntrada().Trim().ToUpper();
                        if (confirma == "S")
                        {
                            Console.WriteLine("\nPROGRAMA ENCERRADO!");
                            return;
                        }
                        break;
                    case 1:
                     biblioteca.CadastrarUsuario();
                    break;

                    case 2:
                     biblioteca.CadastrarMaterial();
                    break;

                    case 3:
                     biblioteca.ListarUsuarios();
                    break;

                    case 4:
                     biblioteca.ListarMateriais();
                    break;

                    case 5:
                     biblioteca.BuscarPorCodigo();
                    break;

                    default:
                        Console.WriteLine("\nOPÇÂO INVÁLIDA!");
                        break;
                }
                PausarELimpar();
            } while (true);
        }
    }
}
