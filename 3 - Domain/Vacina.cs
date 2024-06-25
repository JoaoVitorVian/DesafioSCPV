using CadastroPostosVacinacao.Domain.Common;

namespace CadastroPostosVacinacao.Domain.Entities
{
    public class Vacina : BaseEntity
    {
        public Vacina(string nome, string fabricante, string lote, int quantidade, DateTime dataValidade, long postoDeVacinacaoId, PostoDeVacinacao postoDeVacinacao)
        {
            Nome = nome;
            Fabricante = fabricante;
            Lote = lote;
            Quantidade = quantidade;
            DataValidade = dataValidade;
            PostoDeVacinacaoId = postoDeVacinacaoId;
            PostoDeVacinacao = postoDeVacinacao;
            _errors = new List<string>();
        }

        public Vacina() { }

        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string Lote { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public long? PostoDeVacinacaoId { get; set; }
        public PostoDeVacinacao PostoDeVacinacao { get; set; }
    }
}
