using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura
{
    public class lcampo
    {
        public string nome;
        public string valor;
    }

    public class llinha : List<lcampo>
    {
    }

    public class ltabela : List<llinha>
    {
        public void Add(object v)
        {
            throw new NotImplementedException();
        }
    }
}
