using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

public class CarLinearAcceleration : MonoBehaviour
{
    public static SerialPort serialPort1 = new SerialPort("COM5", 500000);
    public GameObject AccelerationCube;
    public static Vector3 position;
    public static Vector3 vector;
    public static int samples = 10;
    private static Vector3[] takeposition;
    private static float[] taketime;
    private static int positionSamplesTaken = 0;

    // Use this for initialization
    void Start()
    {

        OpenConnection();
        serialPort1.DtrEnable = true;
        serialPort1.BaudRate = 500000;

    }

    public void OpenConnection()
    {
        if (serialPort1 != null)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                print("Closing port, because it was already open!");
            }
            else
            {
                serialPort1.Open();
                serialPort1.ReadTimeout = 16;
                print("Port Opened!");
            }
        }
        else
        {
            if (serialPort1.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }

    void OnApplicationQuit()
    {
        serialPort1.Close();
    }

    void Update()
    {
        position = transform.InverseTransformPoint(AccelerationCube.transform.position);
        vector = new Vector3(0f, 0f, 0f);
        LinearAcceleration(out vector, position, samples);

    }

    private bool LinearAcceleration(out Vector3 vector, Vector3 position, int samples)
    {

        Vector3 averageSpeedChange = Vector3.zero;
        vector = Vector3.zero;
        Vector3 deltaDistance;
        float deltaTime;
        Vector3 speedA;
        Vector3 speedB;

        //Decide sample amount. To calculate acceleration you need at least 2 changes
        //in speed, so you need at least 3 position samples.
        if (samples < 12)
        {

            samples = 12;
        }

        //Initialize
        if (takeposition == null)
        {

            takeposition = new Vector3[samples];
            taketime = new float[samples];
        }

        //Fill the position and time sample array and shifts to the next slot in the array 
        //each time a new sample is taken. Index  holds the oldest sample.
        //The highest index will always hold the newest sample. 
        for (int i = 0; i < takeposition.Length - 1; i++)
        {

            takeposition[i] = takeposition[i + 1];
            taketime[i] = taketime[i + 1];
        }
       
        takeposition[takeposition.Length - 1] = position;
        taketime[taketime.Length - 1] = Time.time;

        positionSamplesTaken++;

        
        if (positionSamplesTaken >= samples)
        {

            //Calculates speed change.
            for (int i = 0; i < takeposition.Length - 2; i++)
            {

                deltaDistance = takeposition[i + 1] - takeposition[i];
                deltaTime = taketime[i + 1] - taketime[i];
                
                //If delta time is 0, the output is invalid.
                if (deltaTime == 0)
                {

                    return false;
                }

                speedA = deltaDistance / deltaTime;
                deltaDistance = takeposition[i + 2] - takeposition[i + 1];
                deltaTime = taketime[i + 2] - taketime[i + 1];

                if (deltaTime == 0)
                {

                    return false;
                }

                speedB = deltaDistance / deltaTime;

                //This is the accumulated speed change at this stage, not the average yet.
                averageSpeedChange += speedB - speedA;
            }

            //Now this is the average speed change.
            averageSpeedChange /= takeposition.Length - 2;

            //Get the total time difference.
            float deltaTimeTotal = taketime[taketime.Length - 1] - taketime[0];

            //Now calculate the acceleration, which is an average over the amount of samples taken.
            vector = averageSpeedChange / deltaTimeTotal;
            Debug.Log(vector);
            return true;
        }

        else
        {

            return false;
        }
    }
}
