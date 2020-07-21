using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject cameraOne;
    public GameObject cameraTwo;
    public GameObject cameraThree;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;
    AudioListener cameraThreeAudioLis;

    // Use this for initialization
    void Start()
    {

        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();
        cameraThreeAudioLis = cameraThree.GetComponent<AudioListener>();

        //Camera Position Set
        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
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

    //Camera Counter
    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    //Camera change Logic
    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 2)
        {
            camPosition = 0;
        }

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        //Set camera position 1
        if (camPosition == 0)
        {
            cameraOne.SetActive(true);
            cameraOneAudioLis.enabled = true;

            cameraTwoAudioLis.enabled = false;
            cameraTwo.SetActive(false);

            cameraThreeAudioLis.enabled = false;
            cameraThree.SetActive(false);
        }

        //Set camera position 2
        if (camPosition == 1)
        {
            cameraTwo.SetActive(true);
            cameraTwoAudioLis.enabled = true;

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);

            cameraThreeAudioLis.enabled = false;
            cameraThree.SetActive(false);
        }

        //Set camera position 3
        if (camPosition == 2)
        {
            cameraThree.SetActive(true);
            cameraThreeAudioLis.enabled = true;

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);

            cameraTwoAudioLis.enabled = false;
            cameraTwo.SetActive(false);
        }
    }
}
