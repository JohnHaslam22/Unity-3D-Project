using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScaling : MonoBehaviour
{

    public float speed = 5000f;
    Vector3 temp;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
          
            if (transform.localScale.x > 0.2f)
            {
                temp = transform.localScale;
                temp.x -= 0.1f;
                temp.y -= 0.1f;
                temp.z -= 0.1f;
                transform.localScale = temp;
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if (transform.localScale.x < 5.0f)
            {
                
                    temp = transform.localScale;
                    temp.x += 0.1f;
                    temp.y += 0.1f;
                    temp.z += 0.1f;
                    transform.localScale = temp;
                
            }
    }
}
}



