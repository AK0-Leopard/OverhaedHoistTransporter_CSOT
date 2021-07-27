using System;
using System.Diagnostics;
using TIBCO.Rendezvous;

namespace TibcoPublisher
{
    public class Sender
    {
        private NetTransport _transport;
        private string _subject;
        public string exMsg = string.Empty;
        public bool isConn = false;
        private string _service;
        private string _network;
        private string _port;

        public Sender(string sub, string service, string network, string port)
        {
            try
            {
                TIBCO.Rendezvous.Environment.Open();

                _subject = sub;
                _service = service;
                _network = network;
                _port = port;
                _transport = new NetTransport(_service, _network, _port);
            }
            catch (Exception ex)
            {
                exMsg = ex.ToString();
                isConn = false;
            }
        }

        public void conn()
        {
            try
            {
                var strNetwork = $";{_network}";
                _transport = new NetTransport(_service, strNetwork, _port);
                isConn = true;
            }
            catch (Exception ex)
            {
                isConn = false;

                exMsg = $"{ex.Message}";
            }
        }

        public bool Send(string message, string fieldName)
        {
            try
            {
                var strNetwork = $";{_network}";
                _transport = new NetTransport(_service, strNetwork, _port);

                isConn = true;
                var msg = new Message();
                msg.SendSubject = _subject;
                msg.AddField(fieldName, message);
                _transport.Send(msg);

                exMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                exMsg = $"{ex}";
                return false;
            }
        }
    }
}