namespace Biblioteca_Escolar
{
    interface IEmprestavel
    {
        bool Emprestar();
        void Devolver();
    }

    abstract class MaterialBiblioteca : IEmprestavel
    {
        public string Codigo { get; init; }
        public string Titulo { get; init; }
        public int Ano { get; init; }
        public bool Disponivel { get; private set; }

        protected MaterialBiblioteca(string codigo, string titulo, int ano)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("O código é obrigatório.", nameof(codigo));

            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título é obrigatório.", nameof(titulo));

            if (ano <= 0 || ano > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(ano), "O ano deve ser positivo e não pode estar no futuro.");

            Codigo = codigo;
            Titulo = titulo;
            Ano = ano;
            Disponivel = true;
        }

        public bool Emprestar()
        {
            if (!Disponivel)
                return false;

            Disponivel = false;
            return true;
        }

        public void Devolver()
        {
            Disponivel = true;
        }

        public abstract int ObterPrazoEmDias();
        public abstract double ObterMultaPorDia();

        public override string ToString()
        {
            string status = Disponivel ? "Disponível" : "Emprestado";
            return $"[{Codigo}] {Titulo} ({Ano}) - {status}";
        }
    }

    class Livro : MaterialBiblioteca
    {
        public string Autor { get; init; }

        public Livro(string codigo, string titulo, int ano, string autor)
        : base(codigo, titulo, ano)
        {
            if (string.IsNullOrWhiteSpace(autor))
                throw new ArgumentException("A autoria do livro é obrigatória.", nameof(autor));
            Autor = autor;
        }
        public override int ObterPrazoEmDias() => 7;
        public override double ObterMultaPorDia() => 1.50;

        public override string ToString() => $"[Livro] {base.ToString()} | Autor(a): {Autor}";
    }

    class Revista : MaterialBiblioteca
    {
        public int NumeroEdicao { get; init; }

        public Revista(string codigo, string titulo, int ano, int numeroEdicao)
        : base(codigo, titulo, ano)
        {
            if (numeroEdicao <= 0)
                throw new ArgumentOutOfRangeException(nameof(numeroEdicao), "O número da edição deve ser positivo.");

            NumeroEdicao = numeroEdicao;
        }
        public override int ObterPrazoEmDias() => 3;
        public override double ObterMultaPorDia() => 1.00;

        public override string ToString() => $"[Revista] {base.ToString()} | Edição: {NumeroEdicao}";
    }
}
