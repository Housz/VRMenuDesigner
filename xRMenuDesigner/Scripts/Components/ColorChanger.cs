using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material pinkMaterial;
    public Material greyMaterial;

    public void SetPink()
    {
        //Debug.Log("pink");
        SetMaterial(pinkMaterial);
    }

    public void SetGray()
    {
        SetMaterial(greyMaterial);
    }

    private void SetMaterial(Material newMaterial)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = newMaterial;
    }
}
