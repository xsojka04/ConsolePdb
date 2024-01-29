using InfluxDB.Client.Writes;
using InfluxDB.Client;
using System;
using ConsolePdb.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using InfluxDB.Client.Api.Domain;

namespace ConsolePdb.Clients
{
    public class Influx : IDisposable
    {
        private const string _url = "https://eu-central-1-1.aws.cloud2.influxdata.com";
        private const string _org = "ea35ae5e0f210e45";
        private const string _bucket= "pdb";

        private InfluxDBClient Client { get; set; }
        private WriteApiAsync WriteApi { get; set; }

        public Influx(string token)
        {
            Client = InfluxDBClientFactory.Create(_url, token.ToCharArray());
            WriteApi = Client.GetWriteApiAsync();
        }

        public async Task WriteAsync(KafkaMessage kafkaMessage)
        {
            await WriteApi.WritePointsAsync(kafkaMessage.ToPointDataList(), _bucket, _org);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
