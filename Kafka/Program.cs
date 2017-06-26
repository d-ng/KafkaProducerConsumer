using System;

namespace Kafka
{
    class Program
    {
        const string url = "http://10.32.193.218:9092";
        const string topic = "kafka-test-10000meters-15s";
        const int meterNum = 10000;
        const int intervalms = 15000;
        
        static void Main(string[] args)
        {
            KafkaProducer producer = new KafkaProducer(url, topic, meterNum, intervalms);

            //var consumer = new KafkaConsumer(url, topic);
            //consumer.Start();

            Console.ReadLine();
        }
    }
}