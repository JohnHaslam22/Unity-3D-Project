using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour
{

    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;

    public GameObject MinuteDisplay1;
    public GameObject SecondDisplay1;
    public GameObject MilliDisplay1;
    public GameObject MinuteDisplay2;
    public GameObject SecondDisplay2;
    public GameObject MilliDisplay2;

    public GameObject LapTimeBox;

    public GameObject LapCounter1;
    public GameObject LapCounter2;
    public int LapsComplete;

    public float RawTime;

    public GameObject RaceFinish;

    void Update ()
    {
        if (LapsComplete == 0)
        {
            RaceFinish.SetActive (true);
        }
    }

    void OnTriggerEnter()
    {
        LapsComplete += 1;
        RawTime = PlayerPrefs.GetFloat("RawTime");
        if (LapTimeManager.RawTime <= RawTime)
        {

            if (LapTimeManager.SecondCount <= 9)
            {
                SecondDisplay1.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
                SecondDisplay2.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
            }
            else
            {
                SecondDisplay1.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";
                SecondDisplay2.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";
            }

            if (LapTimeManager.MinuteCount <= 9)
            {
                MinuteDisplay1.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ".";
                MinuteDisplay2.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ".";
            }
            else
            {
                MinuteDisplay1.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ".";
                MinuteDisplay2.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ".";
            }

            MilliDisplay1.GetComponent<Text>().text = "" + LapTimeManager.MilliCount;
        }
        PlayerPrefs.SetInt("MinSave", LapTimeManager.MinuteCount);
        PlayerPrefs.SetInt("SecSave", LapTimeManager.SecondCount);
        PlayerPrefs.SetFloat("MilliSave", LapTimeManager.MilliCount);
        PlayerPrefs.SetFloat("RawTime", LapTimeManager.RawTime);

        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MilliCount = 0;
        LapTimeManager.RawTime = 0;
        LapCounter1.GetComponent<Text> ().text = "" + LapsComplete;
        LapCounter2.GetComponent<Text>().text = "" + LapsComplete;
        HalfLapTrig.SetActive(true);
        LapCompleteTrig.SetActive(false);
    }
}
