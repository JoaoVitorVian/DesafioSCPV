using SCPV.Presentation.WebAPI.ViewModel.VacinaViewModel;

namespace SCPV.Presentation.WebAPI.ViewModel.PostoViewModel
{
    public class UpdatePostoDeVacinacaoViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }

        public string Estado { get; set; }
    }
}
