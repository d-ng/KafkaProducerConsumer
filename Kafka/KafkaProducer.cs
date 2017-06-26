using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;

namespace Kafka
{
    public class KafkaProducer
    {
        private KafkaOptions _options;
        private BrokerRouter _router;
        private Producer _client;

        private string _topic;
        private int _meterNum;
        private Timer _timer;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public KafkaProducer(string url, string topic, int meterNum, int intervalms, int delay = 2000)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Url cannot be null");
            }
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException("topic cannot be null");
            }

            _options = new KafkaOptions(new Uri(url));
            _router = new BrokerRouter(_options);
            _client = new Producer(_router);
            _topic = topic;
            _meterNum = meterNum;

            _timer = new Timer(SendEvents, null, 0, intervalms);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void SendEvents(object state)
        {
            DateTime time = DateTime.UtcNow;
            Console.WriteLine("{0}: Sending message", time);
            int start = 0;
            int batchSize = 1000;
                
            while (start < _meterNum)
            {
                Console.WriteLine("start {0}", start);
                IEnumerable<Message> messages = GenerateData(time, start, batchSize);
                _client.SendMessageAsync(_topic, messages).Wait();
                Thread.Sleep(750);
                start += batchSize;
            }
        }

        private IEnumerable<Message> GenerateData(DateTime time, int start, int count)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = start; i < start + count; i++)
            {
                MeterItem item = MeterItem.GenerateRandomMeterItem(i, time, _meterNum, random);
                yield return new Message(item.ToString());
            }

        }
    }
}
