using Confluent.Kafka;
using ConsolePdb.Exceptions;
using ConsolePdb.Models;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace ConsolePdb.Clients
{
    public class Kafka : IDisposable
    {
        private static readonly string _groupId = "pdbgroup";
        private static readonly string _bootstrapServers = "glider-01.srvs.cloudkafka.com:9094,glider-02.srvs.cloudkafka.com:9094,glider-03.srvs.cloudkafka.com:9094";
        private static readonly AutoOffsetReset _autoOffsetReset = AutoOffsetReset.Earliest;
        private static readonly string _topic = "sfpc2vlv-default";

        private IConsumer<Ignore, string> Consumer { get; }

        public Kafka(string username, string password)
        {
            Consumer = new ConsumerBuilder<Ignore, string>(ConsumerConfig(username, password)).Build();
            Consumer.Subscribe(_topic);
        }

        private ConsumerConfig ConsumerConfig(string username, string password)
        {
            return new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = _autoOffsetReset,
                SaslUsername = username,
                SaslPassword = password,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha512
            };
        }
        

        public KafkaMessage GetMessage(CancellationToken token)
        {
            try
            {
                var cr = Consumer.Consume(token);
                var message = JsonConvert.DeserializeObject<KafkaMessage>(cr.Message.Value);
                if (!message.IsValid())
                    throw new KafkaBadFormatException();
                return message;
            } 
            catch (Exception e)
            {
                throw new KafkaBadFormatException(e.Message);
            }
        }

        public void Dispose()
        {
            Consumer?.Dispose();
        }
    }
}
