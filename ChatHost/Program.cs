using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ServiceHost host = new ServiceHost(typeof(NPP_Chat.ServiceChat)))
            {

                host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
                host.AddServiceEndpoint(new UdpDiscoveryEndpoint());

                host.Opening += new EventHandler(host_Opening);
                host.Opened += new EventHandler(host_Opened);
                host.Open();
                Console.ReadLine();
            }
        }

        static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Service is ready...");
        }

        static void host_Opening(object sender, EventArgs e)
        {
            Console.WriteLine("Opening service...");
        }
    }
}
