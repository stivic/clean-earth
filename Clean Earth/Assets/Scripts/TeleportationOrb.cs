using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportationOrb : MonoBehaviour
{
    public float speed;
    public float duration;
    public bool isActive = false;
    private GameObject player;
    public Rigidbody2D myRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("myself");
    }

    public void Setup(Vector2 velocity)
    {
        isActive = true;
        myRigidBody.velocity = velocity.normalized * speed;
        Destroy(gameObject, duration);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            isActive = false;
            SwapPlayers(other.transform.parent.GetComponent<Transform>());
        }
    }
    
    private void SwapPlayers(Transform ai)
    {
        Vector3 temp = player.transform.position;
        player.transform.SetPositionAndRotation(ai.gameObject.transform.position, Quaternion.identity);
        ai.SetPositionAndRotation(temp, Quaternion.identity);
        SwapPlayerInfo(player.GetComponent<PlayerInfo>(), ai.GetComponent<PlayerInfo>());
        SwapPlayerInventory(player.GetComponent<Inventory>(), ai.GetComponent<Inventory>());
    }

    private void SwapPlayerInfo(PlayerInfo p1, PlayerInfo p2)
    {
        //Swap player karma's
        float temp = p1.GetKarma();
        p1.SetKarma(p2.GetKarma());
        p2.SetKarma(temp);
        print("Karma: " + p1.GetKarma());
    }

    private void SwapPlayerInventory(Inventory i1, Inventory i2)
    {
        List<String> temp = i1.inventory;
        i1.inventory = i2.inventory;
        i2.inventory = temp;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        isActive = false;
        Destroy(gameObject);
    }
}
