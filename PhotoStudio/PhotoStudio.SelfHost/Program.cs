using PhotoStudio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(PhotoStudioService));
            host.Open();
            Console.WriteLine("Photo studio service is running..."); 
            Console.ReadKey();
            host.Close(); 
        }
    }
}
