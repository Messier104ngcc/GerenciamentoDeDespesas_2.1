namespace GerenciamentoDeDespesas_2._1.Models
{
    public class ViewModel
    {
        public string Bancos { get; set; }

        public Conta_Bancaria contas { get; set; }

        public IEnumerable<Conta_Bancaria> Contas { get; set; }
    }
}
