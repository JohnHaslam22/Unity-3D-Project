using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngZ;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float XAngle;
    [SerializeField]
    float YAngle;
    [SerializeField]
    float ZAngle;

    public GameObject XBox;
    public GameObject YBox;
    public GameObject ZBox;

    void Update()
    {
        eulerAngX = transform.localEulerAngles.x;
        eulerAngY = transform.localEulerAngles.y;
        eulerAngZ = transform.localEulerAngles.z;
        if (eulerAngX >= 180.0f && eulerAngX <= 360.0f)
        {
            XAngle = -((eulerAngX * -1.0f) + 360.0f);
        }
        else
        {
            XAngle = eulerAngX;
        }
        if (eulerAngY >= 180.0f && eulerAngY <= 360.0f)
        {
            YAngle = -((eulerAngY * -1.0f) + 360.0f);
        }
        else
        {
            YAngle = eulerAngY;
        }
        if (eulerAngZ >= 180.0f && eulerAngZ <= 360.0f)
        {
            ZAngle = -((eulerAngZ * -1.0f) + 360.0f);
        }
        else
        {
            ZAngle = eulerAngZ;
        }
        XBox.GetComponent<Text>().text = "" + XAngle;
        YBox.GetComponent<Text>().text = "" + YAngle;
        ZBox.GetComponent<Text>().text = "" + ZAngle;
    }
}