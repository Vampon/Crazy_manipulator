using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerByGyro : MonoBehaviour
{
    private const float slerpFactor = 0.5f;
    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.gyro.enabled)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, ConvertRotation(Input.gyro.attitude), slerpFactor);
            //transform.rotation = ConvertRotation(Input.gyro.attitude);
        }
    }

    private Quaternion ConvertRotation(Quaternion q)
    {
        return Quaternion.Euler(90, 0, 0) * (new Quaternion(-q.x, -q.y, q.z, q.w));
    }
}
