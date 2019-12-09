using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingMenu : MenuInterface
{
    // follow HMD
    public GameObject HMD_CAMERA;
    private Vector3 bias;

    bool _rotateWithHMD = false;

    void Start()
    {
        bias = new Vector3(0.0f, -1.0f, 0.0f);
    }

    void Update()
    {
        // always follow HMD (position)
        GetComponent<Transform>().position = HMD_CAMERA.GetComponent<Transform>().position + bias;

        // always follow HMD (rotation)
        if (_rotateWithHMD)
            GetComponent<Transform>().rotation = Quaternion.Euler(GetComponent<Transform>().eulerAngles.x, HMD_CAMERA.GetComponent<Transform>().eulerAngles.y, GetComponent<Transform>().eulerAngles.z);
    }

    public void SwitchRotateWithHMD()
    {
        if(_rotateWithHMD)
        {
            _rotateWithHMD = false;
        }
        else
        {
            _rotateWithHMD = true;
        }
    }


}
