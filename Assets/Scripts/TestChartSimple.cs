using Assets.Scripts;
using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using Amazon;
using UnityEngine.UI;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

public class TestChartSimple : MonoBehaviour
{

    private static AmazonDynamoDBClient _client;
    private AmazonDynamoDBConfig _config;
    private DynamoDBContext _context;
    private GraphChartBase graph;

    private static Table sensorlogTable;
    private IList<SensorLog> sensorReadings;
    private SensorLog log;

    public Button getSensorData;
    public Button showData;

    // Use this for initialization
    void Start()
    {
        Amazon.UnityInitializer.AttachToGameObject(this.gameObject);
        getSensorData.onClick.AddListener(PerformReadOnTableListener);
        showData.onClick.AddListener(ConstructGraph);
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

    private void ConstructGraph()
    {
        ConstructTemperatureGraph();
    }

    private void PerformReadOnTable(Dictionary<string, AttributeValue> lastKeyEvaluated)
    {
        Table.LoadTableAsync(_client, "SensorReadings", (loadTableResult) =>
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
            TableName = "SensorReadings",
            Limit = 10,
            ExclusiveStartKey = lastKeyEvaluated,
        };

        _client.ScanAsync(request, (result) =>
        {
            foreach (Dictionary<string, AttributeValue> item
                         in result.Response.Items)
            {
                log = new SensorLog();
                ProcessItem(item);
            }
            lastKeyEvaluated = result.Response.LastEvaluatedKey;
            if (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0)
            {
                Debug.Log("*** The last key evaluated is " + lastKeyEvaluated);
                PerformReadOnTable(lastKeyEvaluated);
            }
        });
    }

    private void ProcessItem(Dictionary<string, AttributeValue> attributeList)
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

    private void ConstructTemperatureGraph()
    {
        graph = GetComponent<GraphChartBase>();

        if (graph != null)
        {
            graph.DataSource.StartBatch();
            graph.DataSource.ClearCategory("Temperature");
            for (int i = 0; i < sensorReadings.Count-1; i++)
            {
                graph.DataSource.AddPointToCategory("Temperature", Convert.ToDateTime(sensorReadings[i].Date), sensorReadings[i].Temperature);
            }
            graph.DataSource.EndBatch();
        }
    }
}