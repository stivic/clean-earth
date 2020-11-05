using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCollider : MonoBehaviour
{
    private AIBehaviour parentScript;
    void Start(){
        parentScript = transform.parent.GetComponent<AIBehaviour>();
    }
    void OnTriggerEnter2D(Collider2D other){
        parentScript.RecieveTriggerEnter("distanceCollider", other);
    }
}
