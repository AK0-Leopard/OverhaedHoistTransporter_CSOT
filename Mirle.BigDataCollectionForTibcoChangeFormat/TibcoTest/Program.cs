using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibcoPublisher;

namespace TibcoTest
{
    internal class Program
    {
        private static Sender _sender;

        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Service:");
                    var Service = Console.ReadLine();

                    Console.WriteLine("Network:");
                    var Network = Console.ReadLine();

                    Console.WriteLine("Daemom:");
                    var Daemom = Console.ReadLine();

                    _sender = new Sender("WHCSOT.G6D.BC.PROD.FDC.MOUDLE", Service,
                              Network, Daemom);
                    _sender.conn();

                    Console.WriteLine($"\n\n\n{_sender.isConn }, {_sender.exMsg}\n\n\n");
                    //_sender.Send("44444", "MFSTK500");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Conn Fail : {ex.ToString()}");
                }
                var line = Console.ReadLine();
                if (line.ToUpper().Equals("X"))
                    break;
            }
        }
    }
}