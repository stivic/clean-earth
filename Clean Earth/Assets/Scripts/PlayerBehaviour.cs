using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform playerPosition;
    public float karma;
    // Start is called before the first frame up
    void Start()
    {
        playerPosition = GetComponent<Transform>();
        karma = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
