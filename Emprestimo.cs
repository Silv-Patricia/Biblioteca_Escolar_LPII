using System.Reflection.Metadata.Ecma335;

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

        public Emprestimo(Usuario usuario, MaterialBiblioteca materialBiblioteca, DateTime? dataInformada) {
            Usuario = usuario;
            MaterialBiblioteca = materialBiblioteca;
            if (dataInformada != null) {
                DataEmprestimo = dataInformada.Value;
            }
            else {
                DataEmprestimo = DateTime.Now;
            }

            int prazo = materialBiblioteca.ObterPrazoEmDias();
            DataDevolucaoPrevista = DataEmprestimo.AddDays(prazo);
        }

        public int CalcularAtraso(DateTime? dataInformada = null) {
            DateTime dataDevolucao;

            if (DataDevolucaoReal != null) {
                dataDevolucao = DataDevolucaoReal.Value;
            }
            else if (dataInformada != null) {
                dataDevolucao = dataInformada.Value;
            }
            else {
                dataDevolucao = DateTime.Now;
            }

            if (dataDevolucao <= DataDevolucaoPrevista) {
                return 0;
            }
            TimeSpan tempoDeAtraso = dataDevolucao - DataDevolucaoPrevista;
            return tempoDeAtraso.Days;
        }

        public double CalcularMulta(DateTime? dataInformada = null) {
            if (dataInformada < DataEmprestimo){
                throw new ArgumentOutOfRangeException("A data de devolução é menor que a data de empréstimo!");
            }
            int diasAtraso = CalcularAtraso(dataInformada);
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
            if (dataInformada < DataEmprestimo){
                Console.WriteLine("Erro: A data de devolução é menor que a data de empréstimo!");
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
