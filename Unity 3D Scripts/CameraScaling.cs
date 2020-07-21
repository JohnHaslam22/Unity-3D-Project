using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour {
    public float speed = 5000f;
    Vector3 temp;
    public GameObject JohnsVan;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        {

            if (Input.GetMouseButton(0))
            {
                if (JohnsVan.transform.localScale.x < 0.2f)
                {
                    temp = transform.position;                    
                    temp.y += 0.16f;
                    temp.z -= 0.28f;
                    transform.localScale = temp;

                }
                else if (JohnsVan.transform.localScale.x > 0.2f)
                {
                    temp = transform.position;                   
                    temp.y -= 0.16f;
                    temp.z += 0.28f;
                    transform.localScale = temp;
                }
            }
            else if (Input.GetMouseButton(1))
            {
                if (JohnsVan.transform.localScale.x < 5.0f)
                {

                    temp = transform.position;                    
                    temp.y += 0.16f;
                    temp.z -= 0.28f;
                    transform.localScale = temp;

                }
                else if (JohnsVan.transform.localScale.x > 5.0f)
                {

                    temp = transform.position;                
                    temp.y -= 0.16f;
                    temp.z += 0.28f;
                    transform.localScale = temp;

                }

            }
        }

    }
}
