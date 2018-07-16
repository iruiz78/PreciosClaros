using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciosClaros
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                Persistance.Dao.Connection.SetConnectionString(Common.Helpers.Setting.Get_ConnectionString);
                switch (args[0].ToLower())
                {
                    case "all":
                        Console.WriteLine("Empieza el proceso para todas las zonas.");
                        var service = new Helpers.ServicePrice();
                        service.GetPriceAll();
                        // Descargo todo.
                        break;
                }
            }
        }
    }
}
