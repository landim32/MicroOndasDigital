using MicroOndas.BLL;
using MicroOndas.Factory;
using MicroOndas.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MicroOndas.View
{
    public class MicroOndaNivel1Cmd: MicroOndaBaseCmd
    {

        protected override string processarComando(string textoFrio, ComandoEnum comando = ComandoEnum.Raiz)
        {
            if (comando == ComandoEnum.Raiz) {
                comando = capturarComando();
            }
            string textoAquecido = string.Empty;
            switch (comando)
            {
                case ComandoEnum.AquecimentoNormal:
                    textoAquecido = MicroOndaFactory.create().aquecer(textoFrio, capturarParametroAquecimentoNormal());
                    break;
                case ComandoEnum.AquecimentoRapido:
                    textoAquecido = MicroOndaFactory.create().aquecer(textoFrio, capturarParametroAquecimentoRapido());
                    break;
            }
            return textoAquecido;
        }

        protected ParamentroInfo capturarParametroAquecimentoRapido()
        {
            return new ParamentroInfo
            {
                Potencia = 8,
                Tempo = TimeSpan.FromSeconds(30)
            };
        }

        protected ParamentroInfo capturarParametroAquecimentoNormal() {
            var param = new ParamentroInfo {
                Potencia = capturarPotencia(),
                Tempo = capturarTempo()
            };
            return param;
        }
    }
}
