using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SensorRepository {

    //private static AmazonDynamoDBClient _client;
    //private AmazonDynamoDBConfig _config;
    //private DynamoDBContext _context;

    //private static Table sensorlogTable;
    //private IList<SensorLog> sensorReadings;
    //private SensorLog log;

    //public SensorRepository()
    //{
    //    _config = new AmazonDynamoDBConfig();
    //    _config.ServiceURL = "http://192.168.0.100:8000/";
    //    _client = new AmazonDynamoDBClient("fakeMyKeyId", "fakeSecretAccessKey", _config);
    //    _context = new DynamoDBContext(_client);
    //    sensorReadings = new List<SensorLog>();
    //}

    //public void ReadTable()
    //{
    //    Table.LoadTableAsync(_client, "SensorLog", (loadTableResult) =>
    //    {
    //        if (loadTableResult.Exception != null)
    //        {
    //            //resultText.text += "\n failed to load reply table";
    //            Debug.Log(" failed to load sensorlog table ");
    //        }
    //        else
    //        {
    //            sensorlogTable = loadTableResult.Result;
    //            Debug.Log(sensorlogTable.TableName);
    //        }
    //    });
    //}

    //public IList<SensorLog> getAllValues()
    //{
    //    return sensorReadings;
    //}

    //public void PerformTableRead()
    //{
    //    PerformReadOnTable(null);
    //}

    //public void PerformReadOnTable(Dictionary<string, AttributeValue> lastKeyEvaluated)
    //{
    //    var request = new ScanRequest
    //    {
    //        TableName = "SensorLog",
    //        Limit = 10,
    //        ExclusiveStartKey = lastKeyEvaluated,
    //    };

    //    _client.ScanAsync(request, (result) =>
    //    {
    //        foreach (Dictionary<string, AttributeValue> item
    //                     in result.Response.Items)
    //        {
    //            log = new SensorLog();
    //            ProcessItem(item);
    //        }
    //        lastKeyEvaluated = result.Response.LastEvaluatedKey;
    //        if (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0)
    //        {
    //            Debug.Log("*** The last key evaluated is " + lastKeyEvaluated);
    //            PerformReadOnTable(lastKeyEvaluated);
    //        }
    //    });
    //}

    //private void ProcessItem(Dictionary<string, AttributeValue> attributeList)
    //{
    //    foreach (var kvp in attributeList)
    //    {
    //        string attributeName = kvp.Key;
    //        AttributeValue value = kvp.Value;

    //        switch (attributeName)
    //        {
    //            case "Id":
    //                log.Id = Convert.ToInt32(value.N);
    //                break; ;
    //            case "Type":
    //                log.Type = value.S;
    //                break;
    //            case "SensorNumber":
    //                log.SensorNumber = Convert.ToInt32(value.N);
    //                break;
    //            case "Temperature":
    //                log.Temperature = float.Parse(value.S, CultureInfo.InvariantCulture.NumberFormat);
    //                break;
    //            case "Humidity":
    //                log.Humidity = float.Parse(value.S, CultureInfo.InvariantCulture.NumberFormat);
    //                break;
    //            case "Pressure":
    //                log.Pressure = float.Parse(value.S, CultureInfo.InvariantCulture.NumberFormat);
    //                break;
    //            case "Datestamp":
    //                log.Date = value.S;
    //                break;
    //            default:
    //                break;
    //        }

    //        //Debug.Log((
    //        //       "\n" + attributeName + " " +
    //        //       (value.S == null ? "" : "S=[" + value.S + "]") +
    //        //       (value.N == null ? "" : "N=[" + value.N + "]")
    //        //   ));
    //    }
    //    sensorReadings.Add(log);
    //}
}
