using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core.Domain.Entities
{
    [DynamoDBTable("SensorReadings")]
    public class SensorReading
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBRangeKey]
        public int SensorNumber { get; set; }

        [DynamoDBProperty(AttributeName = "Type")]
        public string Type { get; set; }

        [DynamoDBProperty(AttributeName = "Temperature")]
        public string Temperature { get; set; }

        [DynamoDBProperty(AttributeName = "Pressure")]
        public string Pressure { get; set; }

        [DynamoDBProperty(AttributeName = "Humidty")]
        public string Humidity { get; set; }

        [DynamoDBProperty(AttributeName = "GPS")]
        public string GPS { get; set; }

        [DynamoDBProperty(AttributeName = "Datestamp")]
        public string Date { get; set; }
    }
 }
