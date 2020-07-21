using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour {

    public GameObject DebugPanel1;
    public GameObject DebugPanel2;

    void Start()
    {
        DebugPanel1.gameObject.SetActive(false);
        DebugPanel2.gameObject.SetActive(false);
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (DebugPanel1.activeSelf)
            {
                DebugPanel1.gameObject.SetActive(false);
                DebugPanel2.gameObject.SetActive(false);
            }
            else if (!DebugPanel1.activeSelf)
            {
                DebugPanel1.gameObject.SetActive(true);
                DebugPanel2.gameObject.SetActive(true);
            }
        }

        }
	}

