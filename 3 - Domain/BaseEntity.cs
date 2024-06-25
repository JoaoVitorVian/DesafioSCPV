using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPostosVacinacao.Domain.Common
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        internal List<String> _errors;
        public IReadOnlyCollection<String> Errors => _errors;
    }
}
