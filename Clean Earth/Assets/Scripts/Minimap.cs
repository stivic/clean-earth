using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    Transform cameraTransform;
    private const float minY = -100f;
    private const float minX = -100f;
    private const float maxY = 100f;

    private const float maxX = 100f;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("Minimap").transform;
        Debug.Log("Camera tr: " + cameraTransform.position);
    }

    void LateUpdate()
    {
        int cameraSize = 100;

        Vector3 newPosition = this.transform.position;
        newPosition.z = cameraTransform.position.z;
        Debug.Log("Pozicija igraca:" + this.transform.position);

        if( (this.transform.position.x + cameraSize) > maxX | (this.transform.position.x - cameraSize) < minX )
        { 
            newPosition.x = cameraTransform.position.x;
        }

        if( (this.transform.position.y + cameraSize) > maxY | (this.transform.position.y - cameraSize) < minY )
        { 
            newPosition.y = cameraTransform.position.y;
        }

        cameraTransform.position = newPosition;
    }
}
