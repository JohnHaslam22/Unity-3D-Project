 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogitechControls : MonoBehaviour {

    LogitechGSDK.DIJOYSTATE2ENGINES rec;
    [Range(0, 65535)]
    public int acceleratorValue;

    [Range(0.001f, 1f)]
    public float sendThisAcceleration;

    [Range(0, 65535)]
    public int brakeValue;

    [Range(0.001f, 1f)]
    public float sendThisBrake;

    public int steeringValue;

    private EasyMotion easyMotion;

    private void Start()
    {
        LogitechGSDK.LogiSteeringInitialize(false);
        easyMotion = GetComponent<EasyMotion>();


    }

    private void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            MapAxesValues();
            ApplyDamperForce();
            ApplySpringForce();
        }
    }

    private void MapAxesValues()
    {
        rec = LogitechGSDK.LogiGetStateUnity(0);
        acceleratorValue = 32767 - rec.lY;
        sendThisAcceleration = (float)acceleratorValue / (float)65535;
        brakeValue = 32767 - rec.lRz;
        sendThisBrake = (float)brakeValue / (float)65535;
        steeringValue = rec.lX;
    }

    private void OnApplicationQuit()
    {
        LogitechGSDK.LogiStopConstantForce(0);
        LogitechGSDK.LogiSteeringShutdown();
    }

    private void ApplyDamperForce()
    {
        float dampingConstant = 2.5f;
        dampingConstant *= VehicleSpeed();
        int dampingAmount = (int)dampingConstant;
        if (dampingAmount < 15)
        {
            dampingAmount = 15;
        }
        LogitechGSDK.LogiPlayDamperForce(0, dampingAmount);
    }

    private float VehicleSpeed()
    {
        return easyMotion.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
    }

    private void ApplySpringForce()
    {
        float springConstant = 2.5f;
        springConstant *= VehicleSpeed();
        int springAmount = (int)springConstant;

        if (Mathf.Abs(VehicleLateralAcceleration()) > 0)
        {
            int saturation = (int)VehicleLateralAcceleration() * 50;
            LogitechGSDK.LogiPlaySpringForce(0, 0, saturation, springAmount);
        }
        else
        {
            LogitechGSDK.LogiStopSpringForce(0);
        }
    }

    private float VehicleLateralAcceleration()
    {
        return easyMotion.ReturnLateralAcceleration();
    }
}
