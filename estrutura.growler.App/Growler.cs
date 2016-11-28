using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using estrutura.growler;

namespace estrutura.growler.App
{
    public class Growler : EstruturaBase
    {
        public String Id;
        public String Temperatura;
        public String Bateria;
        public String DatahoraAtualizacao;

        public Growler()
        {
            Id = "null";
            Temperatura = "0";
            Bateria = "0";
            DatahoraAtualizacao = "null";
        }

        public Growler(String id, String temperatura, String bateria, String datahoraAtualizacao)
        {
            this.Id = id;
            this.Temperatura = temperatura;
            this.Bateria = bateria;
            this.DatahoraAtualizacao = datahoraAtualizacao;
        }

        public String toString()
        {
            return ("Id: " + Id + ", " + "Temperatura: " + Temperatura);
        }
    }

}
