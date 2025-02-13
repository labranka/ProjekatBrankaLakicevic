﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domen;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Server
{
    public class Server
    {
        Socket soket;
       

        public bool pokreniServer()
        {
            try
            {
                soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 20000);
                soket.Bind(ep);

                ThreadStart ts = osluskujKlijente;
                Thread nit = new Thread(ts);
                nit.Start();
                
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        void osluskujKlijente()
        {
            try
            {
                while (true)
                {
                    soket.Listen(4);
                    Socket klijent = soket.Accept();
                    NetworkStream tok = new NetworkStream(klijent);
                    new NitKlijenta(tok);
                   

                }
            }
            catch (Exception)
            {

                
            }
        }

        public bool ZaustaviServer()
        {
            try
            {
                soket.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

    }
}
