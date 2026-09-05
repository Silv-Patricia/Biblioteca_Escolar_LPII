
namespace Biblioteca_Escolar
{
    class Program
    {
        static Biblioteca biblioteca = new Biblioteca();

        static void Main()
        {
            Console.Clear();
            do
            {
                int opcao = Menu.ExibirMenu();
                Console.WriteLine();
                switch (opcao)
                {
                    case 0:
                        Console.Write("Deseja realmente sair? (S/N): ");
                        string confirma = Menu.LeEntrada().Trim().ToUpper();
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
                        biblioteca.ConsultarMaterialPorCodigo();
                        break;
                    case 6:
                        biblioteca.ConsultarUsuarioPorCodigo();
                        break;
                    case 7:
                        biblioteca.RealizarEmprestimo();
                        break;
                    case 8:
                        biblioteca.RealizarDevolucao();
                        break;
                    case 9:
                        biblioteca.ExibirEmprestimosAtivos();
                        break;
                    case 10:
                        biblioteca.GerarRelatorio();
                        break;
                    case 11:
                        biblioteca.AtualizaEmailDeUsuario();
                        break;
                    case 12:
                        biblioteca.TestarFuturaDataDeDevolucao();
                        break;
                    default:
                        Console.WriteLine("\nOPÇÂO INVÁLIDA!");
                        break;
                }
                Menu.PausarELimpar();
            } while (true);
        }
    }
}
