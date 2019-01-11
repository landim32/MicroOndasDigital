using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroOndas
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new View.MicroOndaNivel6Cmd().executar();
            }
            catch (Exception erro) {
                Console.WriteLine(erro.Message);
                Console.WriteLine("Pressione tecla para sair...");
                Console.ReadKey();
            }
        }
    }
}
