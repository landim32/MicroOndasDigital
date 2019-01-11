using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.Events
{
    public class MicroOndaConcluirEventArgs: EventArgs
    {
        public string TextoAquecido { get; set; }
    }
}
