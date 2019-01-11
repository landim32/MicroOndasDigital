using MicroOndas.Events;
using MicroOndas.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace MicroOndas.BLL
{
    /// <summary>
    /// Regra de negócio para a leitura de arquivo
    /// </summary>
    public class MicroOndaArquivoBLL: MicroOndaBLL
    {
        private string aquecerPorArquivo(string arquivo, string textoFrio, ParamentroInfo paramentro, char caracter = '.') {
            validarParametro(paramentro);
            using (var sw = File.AppendText(arquivo))
            {
                StringBuilder textoPorSegundo = new StringBuilder();
                for (var i = 1; i <= paramentro.Tempo.TotalSeconds; i++)
                {
                    textoPorSegundo.Clear();
                    for (var p = 0; p < paramentro.Potencia; p++)
                    {
                        textoPorSegundo.Append(caracter);
                    }
                    sw.Write(textoPorSegundo.ToString());
                    aoProcessar(textoPorSegundo.ToString());
                    Thread.Sleep(1000);
                }
            }
            var textoAquecido = File.ReadAllText(arquivo);
            aoConcluir(textoAquecido);
            return textoAquecido;
        }

        public override string aquecer(string textoFrio, ParamentroInfo paramentro, char caracter = '.')
        {
            var textoAquecido = string.Empty;
            // Caso o texto seja o caminho de um arquivo, executa da rotina 'aquecerPorArquivo'
            if (File.Exists(textoFrio))
            {
                // Le o conteúdo do arquivo original
                var verdadeiroTextoFrio = File.ReadAllText(textoFrio);
                textoAquecido = aquecerPorArquivo(textoFrio, verdadeiroTextoFrio, paramentro, caracter);
            }
            // Caso contrário executa o normal
            else
            {
                textoAquecido = base.aquecer(textoFrio, paramentro, caracter);
            }
            return textoAquecido;
        }
    }
}
