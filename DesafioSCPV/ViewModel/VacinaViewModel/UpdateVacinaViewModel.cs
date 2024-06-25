namespace SCPV.Presentation.WebAPI.ViewModel.VacinaViewModel
{
    public class UpdateVacinaViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string Lote { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public long PostoDeVacinacaoId { get; set; }
    }
}
