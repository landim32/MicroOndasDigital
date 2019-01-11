using MicroOndas.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.Factory
{
    public static class ProgramacaoFactory
    {
        private static ProgramacaoBLL _programacao = null;

        public static ProgramacaoBLL create() {
            if (_programacao == null) {
                _programacao = new ProgramacaoBLL();
            }
            return _programacao;
        }
    }
}
