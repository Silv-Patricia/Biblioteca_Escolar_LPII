namespace Biblioteca_Escolar {

    class Emprestimo {
        public Usuario Usuario { get; private set; }
        public MaterialBiblioteca MaterialBiblioteca { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public string Situacao {
            get {
                if (DataDevolucaoReal != null) return "Devolvido";

                return DateTime.Now > DataDevolucaoPrevista ? "Atrasado" : "Em dia";
            }
        }

        public Emprestimo(Usuario usuario, MaterialBiblioteca materialBiblioteca) {
            Usuario = usuario;
            MaterialBiblioteca = materialBiblioteca;
            DataEmprestimo = DateTime.Now;

            int prazo = materialBiblioteca.ObterPrazoEmDias();
            DataDevolucaoPrevista = DataEmprestimo.AddDays(prazo);
        }

        public int CalcularAtraso(DateTime? dataInformada = null) {
            DateTime dataParaCalcular;

            if (DataDevolucaoReal != null) {
                dataParaCalcular = DataDevolucaoReal.Value;
            }
            else if (dataInformada != null) {
                dataParaCalcular = dataInformada.Value;
            }
            else {
                dataParaCalcular = DateTime.Now;
            }

            if (dataParaCalcular <= DataDevolucaoPrevista) {
                return 0;
            }
            TimeSpan tempoDeAtraso = dataParaCalcular - DataDevolucaoPrevista;
            return tempoDeAtraso.Days;
        }

        public double CalcularMulta() {
            int diasAtraso = CalcularAtraso();
            if (diasAtraso != 0) {
                return diasAtraso * MaterialBiblioteca.ObterMultaPorDia();
            }
            return 0;
        }

        public bool RegistrarDevolucao(DateTime? dataInformada = null) {
            if (DataDevolucaoReal != null) {
                Console.WriteLine("Erro: Este empréstimo já foi encerrado.");
                return false;
            }
            if (dataInformada != null) {
                DataDevolucaoReal = dataInformada.Value;
            }
            else {
                DataDevolucaoReal = DateTime.Now;
            }

            int diasAtraso = CalcularAtraso();
            double multa = CalcularMulta();

            if (diasAtraso > 0) {
                Console.WriteLine($"Atraso de {diasAtraso} dias.");
                Console.WriteLine($"Multa a pagar: R${multa:F2}.");
            }
            else {
                Console.WriteLine($"Devolução realizada dentro do prazo! Você não pagará nenhuma multa!");
            }

            MaterialBiblioteca.Devolver();

            return true;
        }
        public override string ToString() {
            string dataRetiradaFormatada = DataEmprestimo.ToString("dd/MM/yyyy");
            string prazoFormatado = DataDevolucaoPrevista.ToString("dd/MM/yyyy");

            return $"Usuário: {Usuario.Nome} ({Usuario.Matricula}) | Material: {MaterialBiblioteca.Titulo} ({MaterialBiblioteca.Codigo}) | Retirada: {dataRetiradaFormatada} | Prazo: {prazoFormatado} | Situação: {Situacao}";
        }
    }
}
