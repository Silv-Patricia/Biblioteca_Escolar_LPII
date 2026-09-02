namespace Biblioteca_Escolar
{
    class Usuario
    {
        public string Matricula { get; init; }
        public string Nome { get; init; }
        public string Email { get; private set; }

        public void AtualizarEmail(string novoEmail)
        {
            if (string.IsNullOrWhiteSpace(novoEmail))
                throw new ArgumentException("O e-mail não pode ser vazio.", nameof(novoEmail));

            Email = novoEmail;
        }

        public Usuario(string matricula, string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("A matrícula é obrigatória.", nameof(matricula));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome é obrigatório.", nameof(nome));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O e-mail é obrigatório.", nameof(email));

            Matricula = matricula;
            Nome = nome;
            Email = email;
        }

        public override string ToString()
        {
            return $"Matrícula: {Matricula} | Nome: {Nome} | E-mail: {Email}";
        }
    }
}
