using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingMenu : MenuInterface
{
    // follow HMD
    public GameObject HMD_CAMERA;
    private Vector3 bias;

    void Start()
    {
        bias = new Vector3(0.0f, -1.0f, 0.0f);
    }

    void Update()
    {
        // always follow HMD
        GetComponent<Transform>().position = HMD_CAMERA.GetComponent<Transform>().position + bias;
    }


}
