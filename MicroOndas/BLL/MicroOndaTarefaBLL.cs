using MicroOndas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MicroOndas.BLL
{
    /// <summary>
    /// Regra de negocio para ser usada na Thread
    /// Deve ser usando apenas na Interface MicroOndaNivel6Cmd
    /// </summary>
    public class MicroOndaTarefaBLL: MicroOndaArquivoBLL
    {
        protected Thread _thread = null;

        /// <summary>
        /// Metodo que fica ativo enquanto a thread roda e fica capturando as teclas pressionadas
        /// </summary>
        private void pausarResumirOuCancelar() {
            if (!_thread.IsAlive) {
                return;
            }
            var comando = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (comando)
            {
                case 'p':
                    if (_thread.ThreadState == ThreadState.Suspended) {
                        _thread.Resume();
                        pausarResumirOuCancelar();
                    }
                    else
                    {
                        _thread.Suspend();
                        Console.WriteLine("Operação pausada. Pressione 'p' para continuar ou 'x' para cancelar.");
                        pausarResumirOuCancelar();
                    }
                    break;
                case 'x':
                    _thread.Abort();
                    Console.WriteLine("Operação cancelada!");
                    break;
                default:
                    pausarResumirOuCancelar();
                    break;
            }
        }

        public override string aquecer(string textoFrio, ParamentroInfo paramentro, char caracter = '.') {
            _thread = new Thread(() => {
                base.aquecer(textoFrio, paramentro, caracter);
            });
            _thread.Start();

            Console.WriteLine("Para pausa pressione 'p', para cancelar pressione 'x'...");
            pausarResumirOuCancelar();
            return string.Empty;
        }
    }
}
