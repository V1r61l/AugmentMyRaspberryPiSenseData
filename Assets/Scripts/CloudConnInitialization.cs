using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Assets.Scripts;
using UnityEngine.UI;
using System;
using System.Globalization;

public class CloudConnInitialization : MonoBehaviour {

    public Button getterSensorData;
    public Button showSensorData;

    //public LineChart _lineChart = null; 

    private static AmazonDynamoDBClient _client;
    private AmazonDynamoDBConfig _config;
    private DynamoDBContext _context;

    private static Table sensorlogTable;

    private IList<SensorLog> sensorReadings;
    private SensorLog log;
    private float[][] data;

    // Use this for initialization
    void Start()
    {
        Amazon.UnityInitializer.AttachToGameObject(this.gameObject);
        getterSensorData.onClick.AddListener(PerformReadOnTableListener);
        showSensorData.onClick.AddListener(ConstructTemperatureGraph);
        _config = new AmazonDynamoDBConfig();
        _config.ServiceURL = "http://192.168.0.100:8000/";
        _client = new AmazonDynamoDBClient("fakeMyKeyId", "fakeSecretAccessKey", _config);
        _context = new DynamoDBContext(_client);
        sensorReadings = new List<SensorLog>();
    }

    private void PerformReadOnTableListener()
    {
        PerformReadOnTable(null);
    }

    private void PerformReadOnTable(Dictionary<string, AttributeValue> lastKeyEvaluated)
    {
        Table.LoadTableAsync(_client, "SensorLog", (loadTableResult) =>
        {
            if (loadTableResult.Exception != null)
            {
                //resultText.text += "\n failed to load reply table";
                Debug.Log(" failed to load sensorlog table ");
            }
            else
            {
                sensorlogTable = loadTableResult.Result;
                Debug.Log(sensorlogTable.TableName);
            }
        });

        var request = new ScanRequest
        {
            TableName = "SensorLog",
            Limit = 10,
            ExclusiveStartKey = lastKeyEvaluated,
        };
        
        _client.ScanAsync(request, (result) =>
        {
            foreach (Dictionary<string, AttributeValue> item
                         in result.Response.Items)
            {
                log = new SensorLog();
                PrintItem(item);
            }
            lastKeyEvaluated = result.Response.LastEvaluatedKey;
            if (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0)
            {
                Debug.Log("*** The last key evaluated is " + lastKeyEvaluated);
                PerformReadOnTable(lastKeyEvaluated);
            }
        });
    }

    private void PrintItem(Dictionary<string, AttributeValue> attributeList)
    {
        foreach (var kvp in attributeList)
        {
            string attributeName = kvp.Key;
            AttributeValue value = kvp.Value;

            switch (attributeName)
            {
                case "Id":
                    log.Id = Convert.ToInt32(value.N);
                    break; ;
                case "Type":
                    log.Type = value.S;
                    break;
                case "SensorNumber":
                    log.SensorNumber = Convert.ToInt32(value.N);
                    break;
                case "Temperature":
                    log.Temperature = float.Parse(value.S, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "Humidity":
                    log.Humidity = float.Parse(value.S, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "Pressure":
                    log.Pressure = float.Parse(value.S, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "Datestamp":
                    log.Date = value.S;
                    break;
                default:
                    break;
            }

            Debug.Log((
                   "\n" + attributeName + " " +
                   (value.S == null ? "" : "S=[" + value.S + "]") +
                   (value.N == null ? "" : "N=[" + value.N + "]")
               ));
        }
        sensorReadings.Add(log);
    }

    private void listObjList()
    {
        foreach (var item in sensorReadings)
        {
            Debug.Log(string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6}",
                        item.Id,
                        item.SensorNumber,
                        item.Type,
                        item.Temperature,
                        item.Humidity,
                        item.Pressure,
                        item.Date));
        }
    }


    private void ConstructGraph()
    {
        ConstructTemperatureGraph();

    }   

    private void ConstructTemperatureGraph()
    {
        //data = new float[3][];
        //for (int i = 0; i < data.Length; i++)
        //{
        //    data[i] = new float[sensorReadings.Count];
        //    if (sensorReadings.Count != 0)
        //    {
        //        for (int j = 0; j < sensorReadings.Count; j++)
        //        {
        //            //if (i == 0)
        //            //{
        //                data[i][j] = float.Parse(sensorReadings[j].Temperature, CultureInfo.InvariantCulture.NumberFormat);
        //            //}
        //            //else if (i == 1)
        //            //{
        //            //    data[i][j] = float.Parse(sensorReadings[j].Humidity, CultureInfo.InvariantCulture.NumberFormat);
        //            //}
        //            //else
        //            //{
        //            //    data[i][j] = float.Parse(sensorReadings[j].Pressure, CultureInfo.InvariantCulture.NumberFormat);
        //            //}
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("There are no readings from table");
        //    }
        //}

        //if (_lineChart != null)
        //{
        //    _lineChart.UpdateData(data);
        //}
    }
}
