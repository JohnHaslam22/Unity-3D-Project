using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraSwitch : MonoBehaviour
{

    public GameObject ovrcam1;
    public GameObject overheadcam;
    public GameObject wheelcam;

    void Start()
    {

        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
        wheelcam.SetActive(false);
       

        ovrcam1.SetActive(false);
       

        overheadcam.SetActive(true);
        

    }

    // Update is called once per frame
    void Update()
    {
        //Change Camera Keyboard
        switchCamera();
    }

    //UI JoyStick Method
    public void cameraPositonM()
    {
        cameraChangeCounter();
    }

    //Change Camera Keyboard
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            cameraChangeCounter();
        }
    }

    //Counts Position
    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    //Position change logic
    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 2)
        {
            camPosition = 0;
        }

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        //Sets position 1
        if (camPosition == 0)
        {
            wheelcam.SetActive(false);
            

            ovrcam1.SetActive(false);
            
            overheadcam.SetActive(true);
            
        }

        //Sets position 2
        if (camPosition == 1)
        {
            wheelcam.SetActive(false);
           
            ovrcam1.SetActive(true);
            
            overheadcam.SetActive(false);

        }

        //Sets position 3
        if (camPosition == 2)
        {
            wheelcam.SetActive(true);
            
            ovrcam1.SetActive(false);
           
            overheadcam.SetActive(false);

        }
    }
}


       