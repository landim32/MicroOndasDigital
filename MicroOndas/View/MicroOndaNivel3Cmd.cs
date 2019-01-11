using MicroOndas.Factory;
using MicroOndas.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MicroOndas.View
{
    public class MicroOndaNivel3Cmd: MicroOndaNivel2Cmd
    {
        protected override void exibirComando()
        {
            base.exibirComando();
            Console.WriteLine("4 - INCLUIR NOVA PROGRAMAÇÃO");
        }

        protected override string processarComando(string textoFrio, ComandoEnum comando = ComandoEnum.Raiz)
        {
            if (comando == ComandoEnum.Raiz) {
                comando = capturarComando();
            }
            string textoAquecido = string.Empty;
            switch (comando)
            {
                case ComandoEnum.NovaProgramacao:
                    var programacao = novaProgramacao();
                    ProgramacaoFactory.create().inserir(programacao);
                    Console.WriteLine("Programação incluída com sucesso!");
                    base.processarComando(textoFrio, ComandoEnum.Raiz);
                    break;
                default:
                    base.processarComando(textoFrio, comando);
                    break;
            }
            return textoAquecido;
        }

        protected ProgramacaoInfo novaProgramacao() {
            var programacao = new ProgramacaoInfo();
            Console.WriteLine("Preencha os dados da Programação:");
            Console.Write("Nome: ");
            programacao.Nome = Console.ReadLine();
            Console.Write("Instruções: ");
            programacao.Instrucao = Console.ReadLine();
            Console.Write("Tempo: ");
            programacao.Tempo = capturarTempo();
            Console.Write("Potência: ");
            programacao.Potencia = capturarPotencia();
            Console.Write("Caracter: ");
            programacao.Caracter = Console.ReadLine()[0];
            Console.Write("Palavra-chave de alimento compatível: ");
            programacao.AlimentoCompativel = Console.ReadLine();
            return programacao;
        }
    }
}
