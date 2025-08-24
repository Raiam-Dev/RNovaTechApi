using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automacao.Domain
{
    public class Dispositivo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public Status Status { get; set; }

    }
    public enum Status
    {
        Ligado,
        Desligado,
        EmEspera
    }
}
