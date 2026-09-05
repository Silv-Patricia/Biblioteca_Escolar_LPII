using System.ComponentModel;

namespace Biblioteca_Escolar
{
    class Biblioteca
    {
        private List<Usuario> Usuarios = new List<Usuario>();
        private List<MaterialBiblioteca> Materiais = new List<MaterialBiblioteca>();
        private List<Emprestimo> Emprestimos = new List<Emprestimo>();

        public void CadastrarUsuario()
        {
            string matricula = Menu.LerStringObrigatoria("Digite sua matricula: ");

            if (UsuarioExiste(matricula) != null)
            {
                Console.WriteLine("Erro: Já existe um usuário cadastrado com esta matrícula!");
                return;
            }

            string nome = Menu.LerStringObrigatoria("Digite o nome: ");
            string email = Menu.LerStringObrigatoria("Digite o e-mail: ");

            try
            {
                Usuario novoUsuario = new Usuario(matricula, nome, email);
                Usuarios.Add(novoUsuario);

                Console.WriteLine("Usuário cadastrado com sucesso!");
            }
            catch (ArgumentException erro)
            {
                Console.WriteLine($"Erro ao cadastrar: {erro.Message}");
            }
        }

        public void AtualizaEmailDeUsuario()
        {
            string matricula = Menu.LerStringObrigatoria("Digite a matrícula do usuário: ");
            Usuario? usuario = UsuarioExiste(matricula);

            if (usuario != null)
            {
                Console.WriteLine(usuario);
                string novoEmail = Menu.LerStringObrigatoria("Digite o novo e-mail: ");
                try
                {
                    usuario.AtualizarEmail(novoEmail);
                    Console.WriteLine("E-mail atualizado com sucesso!");
                    return;
                }
                catch (ArgumentException erro)
                {
                    Console.WriteLine($"Erro ao atualizar e-mail: {erro.Message}");
                }
            }
            Console.WriteLine($"Usuário com a matrícula: \"{matricula}\" não encontrado!");
        }

        public void CadastrarMaterial()
        {

            Console.WriteLine("\n****** Qual tipo de material deseja cadastrar? ******");
            Console.WriteLine("1 - Livro");
            Console.WriteLine("2 - Revista");
            int opc = Menu.LerNumeroInteiro("Digite sua opção: ");

            while (opc != 1 && opc != 2)
            {
                opc = Menu.LerNumeroInteiro("Digite sua opção: ");
            }

            string codigo = Menu.LerStringObrigatoria("Digite o código do material: ");

            if (MaterialExiste(codigo) != null)
            {
                Console.WriteLine("Erro: Já existe um material cadastrado com este código!");
                return;
            }

            string titulo = Menu.LerStringObrigatoria("Digite o título: ");

            int ano = Menu.LerNumeroInteiro("Digite o ano de publicação: ");
            while (ano > DateTime.Now.Year)
            {
                Console.WriteLine("O ano deve ser menor ou igual ao atual!");
                ano = Menu.LerNumeroInteiro("Digite o ano de publicação: ");
            }

            try
            {
                if (opc == 1)
                {
                    string autor = Menu.LerStringObrigatoria("Digite o nome do autor: ");

                    Livro novoLivro = new Livro(codigo, titulo, ano, autor);
                    Materiais.Add(novoLivro);
                    Console.WriteLine("Livro cadastrado com sucesso!");
                }
                else
                {
                    int numeroEdicao = Menu.LerNumeroInteiro("Digite o número da Edição: ");
                    while (numeroEdicao <= 0)
                    {
                        Console.WriteLine("O número da edição deve ser positivo.");
                        numeroEdicao = Menu.LerNumeroInteiro("Digite o número da Edição: ");
                    }

                    Revista novaRevista = new Revista(codigo, titulo, ano, numeroEdicao);
                    Materiais.Add(novaRevista);
                    Console.WriteLine("Revista cadastrada com sucesso!");
                }
            }
            catch (ArgumentException erro)
            {
                Console.WriteLine($"Erro ao cadastrar: {erro.Message}");
            }
        }

        public bool ListaVazia<T>(List<T> lista)
        {
            if (lista.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void ListarUsuarios()
        {
            if (ListaVazia(Usuarios))
            {
                Console.WriteLine("Nenhum usuário cadastrado!");
                return;
            }
            foreach (Usuario u in Usuarios)
            {
                Console.WriteLine(u);
            }

        }
        public void ListarMateriais()
        {
            if (ListaVazia(Materiais))
            {
                Console.WriteLine("Nenhum material cadastrado!");
                return;
            }

            foreach (MaterialBiblioteca mb in Materiais)
            {
                Console.WriteLine(mb);
            }
        }

        public void ConsultarMaterialPorCodigo()
        {
            if (ListaVazia(Materiais))
            {
                Console.WriteLine("Nenhum material cadastrado!");
                return;
            }

            string codigoBusca = Menu.LerStringObrigatoria("Escreva o código do material que deseja buscar: ");
            MaterialBiblioteca? materialEncontrado = MaterialExiste(codigoBusca);

            if (materialEncontrado != null)
            {
                Console.WriteLine(materialEncontrado);
            }
            else
            {
                Console.WriteLine($"Livro com código: \"{codigoBusca}\" não encontrado!");
            }
        }

        public void ConsultarUsuarioPorCodigo()
        {
            if (ListaVazia(Usuarios))
            {
                Console.WriteLine("Nenhum usuário cadastrado!");
                return;
            }

            string matricula = Menu.LerStringObrigatoria("Escreva o matrícula do usuário que deseja buscar: ");

            Usuario? usuarioEncontrado = UsuarioExiste(matricula);

            if (usuarioEncontrado != null)
            {
                Console.WriteLine(usuarioEncontrado);
            }
            else
            {
                Console.WriteLine($"Usuário com a matrícula: \"{matricula}\" não encontrado!");
            }
        }
        public bool PodeRealizarEmprestimo(Usuario usuario)
        {
            int totalEmprestimosAtivos = 0;
            foreach (Emprestimo emp in Emprestimos)
            {
                if (emp.Usuario.Matricula == usuario.Matricula && emp.DataDevolucaoReal == null)
                {
                    totalEmprestimosAtivos++;
                }
            }

            if (totalEmprestimosAtivos >= 3)
            {
                Console.WriteLine($"\nOperação negada: O usuário {usuario.Nome} já possui 3 empréstimos ativos.");
                return false;
            }

            return true;
        }

        public Usuario? UsuarioExiste(string matriculausuario)
        {
            foreach (Usuario u in Usuarios)
            {
                if (u.Matricula == matriculausuario)
                {
                    return u;
                }
            }
            return null;
        }

        public MaterialBiblioteca? MaterialExiste(string codigoBusca)
        {
            foreach (MaterialBiblioteca mb in Materiais)
            {
                if (mb.Codigo == codigoBusca)
                {
                    return mb;
                }
            }
            return null;
        }

        // código em construção
        public void RealizarEmprestimo()
        {
            if (ListaVazia(Materiais))
            {
                Console.WriteLine("Nenhum material Cadastrado!");
                return;
            }
            if (ListaVazia(Usuarios))
            {
                Console.WriteLine("Nenhum usuário Cadastrado!");
                return;
            }

            string matriculausuario = Menu.LerStringObrigatoria("Escreva a matrícula de quem vai pegar um livro: ");
            Usuario? usuarioEncontrado = UsuarioExiste(matriculausuario);

            if (usuarioEncontrado == null)
            {
                Console.WriteLine($"Usuário com a matrícula \"{matriculausuario}\"não existe!");
                return;
            }

            string codigoBusca = Menu.LerStringObrigatoria("Escreva o código do material que deseja pedir empréstimo: ");
            MaterialBiblioteca? materialEncontrado = MaterialExiste(codigoBusca);

            if (materialEncontrado == null)
            {
                Console.WriteLine($"Material com o código \"{codigoBusca}\" não existe!");
                return;
            }

            if (PodeRealizarEmprestimo(usuarioEncontrado))
            {
                // lógica para emprestrar o livro
                if (materialEncontrado.Emprestar())
                {
                    Emprestimo novoEmprestimo = new Emprestimo(usuarioEncontrado, materialEncontrado);

                    Emprestimos.Add(novoEmprestimo);

                    Console.WriteLine("\nEmpréstimo realizado com sucesso!");
                    Console.WriteLine($"Material: {materialEncontrado.Titulo}");
                    Console.WriteLine($"Devolução prevista para: {novoEmprestimo.DataDevolucaoPrevista}");
                }
                else
                {
                    Console.WriteLine($"\nOperação negada, O material \"{materialEncontrado.Titulo}\" já se encontra emprestado no momento.");
                }
            }
        }
    }
}