using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCollider : MonoBehaviour
{
    public GameObject parent;
    private AIBehaviour parentScript;
    void Awake(){
        parentScript = parent.GetComponent<AIBehaviour>();
    }
    void OnTriggerEnter2D(Collider2D other){
        parentScript.RecieveTriggerEnter("distanceCollider", other);
    }
}
