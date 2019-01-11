using MicroOndas.BLL;
using MicroOndas.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MicroOndas.View
{
    /// <summary>
    /// Base da Interface do usuário
    /// </summary>
    public abstract class MicroOndaBaseCmd
    {
        protected abstract string processarComando(string textoFrio, ComandoEnum comando = ComandoEnum.Raiz);

        public ComandoEnum capturarComando()
        {
            Console.WriteLine("Escolha um dos comando abaixo:");
            exibirComando();
            var retorno = (ComandoEnum)Console.ReadKey().KeyChar;
            Console.WriteLine();
            return retorno;
        }

        protected string capturarTexto()
        {
            Console.Write("Texto a ser aquecido: ");
            return Console.ReadLine();
        }

        protected int capturarPotencia()
        {
            Console.Write("Informe a potência (1 à 10): ");
            int potencia = 0;
            if (!int.TryParse(Console.ReadLine(), out potencia))
            {
                Console.WriteLine("AVISO: Valor de pontência inválido.");
                return capturarPotencia();
            }
            return potencia;
        }

        protected TimeSpan capturarTempo()
        {
            Console.Write("Informe o tempo (mm:ss): ");
            TimeSpan tempo;
            if (!TimeSpan.TryParseExact(Console.ReadLine(), "mm\\:ss", CultureInfo.CurrentCulture, out tempo))
            {
                Console.WriteLine("AVISO: Tempo inválido.");
                return capturarTempo();
            }
            return tempo;
        }

        protected virtual void exibirComando() {
            Console.WriteLine("1 - AQUECIMENTO NORMAL");
            Console.WriteLine("2 - AQUECIMENTO RÁPIDO (30 segundos, Potência 8)");
        }

        public virtual void executar(string textoFrio = null) {
            if (string.IsNullOrEmpty(textoFrio)) {
                textoFrio = capturarTexto();
            }
            string textoAquecido = processarComando(textoFrio);
            Console.WriteLine("Pressione tecla para sair...");
            Console.ReadKey();
        }
    }
}
