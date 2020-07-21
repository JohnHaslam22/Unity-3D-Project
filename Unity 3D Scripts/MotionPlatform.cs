using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using UnityEngine;

public class MotionPlatform : MonoBehaviour
{

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngZ;
    [SerializeField]
    float XAngle;
    [SerializeField]
    float ZAngle;
    public Rigidbody Van;
    public float PreviousLinearV = 0;
    public int sampleno = 0;
    public static int samples = 12;
    public static float[] LinearAccelerationSamples;
    public static float AverageLinearAccleration;

    // Use this for initialization

    void Start()
    {
        Van = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
       

        byte[] motorA = new byte[5];
            byte[] motorB = new byte[5];
            motorA[0] = 0x5B;
            motorA[1] = 0x41;
            motorA[4] = 0x5D;
            motorB[0] = 0x5B;
            motorB[1] = 0x42;
            motorB[4] = 0x5D;

            eulerAngX = transform.localEulerAngles.x;
            eulerAngZ = transform.localEulerAngles.z;

            if (eulerAngX >= 180.0f && eulerAngX <= 360.0f)
            {
                XAngle = -((eulerAngX * -1.0f) + 360.0f);
            }
            else
            {
                XAngle = eulerAngX;
            }

            if (eulerAngZ >= 180.0f && eulerAngZ <= 360.0f)
            {
                ZAngle = -((eulerAngZ * -1.0f) + 360.0f);
            }
            else
            {
                ZAngle = eulerAngZ;
            }

            if (XAngle < 45.0f && XAngle >= 0.0f && LinearAcceleration() < 0)
            {
                float LAmagnified = LinearAcceleration() * 36;
                int valueLA = (int)LAmagnified;
                int value = (int)XAngle;
                int valueA = 511 + (value * 8) + (-valueLA);
                int valueB = 511 - (value * 8) + (valueLA);
                if (valueA > 871)
                {
                valueA = 871;
                }
                if (valueB < 151)
                {
                valueB = 151;
                }
                
                string byte3 = valueA.ToString("X4").Substring(0, 2);
                string byte4 = valueA.ToString("X4").Substring(2, 2);
                string byte5 = valueB.ToString("X4").Substring(0, 2);
                string byte6 = valueB.ToString("X4").Substring(2, 2);
                motorA[2] = Convert.ToByte(byte3, 16);
                motorA[3] = Convert.ToByte(byte4, 16);
                motorB[2] = Convert.ToByte(byte5, 16);
                motorB[3] = Convert.ToByte(byte6, 16);
                if (ConnectionForm.serialPort1.IsOpen)
                {
                    ConnectionForm.serialPort1.Write(motorA, 0, motorA.Length);
                    ConnectionForm.serialPort1.Write(motorB, 0, motorB.Length);
                }
            }

            else if (XAngle > -45.0f && XAngle <= 0.0f && LinearAcceleration() > 0)
            {

                float LAmagnified = LinearAcceleration() * 36;
                int valueLA = (int)LAmagnified;
                int value = (int)XAngle;
                int valueA = 511 + (value * 8) + (-valueLA);
                int valueB = 511 - (value * 8) + (valueLA);
                if (valueA < 151)
                {
                valueA = 151;
                }
                if (valueB > 871)
                {
                valueB = 871;
                }
                string byte3 = valueA.ToString("X4").Substring(0, 2);
                string byte4 = valueA.ToString("X4").Substring(2, 2);
                string byte5 = valueB.ToString("X4").Substring(0, 2);
                string byte6 = valueB.ToString("X4").Substring(2, 2);
                motorA[2] = Convert.ToByte(byte3, 16);
                motorA[3] = Convert.ToByte(byte4, 16);
                motorB[2] = Convert.ToByte(byte5, 16);
                motorB[3] = Convert.ToByte(byte6, 16);
                if (ConnectionForm.serialPort1.IsOpen)
                {
                    ConnectionForm.serialPort1.Write(motorA, 0, motorA.Length);
                    ConnectionForm.serialPort1.Write(motorB, 0, motorB.Length);
                }
            }

            if (ZAngle < 45.0f && ZAngle >= 0.0f)
            {
                
                float AVmagnified = AngularVelocity() * 90;
                int valueAV = (int)AVmagnified;
                int value = (int)ZAngle;
                value = 511 + (value * 8) + (valueAV);
                //Debug.Log(valueAV);
                string byte3 = value.ToString("X4").Substring(0, 2);
                string byte4 = value.ToString("X4").Substring(2, 2);
                motorA[2] = Convert.ToByte(byte3, 16);
                motorA[3] = Convert.ToByte(byte4, 16);
                motorB[2] = Convert.ToByte(byte3, 16);
                motorB[3] = Convert.ToByte(byte4, 16);
                if (ConnectionForm.serialPort1.IsOpen)
                {
                    ConnectionForm.serialPort1.Write(motorA, 0, motorA.Length);
                    ConnectionForm.serialPort1.Write(motorB, 0, motorB.Length);
                }
            }
            else if (ZAngle > -45.0f && ZAngle <= 0.0f)
            {

                float AVmagnified = AngularVelocity() * 90;
                int valueAV = (int)AVmagnified;
                int value = (int)ZAngle;
                value = 511 + (value * 8) + (valueAV);
                string byte3 = value.ToString("X4").Substring(0, 2);
                string byte4 = value.ToString("X4").Substring(2, 2);
                motorA[2] = Convert.ToByte(byte3, 16);
                motorA[3] = Convert.ToByte(byte4, 16);
                motorB[2] = Convert.ToByte(byte3, 16);
                motorB[3] = Convert.ToByte(byte4, 16);
                if (ConnectionForm.serialPort1.IsOpen)
                {
                    ConnectionForm.serialPort1.Write(motorA, 0, motorA.Length);
                    ConnectionForm.serialPort1.Write(motorB, 0, motorB.Length);
                }
            }
        }
    

    private float LinearAcceleration()
    {

        if (samples < 12)
        {

            samples = 12;
        }

        //Initialize
        if (LinearAccelerationSamples == null)
        {

            LinearAccelerationSamples = new float[samples];
        }

        //Fill the position and time sample array and shifts to the next slot in the array 
        //each time a new sample is taken. Index  holds the oldest sample.
        //The highest index will always hold the newest sample. 
        for (int i = 0; i < LinearAccelerationSamples.Length - 1; i++)
        {
            LinearAccelerationSamples[i] = LinearAccelerationSamples[i + 1];
        }

        LinearAccelerationSamples[LinearAccelerationSamples.Length - 1] = (Van.velocity.magnitude - PreviousLinearV) / Time.fixedDeltaTime;
        PreviousLinearV = Van.velocity.magnitude;
        sampleno++;

        //The output acceleration can only be calculated if enough samples are taken.
        if (sampleno >= samples)
        {

            float TotalLinearAcclerationSamples = 0;
            foreach (float sampleA in LinearAccelerationSamples)
            {
                TotalLinearAcclerationSamples += sampleA;
            }

            AverageLinearAccleration = TotalLinearAcclerationSamples / LinearAccelerationSamples.Length;

            if (!VanGoingForward())
            {
                AverageLinearAccleration = -AverageLinearAccleration;
            }
           // Debug.Log(AverageLinearAccleration);
        }
        return AverageLinearAccleration;
    }
   

    private float AngularVelocity()
    {

        float AngularVelocity = Van.angularVelocity.y;
        Debug.Log(AngularVelocity);
        return AngularVelocity;
    }


    public bool VanGoingForward()
    {
        bool VanGoingForward;
        var VanVelocity = transform.InverseTransformDirection(Van.velocity);
        if (VanVelocity.z > 0)
        {
            VanGoingForward = true;
        }
        else
        {
            VanGoingForward = false;
        }
        return VanGoingForward;
    }


}
