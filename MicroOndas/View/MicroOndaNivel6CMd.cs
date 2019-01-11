using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.View
{
    public class MicroOndaNivel6Cmd: MicroOndaNivel3Cmd
    {
        public override void executar(string textoFrio = null)
        {
            if (string.IsNullOrEmpty(textoFrio))
            {
                textoFrio = capturarTexto();
            }
            processarComando(textoFrio);
            // Essa parte é feita para rodar com a Thread
            // Isso deve ser exibido no evento de AoConcluir da regra de negócio do MicroOnda
            //Console.WriteLine("Pressione tecla para sair...");
            //Console.ReadKey();
        }
    }
}
