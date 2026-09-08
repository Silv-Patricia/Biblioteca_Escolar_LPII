using System.ComponentModel;
using System.Runtime.Intrinsics.X86;

namespace Biblioteca_Escolar {
    class Biblioteca {
        private List<Usuario> Usuarios = new List<Usuario>();
        private List<MaterialBiblioteca> Materiais = new List<MaterialBiblioteca>();
        private List<Emprestimo> Emprestimos = new List<Emprestimo>();

        public void CadastrarUsuario() {
            string matricula = Menu.LerStringObrigatoria("Digite sua matricula: ");

            if (UsuarioExiste(matricula) != null) {
                Console.WriteLine("\nErro: Já existe um usuário cadastrado com esta matrícula!");
                return;
            }

            string nome = Menu.LerStringObrigatoria("Digite o nome: ");
            string email = Menu.LerStringObrigatoria("Digite o e-mail: ");

            try {
                Usuario novoUsuario = new Usuario(matricula, nome, email);
                Usuarios.Add(novoUsuario);

                Console.WriteLine("\nUsuário cadastrado com sucesso!");
            }
            catch (ArgumentException erro) {
                Console.WriteLine($"\nErro ao cadastrar: {erro.Message}");
            }
        }

        public void AtualizaEmailDeUsuario() {
            if (ListaVazia(Usuarios)) {
                Console.WriteLine("Nenhum usuário cadastrado!");
                return;
            }

            string matricula = Menu.LerStringObrigatoria("Digite a matrícula do usuário: ");
            Usuario? usuario = UsuarioExiste(matricula);

            if (usuario != null) {
                Console.WriteLine(usuario);
                string novoEmail = Menu.LerStringObrigatoria("Digite o novo e-mail: ");
                try {
                    usuario.AtualizarEmail(novoEmail);
                    Console.WriteLine("\nE-mail atualizado com sucesso!");
                    return;
                }
                catch (ArgumentException erro) {
                    Console.WriteLine($"\nErro ao atualizar e-mail: {erro.Message}");
                }
            }
            Console.WriteLine($"\nUsuário com a matrícula: \"{matricula}\" não encontrado!");
        }

        public void CadastrarMaterial() {

            Console.WriteLine("\n****** Qual tipo de material deseja cadastrar? ******");
            Console.WriteLine("1 - Livro");
            Console.WriteLine("2 - Revista");
            int opc = Menu.LerNumeroInteiro("Digite sua opção: ");

            while (opc != 1 && opc != 2) {
                opc = Menu.LerNumeroInteiro("Digite sua opção: ");
            }

            string codigo = Menu.LerStringObrigatoria("Digite o código do material: ");

            if (MaterialExiste(codigo) != null) {
                Console.WriteLine("\nErro: Já existe um material cadastrado com este código!");
                return;
            }

            string titulo = Menu.LerStringObrigatoria("Digite o título: ");

            int ano = Menu.LerNumeroInteiro("Digite o ano de publicação: ");
            while (ano <= 0 || ano > DateTime.Now.Year) {
            Console.WriteLine("\nO ano deve ser maior que zero e menor ou igual ao atual!");
            ano = Menu.LerNumeroInteiro("Digite o ano de publicação: ");
}

            try {
                if (opc == 1) {
                    string autor = Menu.LerStringObrigatoria("Digite o nome do autor: ");

                    Livro novoLivro = new Livro(codigo, titulo, ano, autor);
                    Materiais.Add(novoLivro);
                    Console.WriteLine("\nLivro cadastrado com sucesso!");
                }
                else {
                    int numeroEdicao = Menu.LerNumeroInteiro("Digite o número da Edição: ");
                    while (numeroEdicao <= 0) {
                        Console.WriteLine("\nO número da edição deve ser positivo.");
                        numeroEdicao = Menu.LerNumeroInteiro("Digite o número da Edição: ");
                    }

                    Revista novaRevista = new Revista(codigo, titulo, ano, numeroEdicao);
                    Materiais.Add(novaRevista);
                    Console.WriteLine("\nRevista cadastrada com sucesso!");
                }
            }
            catch (ArgumentException erro) {
                Console.WriteLine($"\nErro ao cadastrar: {erro.Message}");
            }
        }

        private static bool ListaVazia<T>(List<T> lista) {
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

            int largMatricula = Math.Max(12, Usuarios.Max(u => u.Matricula.Length));
            int largNome = Math.Max(20, Usuarios.Max(u => u.Nome.Length));

            Console.WriteLine($"\n{"MATRÍCULA".PadRight(largMatricula)} | {"NOME".PadRight(largNome)} | E-MAIL");
            Console.WriteLine(new string('-', largMatricula + largNome + 20));

            foreach (Usuario u in Usuarios) {
                Console.WriteLine($"{u.Matricula.PadRight(largMatricula)} | {u.Nome.PadRight(largNome)} | {u.Email}");
            }
        }
        public void ListarMateriais() {
            if (ListaVazia(Materiais)) {
                Console.WriteLine("Nenhum material cadastrado!");
                return;
            }

            int largCodigo = Math.Max(10, Materiais.Max(m => m.Codigo.Length));
            int largTitulo = Math.Max(25, Materiais.Max(m => m.Titulo.Length));

            Console.WriteLine($"\n{"CÓDIGO".PadRight(largCodigo)} | {"TÍTULO".PadRight(largTitulo)} | ANO  | STATUS     | DETALHES");
            Console.WriteLine(new string('-', largCodigo + largTitulo + 45));

            foreach (MaterialBiblioteca mb in Materiais) {
                string status = mb.Disponivel ? "Disponível" : "Emprestado";
                string detalhes = mb is Livro l ? $"Autor(a): {l.Autor}" : $"Edição: {((Revista)mb).NumeroEdicao}";
                string tipo = mb is Livro ? "[Livro]" : "[Revista]";

                // O alinhamento negativo ,-4 e ,-10 formata as strings com tamanho fixo
                Console.WriteLine($"{mb.Codigo.PadRight(largCodigo)} | {mb.Titulo.PadRight(largTitulo)} | {mb.Ano,-4} | {status,-10} | {tipo} {detalhes}");
            }
        }

        public void ConsultarMaterialPorCodigo() {
            if (ListaVazia(Materiais)) {
                Console.WriteLine("Nenhum material cadastrado!");
                return;
            }

            string codigoBusca = Menu.LerStringObrigatoria("Escreva o código do material que deseja buscar: ");
            MaterialBiblioteca? materialEncontrado = MaterialExiste(codigoBusca);

            if (materialEncontrado != null) {
                Console.WriteLine($"\nMaterial encontrado!\n{materialEncontrado}");
            }
            else {
                Console.WriteLine($"\nLivro com código: \"{codigoBusca}\" não encontrado!");
            }
        }

        public void ConsultarUsuarioPorCodigo() {
            if (ListaVazia(Usuarios)) {
                Console.WriteLine("Nenhum usuário cadastrado!");
                return;
            }

            string matricula = Menu.LerStringObrigatoria("Escreva o matrícula do usuário que deseja buscar: ");

            Usuario? usuarioEncontrado = UsuarioExiste(matricula);

            if (usuarioEncontrado != null) {
                Console.WriteLine($"\nUsuário encontrado!\n{usuarioEncontrado}");
            }
            else {
                Console.WriteLine($"\nUsuário com a matrícula: \"{matricula}\" não encontrado!");
            }
        }
        private bool PodeRealizarEmprestimo(Usuario usuario) {
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

        private Usuario? UsuarioExiste(string matriculausuario) {
            foreach (Usuario u in Usuarios) {
                if (u.Matricula == matriculausuario) {
                    return u;
                }
            }
            return null;
        }

        private MaterialBiblioteca? MaterialExiste(string codigoBusca) {
            foreach (MaterialBiblioteca mb in Materiais) {
                if (mb.Codigo == codigoBusca) {
                    return mb;
                }
            }
            return null;
        }

        public void RealizarEmprestimo() {
            if (ListaVazia(Materiais)) {
                Console.WriteLine("Nenhum material cadastrado!");
                return;
            }
            if (ListaVazia(Usuarios)) {
                Console.WriteLine("Nenhum usuário cadastrado!");
                return;
            }

            string matriculausuario = Menu.LerStringObrigatoria("Escreva a matrícula de quem vai pegar um livro: ");
            Usuario? usuarioEncontrado = UsuarioExiste(matriculausuario);

            if (usuarioEncontrado == null) {
                Console.WriteLine($"\nUsuário com a matrícula \"{matriculausuario}\"não existe!");
                return;
            }

            string codigoBusca = Menu.LerStringObrigatoria("Escreva o código do material que deseja pedir empréstimo: ");
            MaterialBiblioteca? materialEncontrado = MaterialExiste(codigoBusca);

            if (materialEncontrado == null) {
                Console.WriteLine($"\nMaterial com o código \"{codigoBusca}\" não existe!");
                return;
            }

            if (PodeRealizarEmprestimo(usuarioEncontrado)) {
                if (materialEncontrado.Emprestar()) {
                    DateTime? dataInformada = Menu.LerDataAtualOuPassada("Digite a data do empréstimo (DD/MM/AAAA) ou pressione Enter para usar a data de hoje: ");

                    Emprestimo novoEmprestimo = new Emprestimo(usuarioEncontrado, materialEncontrado, dataInformada);

                    Emprestimos.Add(novoEmprestimo);

                    Console.WriteLine("\nEmpréstimo realizado com sucesso!");
                    Console.WriteLine($"Usuário: {usuarioEncontrado.Nome} ({usuarioEncontrado.Matricula})");
                    Console.WriteLine($"Material: {materialEncontrado.Titulo} ({materialEncontrado.Codigo})");
                    Console.WriteLine($"Devolução prevista para: {novoEmprestimo.DataDevolucaoPrevista}");
                }
                else {
                    Console.WriteLine($"\nOperação negada, O material \"{materialEncontrado.Titulo}\" já se encontra emprestado no momento.");
                }
            }
        }

        public void RealizarDevolucao() {
            if (ListaVazia(Emprestimos)) {
                Console.WriteLine("Nenhum empréstimo registrado!");
                return;
            }

            string codigoBusca = Menu.LerStringObrigatoria("Digite o código do material emprestado: ");
            MaterialBiblioteca? material = MaterialExiste(codigoBusca);

            if (material == null) {
                Console.WriteLine($"\nMaterial com o código \"{codigoBusca}\" não encontrado!");
                return;
            }

            if (material.Disponivel) {
                Console.WriteLine($"\nO material \"{material.Titulo}\" já consta como disponível na biblioteca.");
                return;
            }

            Emprestimo? emprestimoAtivo = null;
            foreach (Emprestimo emp in Emprestimos) {
                if (emp.MaterialBiblioteca.Codigo == material.Codigo && emp.DataDevolucaoReal == null) {
                    emprestimoAtivo = emp;
                    break;
                }
            }

            if (emprestimoAtivo == null) {
                Console.WriteLine("\nErro crítico: Material consta como indisponível, mas nenhum empréstimo ativo foi encontrado.");
                return;
            }

            Console.WriteLine($"\nDevolvendo: {emprestimoAtivo.MaterialBiblioteca.Codigo} - {emprestimoAtivo.MaterialBiblioteca.Titulo} (Usuário: {emprestimoAtivo.Usuario.Nome} - {emprestimoAtivo.Usuario.Matricula})");

            DateTime? dataInformada = Menu.LerDataAtualOuPassada("Digite a data de devolução (DD/MM/AAAA) ou pressione Enter para usar a data de hoje: ");

            if (emprestimoAtivo.RegistrarDevolucao(dataInformada)) {
                Console.WriteLine($"\nMaterial devolvido com sucesso!");
            }
        }

        public void TestarFuturaDataDeDevolucao() {
            if (ListaVazia(Emprestimos)) {
                Console.WriteLine("Nenhum empréstimo registrado!");
                return;
            }

            string codigoBusca = Menu.LerStringObrigatoria("Digite o código do material emprestado que deseja testar: ");
            MaterialBiblioteca? material = MaterialExiste(codigoBusca);

            if (material == null) {
                Console.WriteLine($"\nMaterial com o código \"{codigoBusca}\" não encontrado!");
                return;
            }

            if (material.Disponivel) {
                Console.WriteLine($"\nO material \"{material.Titulo}\" já consta como disponível na biblioteca.");
                return;
            }

            Emprestimo? emprestimoAtivo = null;
            foreach (Emprestimo emp in Emprestimos) {
                if (emp.MaterialBiblioteca.Codigo == material.Codigo && emp.DataDevolucaoReal == null) {
                    emprestimoAtivo = emp;
                    break;
                }
            }

            if (emprestimoAtivo == null) {
                Console.WriteLine("\nErro crítico: Material consta como indisponível, mas nenhum empréstimo ativo foi encontrado.");
                return;
            }

            Console.WriteLine($"\nTestando devolução para: {emprestimoAtivo.MaterialBiblioteca.Codigo} - {emprestimoAtivo.MaterialBiblioteca.Titulo} (Usuário: {emprestimoAtivo.Usuario.Nome} - {emprestimoAtivo.Usuario.Matricula})");

            DateTime? dataInformada = Menu.LerQualquerData("Digite a data simulada (DD/MM/AAAA) ou pressione Enter para usar a data de hoje: ");

            try {
                double multa = emprestimoAtivo.CalcularMulta(dataInformada);
                int diasAtraso = emprestimoAtivo.CalcularAtraso(dataInformada);

                Console.WriteLine($"\n--- RESULTADO DA SIMULAÇÃO ---");
                Console.WriteLine($"Dias de atraso projetados: {diasAtraso}");
                Console.WriteLine($"Multa projetada: R${multa:F2}");
            }
            catch (ArgumentOutOfRangeException erro) {
                Console.WriteLine($"\nErro na simulação: {erro.Message}");
            }
        }

        public void ExibirEmprestimosAtivos() {
            if (ListaVazia(Emprestimos)) {
                Console.WriteLine("Nenhum empréstimo registrado!");
                return;
            }

            var ativos = Emprestimos.Where(e => e.DataDevolucaoReal == null).ToList();
            if (ativos.Count == 0) {
                Console.WriteLine("Nenhum empréstimo ativo no momento!");
                return;
            }

            int largUsuario = Math.Max(15, ativos.Max(e => e.Usuario.Nome.Length));
            int largMaterial = Math.Max(20, ativos.Max(e => e.MaterialBiblioteca.Titulo.Length));

            Console.WriteLine($"\n{"USUÁRIO".PadRight(largUsuario)} | {"MATERIAL".PadRight(largMaterial)} | RETIRADA   | PRAZO      | SITUAÇÃO");
            Console.WriteLine(new string('-', largUsuario + largMaterial + 45));

            foreach (Emprestimo em in ativos) {
                string retirada = em.DataEmprestimo.ToString("dd/MM/yyyy");
                string prazo = em.DataDevolucaoPrevista.ToString("dd/MM/yyyy");
                Console.WriteLine($"{em.Usuario.Nome.PadRight(largUsuario)} | {em.MaterialBiblioteca.Titulo.PadRight(largMaterial)} | {retirada} | {prazo} | {em.Situacao}");
            }
        }

        public void GerarRelatorio() {
            Console.WriteLine("-------- RELATÓRIO --------");
            Console.WriteLine($"Quantidade de usuários: {Usuarios.Count}");
            int quantidadeLivros = 0, quantidadeRevistas = 0, materiaisDisponiveis = 0;
            foreach (MaterialBiblioteca mb in Materiais) {
                if (mb is Livro) {
                    quantidadeLivros++;
                }
                else {
                    quantidadeRevistas++;
                }
                if (mb.Disponivel) {
                    materiaisDisponiveis++;
                }
            }
            Console.WriteLine($"Quantidade de Livros: {quantidadeLivros}");
            Console.WriteLine($"Quantidade de Revistas: {quantidadeRevistas}");
            Console.WriteLine($"Quantidade de materiais disponíveis: {materiaisDisponiveis}");

            int emprestimosAtivos = 0, devolucoesAtrasadas = 0;
            double valorTotalMultas = 0;
            foreach (Emprestimo em in Emprestimos) {
                if (em.DataDevolucaoReal == null) {
                    emprestimosAtivos++;
                }
                if (em.DataDevolucaoPrevista < DateTime.Now.Date && em.DataDevolucaoReal == null) {
                    devolucoesAtrasadas++;
                }
                valorTotalMultas += em.CalcularMulta();
            }
            Console.WriteLine($"Quantidade de empréstimos ativos: {emprestimosAtivos}");
            Console.WriteLine($"Valor total de multas: R${valorTotalMultas:F2}");
            Console.WriteLine("---------------------------");
        }
    }
}