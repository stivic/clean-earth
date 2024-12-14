using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Minimap : MonoBehaviourPun
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
       
        cameraTransform = GameObject.FindGameObjectWithTag("Minimap").transform;
        if (GameObject.FindGameObjectWithTag("myself") == null)
        {
            return;
        } 
        playerTransform =  GameObject.FindGameObjectWithTag("myself").transform;
    }

    void LateUpdate()
    {
        if (playerTransform == null || cameraTransform == null)
        {
            if (GameObject.FindGameObjectWithTag("myself") == null)
            {
                return;
            }
            playerTransform = GameObject.FindGameObjectWithTag("myself").transform;
            return;
        }
        int cameraSize = 65;

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
