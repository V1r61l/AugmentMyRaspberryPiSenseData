using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initialize_scene : MonoBehaviour {

    public Button getterSensorData;

	// Use this for initialization
	void Start () {
        Amazon.UnityInitializer.AttachToGameObject(this.gameObject);
        getterSensorData.onClick.AddListener(GetterSensorData);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetterSensorData()
    {
        SceneManager.LoadScene(1);
    }
}
