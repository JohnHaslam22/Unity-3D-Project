using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using Vehicle.JohnsVan;
public class RaceFinish : MonoBehaviour
{

    public GameObject JohnsVan;
    public GameObject FinishCam;
    public GameObject ViewMode1;
    public GameObject LevelMusic;
    public AudioSource FinishMusic;
    public GameObject ViewMode2;
    public GameObject ViewMode3;
    public GameObject CameraSwitch;
    public GameObject OverheadCamera;
    public GameObject Wheelcamera;
    public GameObject RaceFinishTrigger;


    void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            StartCoroutine(Finish());
            JohnsVan.GetComponent<MotionPlatform>().enabled = false;
            VehicleFree.speed = 0.0f;
            JohnsVan.GetComponent<VehicleFree>().enabled = false;
            JohnsVan.GetComponent<CarScaling>().enabled = false;
            OverheadCamera.GetComponent<Something>().enabled = false;
            Wheelcamera.GetComponent<Something>().enabled = false;
            FinishCam.SetActive(true);
            ViewMode1.SetActive(false);
            ViewMode2.SetActive(false);
            ViewMode3.SetActive(false);
            LevelMusic.SetActive(false);
            CameraSwitch.SetActive(false);
            RaceFinishTrigger.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            CarController.m_Topspeed = 0.0f;
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.0f);
        //Countdown.GetComponent<Text>().text = "3";
        FinishMusic.Play();
    }

}

