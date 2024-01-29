using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using System;
using System.Collections.Generic;

namespace ConsolePdb.Models
{
    public class KafkaMessage
    {
        public string Measurement { get; set; }
        public Dictionary<string, string> TagSet { get; set; }
        public Dictionary<string, object> FieldSet { get; set; }
        
        public List<PointData> ToPointDataList()
        {
            var pointDataList = new List<PointData>();
            foreach (KeyValuePair<string, object> field in FieldSet)
            {
                var builder = PointData.Builder.Measurement(Measurement);
                foreach (KeyValuePair<string, string> tag in TagSet)
                    builder.Tag(tag.Key, tag.Value);
                builder.Field(field.Key, field.Value);
                builder.Timestamp(DateTime.UtcNow, WritePrecision.Ns);
                pointDataList.Add(builder.ToPointData());
            }
            return pointDataList;
        }

        private string ErrorMessage()
        {
            if (Measurement is null)
                return "Measurement name missing";
            if (TagSet is null)
                return "TagSet is null";
            if (FieldSet is null)
                return "FieldSet is null";
            if (TagSet?.Count == 0)
                return "TagSet is empty";
            if (FieldSet?.Count == 0)
                return "FieldSet is empty";
            return null;
        }

        public bool IsValid()
        {
            string errorMessage = ErrorMessage();
            if (errorMessage is null)
                return true;
            Console.WriteLine(errorMessage);
            return false;
        }
    }
}
