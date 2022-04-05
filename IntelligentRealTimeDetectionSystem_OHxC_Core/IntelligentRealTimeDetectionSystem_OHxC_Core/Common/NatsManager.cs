using STAN.Client;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace com.mirle.iibg3k0.ids.ohxc.Common
{
    public class NatsManager
    {
        StanConnectionFactory ConnectionFactory = null;
        IStanConnection conn = null;
        ConcurrentDictionary<string, IStanSubscription> dicSubscription = new ConcurrentDictionary<string, IStanSubscription>();
        StanOptions cOpts = StanOptions.GetDefaultOptions();
        readonly string DefaultNatsURL = "nats://192.168.39.111:4222";
        string clusterID = null;
        string clientID = null;
        string producID = null;

        public NatsManager(string product_id, string cluster_id, string client_id)
        {
            clusterID = cluster_id;
            clientID = client_id;
            producID = product_id;

            string nats_server_ip = getHostTableIP("nats.ohxc.mirle.com.tw");
            if (nats_server_ip != null)
            {
                DefaultNatsURL = $"nats://{nats_server_ip}:4222";
            }

            cOpts.NatsURL = DefaultNatsURL;
            ConnectionFactory = new StanConnectionFactory();
            conn = getConnection();
        }

        private string getHostTableIP(string ip)
        {
            string return_ip = null;
            var remoteipAdr = System.Net.Dns.GetHostAddresses(ip);
            if (remoteipAdr != null && remoteipAdr.Count() > 0)
            {
                return_ip = remoteipAdr[0].ToString();
            }

            return return_ip;
        }

        IStanConnection getConnection()
        {
            return ConnectionFactory.CreateConnection(clusterID, clientID, cOpts);
        }

        public string Publish(string subject, byte[] data, EventHandler<StanAckHandlerArgs> handler)
        {
            subject = $"{producID}_{subject}";
            return conn.Publish(subject, data, handler);
        }

        public void Publish(string subject, byte[] data)
        {
            subject = $"{producID}_{subject}";
            conn.Publish(subject, data);
        }

        public void PublishAsync(string subject, byte[] data)
        {
            subject = $"{producID}_{subject}";
            conn.PublishAsync(subject, data);
        }

        public void Subscriber(string subject, EventHandler<StanMsgHandlerArgs> handler, bool in_all = false, bool is_last = false, ulong since_seq_no = 0, DateTime? since_duration = null)
        {
            subject = $"{producID}_{subject}";
            StanSubscriptionOptions sOpts = StanSubscriptionOptions.GetDefaultOptions();
            if (in_all)
            {
                sOpts.DeliverAllAvailable();
            }
            else if (is_last)
            {
                sOpts.StartWithLastReceived();
            }
            else if (since_seq_no != 0)
            {
                sOpts.StartAt(since_seq_no);
            }
            else if (since_duration.HasValue)
            {
                sOpts.StartAt(since_duration.Value);
            }
            dicSubscription.GetOrAdd(subject, conn.Subscribe(subject, sOpts, handler));
        }

        public void close()
        {
            if (dicSubscription.Count > 0)
            {
                foreach (var keyPair in dicSubscription)
                {
                    //keyPair.Value.Unsubscribe();
                    keyPair.Value.Close();
                }
            }

            conn?.Close();
        }
    }
}
