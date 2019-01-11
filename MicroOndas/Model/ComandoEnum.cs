using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.Model
{
    /// <summary>
    /// Possíveis comandos no menu inicial
    /// </summary>
    public enum ComandoEnum
    {
        Raiz = '0',
        AquecimentoNormal = '1',
        AquecimentoRapido = '2',
        ListarProgramacao = '3',
        NovaProgramacao = '4',
    }
}
