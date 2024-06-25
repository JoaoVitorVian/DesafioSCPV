using Application.DTO.Vacina;

namespace Application.DTO.PostoDeVacinacao
{
    public class PostoDeVacinacaoDTO
    {
        public PostoDeVacinacaoDTO()
        { }

        public PostoDeVacinacaoDTO(long id, string nome, List<VacinaDTO> vacinas, string endereco, string cidade, string estado)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            Cidade = cidade;
            Estado = estado;

            Vacinas = vacinas;
        }

        public long Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }

        public string Estado { get; set; }

        public List<VacinaDTO> Vacinas { get; set; }
    }
}
