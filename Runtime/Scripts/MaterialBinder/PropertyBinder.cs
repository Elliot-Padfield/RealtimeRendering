using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyBinder : MonoBehaviour
{
    public Material material;
    public Transform target;

    public string position = "Position";
    public string rotation = "Rotation";
    public string scale = "Scale";

    // Update is called once per frame
    void Update()
    {
        material.SetVector(position, target.position);
        material.SetVector(rotation, target.eulerAngles);
        material.SetVector(scale, target.localScale);
    }
}
