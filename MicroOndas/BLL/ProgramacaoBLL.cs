using MicroOndas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas.BLL
{
    /// <summary>
    /// Regra da programação
    /// Não está gravando, funciona apenas em memória
    /// Não criei rotina para alteração e exclusão pq não estava especificado
    /// </summary>
    public class ProgramacaoBLL
    {
        private IList<ProgramacaoInfo> _programacaoAtual;

        public ProgramacaoBLL() {
            _programacaoAtual = gerarProgramacaoInicial();
        }

        public IList<ProgramacaoInfo> listar() {
            return (
                from p in _programacaoAtual
                orderby p.Nome
                select p
            ).ToList();
        }

        public IList<string> listarAlimentoCompativel()
        {
            return (
                from p in _programacaoAtual
                orderby p.AlimentoCompativel
                select p.AlimentoCompativel
            ).Distinct().ToList();
        }

        public IList<ProgramacaoInfo> listarPorAlimento(string alimento)
        {
            return (
                from p in _programacaoAtual
                where p.AlimentoCompativel.ToLower().IndexOf(alimento.ToLower()) >= 0 
                orderby p.Nome
                select p
            ).ToList();
        }

        public void inserir(ProgramacaoInfo programacao) {
            _programacaoAtual.Add(programacao);
        }

        /// <summary>
        /// Gera as 5 programações iniciais
        /// </summary>
        /// <returns></returns>
        private IList<ProgramacaoInfo> gerarProgramacaoInicial() {
            return new List<ProgramacaoInfo>
            {
                new ProgramacaoInfo{
                    Nome = "Descongelar Carnes",
                    Instrucao = "Descongelar carnes por 1 minuto e meio que contenham a palavra-chave 'Carne' em potência 1 exibindo o caracter '*'.",
                    Tempo = TimeSpan.FromSeconds(90),
                    Potencia = 1,
                    AlimentoCompativel = "Carne",
                    Caracter = '*'
                },
                new ProgramacaoInfo{
                    Nome = "Descongelar Frangos",
                    Instrucao = "Descongelar frangos por 2 minutos que contenham a palavra-chave 'frango' em potência 3 exibindo o caracter '&'.",
                    Tempo = TimeSpan.FromMinutes(2),
                    Potencia = 3,
                    AlimentoCompativel = "Frango",
                    Caracter = '&'
                },
                new ProgramacaoInfo{
                    Nome = "Pipoca",
                    Instrucao = "Fazer pipoca por 1 minuto e meio que contenham a palavra-chave 'pipoca' em potência 5 exibindo o caracter '%'.",
                    Tempo = TimeSpan.FromMinutes(1.5),
                    Potencia = 5,
                    AlimentoCompativel = "Pipoca",
                    Caracter = '%'
                },
                new ProgramacaoInfo{
                    Nome = "Assar Bolo",
                    Instrucao = "Assar Bolo por 1 minutos que contenham a palavra-chave 'bolo' em potência 2 exibindo o caracter '$'.",
                    Tempo = TimeSpan.FromMinutes(1),
                    Potencia = 2,
                    AlimentoCompativel = "Pipoca",
                    Caracter = '$'
                },
                new ProgramacaoInfo{
                    Nome = "Aquecer Miojo",
                    Instrucao = "Assar Miojo por 30 segundos que contenham a palavra-chave 'miojo' em potência 10 exibindo o caracter '@'.",
                    Tempo = TimeSpan.FromSeconds(30),
                    Potencia = 10,
                    AlimentoCompativel = "Miojo",
                    Caracter = '@'
                }
            };
        }
    }
}
