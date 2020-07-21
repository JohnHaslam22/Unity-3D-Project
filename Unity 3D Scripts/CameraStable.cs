using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour {

    public GameObject JohnsVan;
    public float CarX;
    public float CarY;
    public float CarZ;
    

    void Update () {
        CarX = JohnsVan.transform.eulerAngles.x;
        CarY = JohnsVan.transform.eulerAngles.y;
        CarZ = JohnsVan.transform.eulerAngles.z;

        if (CarZ > -45.0f && CarZ < 45.0f)
        {
            transform.eulerAngles = new Vector3(CarX - CarX, CarY, CarZ);
        }
        else
        {
            transform.eulerAngles = new Vector3(CarX - CarX, CarY, CarZ - CarZ);
        }
    }
}
