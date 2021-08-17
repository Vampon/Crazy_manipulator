using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initView : MonoBehaviour
{
    public Camera Arcam;
    // Start is called before the first frame update
    void Start()
    {
        Arcam.fieldOfView = 42;
    }
    
}
