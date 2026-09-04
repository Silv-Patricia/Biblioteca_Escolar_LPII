namespace Biblioteca_Escolar {
    class Biblioteca {
        private List<Usuario> Usuarios = new List<Usuario>();
        private List<MaterialBiblioteca> Materiais = new List<MaterialBiblioteca>();
        private List<Emprestimo> Emprestimos = new List<Emprestimo>();

        public void CadastrarUsuario() {
            String matricula, nome, email;
            Console.WriteLine("Digite sua matricula:");
            matricula = Console.ReadLine() ?? " ";

            foreach (Usuario u in Usuarios) {
                if (u.Matricula == matricula) {
                    Console.WriteLine("Erro: Já existe um usuário cadastrado com esta matrícula!");
                    return;
                }
            }

            Console.Write("Digite o nome: ");
            nome = Console.ReadLine() ?? "";

            Console.Write("Digite o e-mail: ");
            email = Console.ReadLine() ?? "";

            try {
                Usuario novoUsuario = new Usuario(matricula, nome, email);
                Usuarios.Add(novoUsuario);

                Console.WriteLine("Usuário cadastrado com sucesso!");
            }
            catch (ArgumentException erro) {
                Console.WriteLine($"Erro ao cadastrar: {erro.Message}");
            }

        }

        public void CadastrarMaterial() {
            int opc, ano;
            string codigo, titulo;

            while (true) {
                Console.WriteLine("\n****** Qual tipo de material deseja cadastrar? ******");
                Console.WriteLine("1 - Livro");
                Console.WriteLine("2 - Revista");
                Console.Write("Opção: ");
                string op = Console.ReadLine() ?? "";

                if (int.TryParse(op, out opc) && (opc == 1 || opc == 2)) {
                    break;
                }
                Console.WriteLine("Digite um valor válido!");
            }

            Console.Write("Digite o código do material: ");
            codigo = Console.ReadLine() ?? "";

            foreach (MaterialBiblioteca mb in Materiais) {
                if (mb.Codigo == codigo) {
                    Console.WriteLine("Erro: Já existe um material cadastrado com este código!");
                    return;
                }
            }
            Console.Write("Digite o título: ");
            titulo = Console.ReadLine() ?? "";

            Console.Write("Digite o ano de publicação: ");
            int.TryParse(Console.ReadLine(), out ano);

            try {
                if (opc == 1) {
                    Console.Write("Digite o nome do autor: ");
                    string autor = Console.ReadLine() ?? "";

                    Livro novoLivro = new Livro(codigo, titulo, ano, autor);
                    Materiais.Add(novoLivro);
                    Console.WriteLine("Livro cadastrado com sucesso!");
                }
                else {
                    Console.Write("Digite o numero da Edição: ");
                    int.TryParse(Console.ReadLine(), out int numeroEdicao);

                    Revista novaRevista = new Revista(codigo, titulo, ano, numeroEdicao);
                    Materiais.Add(novaRevista);
                    Console.WriteLine("Revista cadastrada com sucesso!");

                }
            }
            catch (ArgumentException erro) {
                Console.WriteLine($"Erro ao cadastrar: {erro.Message}");
            }

        }

        public bool ListaVazia<T>(List<T> lista) {
            if (lista.Count == 0) {
                return true;
            }
            return false;
        }

        public void ListarUsuarios() {
            if (ListaVazia(Usuarios)) {
                Console.WriteLine("Nenhum usuário cadastrado!");
                return;
            }
            foreach (Usuario u in Usuarios) {
                Console.WriteLine(u);
            }

        }
        public void ListarMateriais() {
            if (ListaVazia(Materiais)) {
                Console.WriteLine("Nenhum material Cadastrado!");
                return;
            }

            foreach (Usuario u in Usuarios) {
                Console.WriteLine(u);
            }
        }

        public void BuscarPorCodigo() {
            if (ListaVazia(Materiais)) {
                Console.WriteLine("Nenhum material Cadastrado!");
                return;
            }
            Console.Write("Escreva o código do material que deseja buscar: ");
            string codigoBusca = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(codigoBusca)) {
                throw new ArgumentException("\nDigite um código para busca!", nameof(codigoBusca));
            }
            else {
                foreach (MaterialBiblioteca mb in Materiais) {
                    if (mb.Codigo == codigoBusca) {
                        Console.WriteLine(mb);
                        return;
                    }
                Console.WriteLine($"Livro com código: \"{codigoBusca}\" não encontrado!");
                }
            }
        }

        
    }
}