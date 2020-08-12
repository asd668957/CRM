using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_clientdatabase
{
    class Class_scan
    {
        public string scan(string subnet)
        {
            Ping myPing;
            PingReply reply;
            IPAddress addr;
            IPHostEntry host;


            //搜尋末位ip 100~120
            for (int i = 100; i < 121; i++)
            {
                string subnetn = "." + i.ToString();
                myPing = new Ping();
                reply = myPing.Send(subnet + subnetn, 900);

                if (reply.Status == IPStatus.Success)
                {
                    try
                    {
                        addr = IPAddress.Parse(subnet + subnetn);
                        host = Dns.GetHostEntry(addr);


                        if (host.HostName == "Database") { return (subnet + subnetn); }
                    }
                    catch
                    {
                        //return "0";
                    }
                }
            }
            return "0";
        }
    }
}
