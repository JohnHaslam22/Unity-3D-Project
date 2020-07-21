using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelRotation : MonoBehaviour {

    public float degree;
    [Range(1f, 100f)]
    public float calibrateTurn;
    private GameObject carRoot;
    private LogitechControls logitechData;
	// Use this for initialization
	void Start () {
        carRoot = GameObject.FindGameObjectWithTag("Car");
        logitechData = carRoot.GetComponent<LogitechControls>();
	}
	
	// Update is called once per frame
	void Update () {
        degree = logitechData.steeringValue;
        transform.localEulerAngles = new Vector3(0, -(logitechData.steeringValue/calibrateTurn), 0);
	}
}
