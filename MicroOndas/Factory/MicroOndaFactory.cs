using MicroOndas.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.Factory
{
    public static class MicroOndaFactory
    {
        private static MicroOndaBLL _microOnda = null;

        public static MicroOndaBLL create() {
            if (_microOnda == null) {
                // Podem ser usadas essas outras regras de negócio
                //_microOnda = new MicroOndaBLL();
                //_microOnda = new MicroOndaArquivoBLL();
                _microOnda = new MicroOndaTarefaBLL();
                _microOnda.AoErro += (sender, e) => {
                    throw new Exception(e.Erro);
                };
                _microOnda.AoProcessar += (sender, e) => {
                    Console.Write(e.TextoParciamenteAquecido);
                };

                _microOnda.AoConcluir += (sender, e) => {
                    // Incluído isso para roda na Thread
                    Console.WriteLine();
                    Console.WriteLine("---TEXTO AQUECIDO---");
                    Console.WriteLine(e.TextoAquecido);
                    Console.WriteLine("Pressione tecla para sair...");
                    Console.ReadKey();
                };
            }
            return _microOnda;
        }
    }
}
