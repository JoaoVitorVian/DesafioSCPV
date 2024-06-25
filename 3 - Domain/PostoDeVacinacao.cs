using CadastroPostosVacinacao.Domain.Common;

namespace CadastroPostosVacinacao.Domain.Entities
{
    public class PostoDeVacinacao : BaseEntity
    {
        public PostoDeVacinacao(string nome, string endereco, string cidade, string estado, ICollection<Vacina> vacinas)
        {
            Nome = nome;
            Endereco = endereco;
            Cidade = cidade;
            Estado = estado;
            Vacinas = new List<Vacina>(); 
            _errors = new List<string>();
        }

        public PostoDeVacinacao() { }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
        public ICollection<Vacina> Vacinas { get; set; }
    }
}
