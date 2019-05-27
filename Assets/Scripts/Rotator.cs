using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public bool inXAxis = false;
    public int speed = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationVec;
        if(inXAxis)
            rotationVec = new Vector3(0, speed, 0);
        else
            rotationVec = new Vector3(speed, 2 * speed, 3 * speed);
        transform.Rotate(rotationVec * Time.deltaTime);
    }
}
