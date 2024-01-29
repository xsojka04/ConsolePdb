using Confluent.Kafka;
using ConsolePdb.Clients;
using ConsolePdb.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsolePdb
{
    internal class Program
    {
        static async Task Main()
        {
            //Získáni konfigurace
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var token = config.GetSection("InfluxDB").GetValue<string>("Token");
            var username = config.GetSection("KafkaDB").GetValue<string>("Username");
            var password = config.GetSection("KafkaDB").GetValue<string>("Password");


            //Vytvoření připojení na Kafku a Influx
            using var kafka = new Kafka(username, password);
            Console.WriteLine("Kafka connection ready");
            using var influx = new Influx(token);
            Console.WriteLine("Influx connection ready");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; 
                cts.Cancel();
            };

            try
            {
                while (true) //cyklus pro zpracovávání zpráv z Kafky a zapisování do Influx
                {
                    try
                    {
                        var message = kafka.GetMessage(cts.Token);
                        Console.WriteLine(JsonConvert.SerializeObject(message));
                        await influx.WriteAsync(message);
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                    catch (KafkaBadFormatException e)
                    {
                        Console.WriteLine($"KafkaBadFormatException: {e.Message}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}
