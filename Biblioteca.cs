using System.ComponentModel;

namespace Biblioteca_Escolar {
    class Biblioteca {
        private List<Usuario> Usuarios = new List<Usuario>();
        private List<MaterialBiblioteca> Materiais = new List<MaterialBiblioteca>();
        private List<Emprestimo> Emprestimos = new List<Emprestimo>();

        public void CadastrarUsuario() {
            string matricula, nome, email;
            Console.WriteLine("Digite sua matricula: ");
            matricula = Menu.LeEntrada().Trim();

            foreach (Usuario u in Usuarios) {
                if (u.Matricula == matricula) {
                    Console.WriteLine("Erro: Já existe um usuário cadastrado com esta matrícula!");
                    return;
                }
            }

            Console.Write("Digite o nome: ");
            nome = Menu.LeEntrada().Trim();

            Console.Write("Digite o e-mail: ");
            email = Menu.LeEntrada().Trim();

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

            Console.WriteLine("\n****** Qual tipo de material deseja cadastrar? ******");
            Console.WriteLine("1 - Livro");
            Console.WriteLine("2 - Revista");
            int opc = Menu.LerInteiro("Digite sua opção: ");

            while (opc != 1 && opc != 2) {
                opc = Menu.LerInteiro("Digite sua opção: ");
            }

            Console.Write("Digite o código do material: ");
            string codigo = Menu.LeEntrada().Trim();

            foreach (MaterialBiblioteca mb in Materiais) {
                if (mb.Codigo == codigo) {
                    Console.WriteLine("Erro: Já existe um material cadastrado com este código!");
                    return;
                }
            }
            Console.Write("Digite o título: ");
            string titulo = Menu.LeEntrada().Trim();

            int ano = Menu.LerInteiro("Digite o ano de publicação: ");

            try {
                if (opc == 1) {
                    Console.Write("Digite o nome do autor: ");
                    string autor = Menu.LeEntrada().Trim();

                    Livro novoLivro = new Livro(codigo, titulo, ano, autor);
                    Materiais.Add(novoLivro);
                    Console.WriteLine("Livro cadastrado com sucesso!");
                }
                else {
                    int numeroEdicao = Menu.LerInteiro("Digite o numero da Edição: ");

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
                Console.WriteLine("Nenhum material cadastrado!");
                return;
            }

            foreach (MaterialBiblioteca mb in Materiais) {
                Console.WriteLine(mb);
            }
        }

        public void MostarBuscarPorCodigo() {
            if (ListaVazia(Materiais)) {
                Console.WriteLine("Nenhum material Cadastrado!");
                return;
            }
            Console.Write("Escreva o código do material que deseja buscar: ");
            string codigoBusca = Menu.LeEntrada().Trim();

            if (string.IsNullOrWhiteSpace(codigoBusca)) {
                throw new ArgumentException("\nDigite um código para busca!", nameof(codigoBusca));
            }
            else {
                foreach (MaterialBiblioteca mb in Materiais) {
                    if (mb.Codigo == codigoBusca) {
                        Console.WriteLine(mb);
                        return;
                    }
                }
                Console.WriteLine($"Livro com código: \"{codigoBusca}\" não encontrado!");
            }
        }
        public bool PodeRealizarEmprestimo(Usuario usuario) {
            int totalEmprestimosAtivos = 0;
            foreach (Emprestimo emp in Emprestimos) {
                if (emp.Usuario.Matricula == usuario.Matricula && emp.DataDevolucaoReal == null) {
                    totalEmprestimosAtivos++;
                }
            }

            if (totalEmprestimosAtivos >= 3) {
                Console.WriteLine($"\nOperação negada: O usuário {usuario.Nome} já possui 3 empréstimos ativos.");
                return false;
            }

            return true;
        }

        public Usuario? UsuarioExiste(string matriculausuario) {
            foreach (Usuario u in Usuarios) {
                if (u.Matricula == matriculausuario) {
                    return u;
                }
            }
            return null;
        }

        public MaterialBiblioteca? MaterialExiste(string codigoBusca) {
            foreach (MaterialBiblioteca mb in Materiais) {
                if (mb.Codigo == codigoBusca) {
                    return mb;
                }
            }
            return null;
        }

        // código em construção
        public void RealizarEmprestimo() {
            if (ListaVazia(Materiais)) {
                Console.WriteLine("Nenhum material Cadastrado!");
                return;
            }
            if (ListaVazia(Usuarios)) {
                Console.WriteLine("Nenhum usuário Cadastrado!");
                return;
            }

            MaterialBiblioteca? materialEncontrado = null;
            Usuario? usuarioEncontrado = null;

            Console.Write("Escreva a matrícula de quem vai pegar um livro: ");
            string matriculausuario = Menu.LeEntrada().Trim();
            usuarioEncontrado = UsuarioExiste(matriculausuario);

            if (usuarioEncontrado == null) {
                Console.WriteLine($"Usuário com a matrícula \"{matriculausuario}\"não existe!");
                return;
            }

            Console.Write("Escreva o código do material que deseja pedir emprestimo: ");
            string codigoBusca = Menu.LeEntrada().Trim();
            materialEncontrado = MaterialExiste(codigoBusca);

            if (materialEncontrado == null) {
                Console.WriteLine($"Material com o código \"{codigoBusca}\" não existe!");
                return;
            }

            if (PodeRealizarEmprestimo(usuarioEncontrado)) {
                // lógica para emprestrar o livro
                if (materialEncontrado.Emprestar()) {
                    Emprestimo novoEmprestimo = new Emprestimo(usuarioEncontrado, materialEncontrado);

                    Emprestimos.Add(novoEmprestimo);

                    Console.WriteLine("\nEmpréstimo realizado com sucesso!");
                    Console.WriteLine($"Material: {materialEncontrado.Titulo}");
                    Console.WriteLine($"Devolução prevista para: {novoEmprestimo.DataDevolucaoPrevista}");
                }
                else {
                    Console.WriteLine($"\nOperação negada, O material \"{materialEncontrado.Titulo}\" já se encontra emprestado no momento.");
                }
            }

        }
    }
}