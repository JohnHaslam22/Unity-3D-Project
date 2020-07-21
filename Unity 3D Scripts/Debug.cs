using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour {

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;

    float xAngle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        eulerAngX = transform.localEulerAngles.x;
        eulerAngY = transform.localEulerAngles.y;
        eulerAngZ = transform.localEulerAngles.z;

        // add the math for your need using if statemnts
        // ...if ( eulerAngX  > 180f) ... code go here...[/B]

        // Get X    [ ATT: SIMILAR TO THIS  EXAMPLE  ]
 
        if (  eulerAngY  > 180.0f)
          {
            if (eulerAngX > 256.0f)
            {
                xAngle = (eulerAngX * -1.0f) + 360.0f;
            }
            else
            {
                xAngle = -eulerAngX;
            }
          } else {

            if (eulerAngX > 256.0f)
            {
                xAngle = eulerAngX - 180.0f;
            }
            else
            {
                xAngle = ((eulerAngX * -1.0f) + 180.0f) * -1.0f;
            }
          }

        Debug.Log(xAngle);
    }
}

