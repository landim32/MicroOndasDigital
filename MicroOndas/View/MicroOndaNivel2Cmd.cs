using MicroOndas.Factory;
using MicroOndas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.View
{
    public class MicroOndaNivel2Cmd: MicroOndaNivel1Cmd
    {
        protected override void exibirComando()
        {
            base.exibirComando();
            Console.WriteLine("3 - EXECUTAR PROGRAMAÇÃO");
        }

        protected override string processarComando(string textoFrio, ComandoEnum comando = ComandoEnum.Raiz)
        {
            if (comando == ComandoEnum.Raiz) {
                comando = capturarComando();
            }
            string textoAquecido = string.Empty;
            switch (comando)
            {
                case ComandoEnum.ListarProgramacao:
                    textoAquecido = executarProgramacao(textoFrio);
                    break;
                default:
                    textoAquecido = base.processarComando(textoFrio, comando);
                    break;
            }
            return textoAquecido;
        }

        protected void exibirProgramacaoLista(IList<ProgramacaoInfo> programacaoLista) {
            int i = 1;
            foreach (var programacao in programacaoLista) {
                exibirProgramacaoItem(i, programacao);
                i++;
            }
        }

        protected void exibirProgramacaoItem(int index, ProgramacaoInfo programacao)
        {
            Console.WriteLine(index + " - " + programacao.Nome.ToUpper());
            Console.WriteLine(" Instrução: " + programacao.Instrucao);
            Console.WriteLine(" Tempo:     " + programacao.Tempo.ToString("mm\\:ss"));
            Console.WriteLine(" Potência:  " + programacao.Potencia);
            Console.WriteLine("---");
        }

        protected string listarProgramacao(string textoFrio) {
            var programacoes = ProgramacaoFactory.create().listar();
            exibirProgramacaoLista(programacoes);
            var programacao = selecionarProgramacao(programacoes);
            return MicroOndaFactory.create().aquecerUsandoProgramacao(textoFrio, programacao); ;
        }

        protected string selecionarAlimentoCompativel(IList<string> alimentosCompativeis)
        {
            var index = 0;
            if (!(int.TryParse(Console.ReadLine(), out index) && (index - 1) >= 0 && (index - 1) < alimentosCompativeis.Count))
            {
                Console.WriteLine("AVISO: Nenhum alimento compatível com esse número.");
                return selecionarAlimentoCompativel(alimentosCompativeis);
            }
            return alimentosCompativeis[index - 1];
        }

        protected string buscarProgramacao(string textoFrio)
        {
            Console.WriteLine("Escolha o alimento compatível que deseja listar:");
            var alimentosCompativeis = ProgramacaoFactory.create().listarAlimentoCompativel();
            int i = 1;
            foreach (var alimento in alimentosCompativeis) {
                Console.WriteLine(i + " - " + alimento);
                i++;
            }
            var alimentoSelecionado = selecionarAlimentoCompativel(alimentosCompativeis);
            Console.WriteLine("Listando programações disponíveis para o alimento " + alimentoSelecionado + ":");
            var programacoes = ProgramacaoFactory.create().listarPorAlimento(alimentoSelecionado);
            exibirProgramacaoLista(programacoes);
            var programacao = selecionarProgramacao(programacoes);
            return MicroOndaFactory.create().aquecerUsandoProgramacao(textoFrio, programacao);
        }

        protected ProgramacaoInfo selecionarProgramacao(IList<ProgramacaoInfo> programacaoLista) {
            var index = 0;
            if (!(int.TryParse(Console.ReadLine(), out index) && (index-1) >= 0 && (index-1) < programacaoLista.Count)) {
                Console.WriteLine("AVISO: Nenhuma programação com esse número.");
                return selecionarProgramacao(programacaoLista);
            }
            return programacaoLista[index-1];
        }

        protected string executarProgramacao(string textoFrio) {
            Console.WriteLine("Escolha um dos comando abaixo ou pressione qualquer outra tecla para cancelar:");
            Console.WriteLine("1 - LISTAR TODAS AS PROGRAMAÇÔES EM ORDEM ALFABÉTICA");
            Console.WriteLine("2 - BUSCAR DE ACORDO COM ALIMENTO COMPATÍVEL");
            string textoAquecido = string.Empty;
            var comando = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (comando) {
                case '1':
                    textoAquecido = listarProgramacao(textoFrio);
                    break;
                case '2':
                    textoAquecido = buscarProgramacao(textoFrio);
                    break;
                default:
                    Console.WriteLine("Operação cancelada!");
                    textoAquecido = processarComando(textoFrio);
                    break;
            }
            return textoAquecido;
        }
    }
}
