namespace Biblioteca_Escolar {
    class Menu {
        public static string LeEntrada() => Console.ReadLine() ?? "";

        public static int LerNumeroInteiro(string mensagem) {
            while (true) {
                Console.Write(mensagem);
                if (int.TryParse(Console.ReadLine(), out int valor)) {
                    return valor;
                }
                Console.WriteLine("\nEntrada inválida! Digite um número inteiro.");
            }
        }

        public static string LerStringObrigatoria(string mensagem) {
            while (true) {
                Console.Write(mensagem);
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(entrada)) {
                    return entrada;
                }

                Console.WriteLine("Entrada inválida! Este campo não pode ficar vazio.");
            }
        }

        public static DateTime? LerDataAtualOuPassada(string mensagem) {
            while (true) {
                Console.Write(mensagem);
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(entrada)) {
                    return null;
                }

                if (DateTime.TryParse(entrada, out DateTime dataValidada)) {
                    if (dataValidada <= DateTime.Now.Date) {
                        return dataValidada;
                    }
                    Console.WriteLine("\nErro: A data informada não pode estar no futuro.\n");
                }
                else {
                    Console.WriteLine("\nFormato de data inválido. Use DD/MM/AAAA.");
                }
            }
        }

        public static DateTime? LerQualquerData(string mensagem) {
            while (true) {
                Console.Write(mensagem);
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(entrada)) {
                    return null;
                }

                if (DateTime.TryParse(entrada, out DateTime dataValidada)) {
                    return dataValidada;
                }
                else {
                    Console.WriteLine("\nFormato de data inválido. Use DD/MM/AAAA.");
                }
            }
        }

        public static void PausarELimpar() {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
        }

        public static int ExibirMenu() {
            Console.WriteLine("\n********************** MENU *********************");
            Console.WriteLine("1 - Cadastrar usuário\t\t7 - Realizar empréstimo");
            Console.WriteLine("2 - Cadastrar material\t\t8 - Registrar devolução");
            Console.WriteLine("3 - Listar usuários\t\t9 - Exibir empréstimos ativos");
            Console.WriteLine("4 - Listar materiais\t\t10 - Exibir relatório");
            Console.WriteLine("5 - Consultar material\t\t11 - Atualizar e-mail de usuário");
            Console.WriteLine("6 - Consultar usuário\t\t12 - Simular devolução com datas futuras");
            Console.WriteLine("0 - Sair da Execução");
            Console.WriteLine("*************************************************");
            return LerNumeroInteiro("Digite sua opção: ");
        }
    }
}