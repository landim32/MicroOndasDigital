using MicroOndas.Events;
using MicroOndas.Model;
using MicroOndas.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MicroOndas.BLL
{
    /// <summary>
    /// Regra principal do Micro Ondas
    /// </summary>
    public class MicroOndaBLL
    {
        public event EventHandler<MicroOndaProcessoEventArgs> AoProcessar;
        public event EventHandler<MicroOndaConcluirEventArgs> AoConcluir;
        public event EventHandler<MicroOndaErroEventArgs> AoErro;

        private void executarErro(string mensagem) {
            if (AoErro != null)
            {
                AoErro(this, new MicroOndaErroEventArgs
                {
                    Erro = mensagem
                });
            }
            else {
                throw new Exception(mensagem);
            }
        }

        protected void validarParametro(ParamentroInfo paramentro)
        {
            if (paramentro.Tempo == null)
            {
                executarErro("Informe o tempo de aquecimento.");
            }
            if (paramentro.Tempo.TotalMinutes > 2)
            {
                executarErro("O tempo de aquecimento deve ser menor que 2 minutos.");
            }
            if (paramentro.Tempo.TotalSeconds < 1)
            {
                executarErro("O tempo de aquecimento deve ser superior a 1 segundo.");
            }
            if (paramentro.Potencia < 1 || paramentro.Potencia > 10)
            {
                executarErro("A potência deve estar entre 1 e 10.");
            }
        }

        /// <summary>
        /// Rotina de aquecimento para a programação
        /// </summary>
        /// <param name="textoFrio"></param>
        /// <param name="programacao"></param>
        /// <returns></returns>
        public string aquecerUsandoProgramacao(string textoFrio, ProgramacaoInfo programacao) {
            if (textoFrio.ToLower().IndexOf(programacao.AlimentoCompativel.ToLower()) < 0) {
                executarErro("Não é possível executar a programação. Alimento imcompatível.");
            }
            return aquecer(textoFrio, new ParamentroInfo
            {
                Potencia = programacao.Potencia,
                Tempo = programacao.Tempo
            }, programacao.Caracter);
        }

        protected void aoProcessar(string textoParcialmenteAquecido) {
            AoProcessar?.Invoke(this, new MicroOndaProcessoEventArgs
            {
                TextoParciamenteAquecido = textoParcialmenteAquecido
            });
        }

        protected void aoConcluir(string textoAquecido) {
            AoConcluir?.Invoke(this, new MicroOndaConcluirEventArgs
            {
                TextoAquecido = textoAquecido
            });
        }

        /// <summary>
        /// Rotina principal de aquecimento
        /// </summary>
        /// <param name="textoFrio"></param>
        /// <param name="paramentro"></param>
        /// <param name="caracter"></param>
        /// <returns></returns>
        public virtual string aquecer(string textoFrio, ParamentroInfo paramentro, char caracter = '.') {
            validarParametro(paramentro);
            StringBuilder textoAquecido = new StringBuilder();
            StringBuilder textoPorSegundo = new StringBuilder();
            textoAquecido.Append(textoFrio);
            for (var i = 1; i <= paramentro.Tempo.TotalSeconds; i++)
            {
                textoPorSegundo.Clear();
                for (var p = 0; p < paramentro.Potencia; p++)
                {
                    textoPorSegundo.Append(caracter);
                }
                textoAquecido.Append(textoPorSegundo);
                aoProcessar(textoPorSegundo.ToString());
                Thread.Sleep(1000);
            }
            aoConcluir(textoAquecido.ToString());
            return textoAquecido.ToString();
        }
    }
}
