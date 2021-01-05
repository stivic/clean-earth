using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    Transform cameraTransform;
    private Transform playerTransform;
    private const float minY = -103f;
    private const float minX = -112f;
    private const float maxY = 103f;

    private const float maxX = 97f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform =  GameObject.FindGameObjectsWithTag("myself")[0].transform;
        
        cameraTransform = GameObject.FindGameObjectWithTag("Minimap").transform;
        Debug.Log("Camera tr: " + cameraTransform.position);
    }

    void LateUpdate()
    {
        int cameraSize = 50;

        Vector3 newPosition = playerTransform.position;
        newPosition.z = cameraTransform.position.z;
        Debug.Log("Pozicija igraca:" + playerTransform.position);

        if( (playerTransform.position.x + cameraSize) > maxX | (playerTransform.position.x - cameraSize) < minX )
        { 
            newPosition.x = cameraTransform.position.x;
        }

        if( (playerTransform.position.y + cameraSize) > maxY | (playerTransform.position.y - cameraSize) < minY )
        { 
            newPosition.y = cameraTransform.position.y;
        }

        cameraTransform.position = newPosition;
    }
}
