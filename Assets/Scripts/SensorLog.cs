﻿using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [DynamoDBTable("SensorLog")]
    public class SensorLog
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBRangeKey]
        public int SensorNumber { get; set; }

        [DynamoDBProperty(AttributeName = "Type")]
        public string Type { get; set; }

        [DynamoDBProperty(AttributeName = "Temperature")]
        public float Temperature { get; set; }

        [DynamoDBProperty(AttributeName = "Pressure")]
        public float Pressure { get; set; }

        [DynamoDBProperty(AttributeName = "Humidty")]
        public float Humidity { get; set; }

        [DynamoDBProperty(AttributeName = "GPS")]
        public string GPS { get; set; }

        [DynamoDBProperty(AttributeName = "Datestamp")]
        public string Date { get; set; }
    }
}
