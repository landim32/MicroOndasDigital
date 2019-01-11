using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.Model
{
    public class ProgramacaoInfo: ParamentroInfo
    {
        public string Nome { get; set; }
        public string Instrucao { get; set; }
        public string AlimentoCompativel { get; set; }
        public char Caracter { get; set; } = '.';
    }
}
