using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.Model
{
    /// <summary>
    /// Paramentros de execução do aquecimento
    /// </summary>
    public class ParamentroInfo
    {
        public TimeSpan Tempo { get; set; }
        public int Potencia { get; set; } = 10;
    }
}
