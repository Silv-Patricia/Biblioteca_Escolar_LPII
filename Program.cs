
namespace Biblioteca_Escolar {
    class Program {
        static void Main() {
            string opc;
            
            do {
                Console.WriteLine("\n***********************Menu**********************");
                Console.WriteLine("1 - Cadastrar usuário\t\t6 - Realizar empréstimo");
                Console.WriteLine("2 - Cadastrar material\t\t7- Registrar devolução");
                Console.WriteLine("3 - Listar usuários\t\t8 - Exibir empréstimos ativos");
                Console.WriteLine("4 - Listar materiais\t\t9 - Exibir relatório");
                Console.WriteLine("5 - Consultar por código\t0 - Sair da Execução");
                Console.WriteLine("*************************************************");

                opc = Console.ReadLine() ?? "";

            } while (!int.TryParse(opc, out int op));
        }
    }
}
