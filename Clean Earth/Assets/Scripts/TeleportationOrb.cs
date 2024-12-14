using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
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
        
        myRigidBody.velocity = velocity.normalized * speed;
        Invoke("SetActive", .5f);
        Destroy(gameObject, duration);
    }

    private void SetActive()
    {
        isActive = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            isActive = false;
            SwapPlayers(other.transform);
        }
    }
    
    private void SwapPlayers(Transform ai)
    {
        if (player.GetComponent<PlayerInfo>().wasBadGuy)
        {
            player.GetComponent<PlayerInfo>().wasBadGuy = false;
        }
        SwapPlayerPosition(player.transform, ai);
        if (ai.GetComponent<PlayerInfo>().isBadGuy)
        {
            List<Transform> aiPlayers = WorldInit.Instance.aiPlayers;
            Transform newAI = aiPlayers[Random.Range(0, aiPlayers.Count-1)];
            SwapPlayerInfo(player.GetComponent<PlayerInfo>(), newAI.GetComponent<PlayerInfo>());
            player.GetComponent<PlayerInfo>().wasBadGuy = true;
            SwapPlayerInventory(player.GetComponent<Inventory>(), newAI.GetComponent<Inventory>()); 
            SwapPlayerPosition(ai, newAI);
            
        }
        else
        {
            
            SwapPlayerInfo(player.GetComponent<PlayerInfo>(), ai.GetComponent<PlayerInfo>());
            SwapPlayerInventory(player.GetComponent<Inventory>(), ai.GetComponent<Inventory>()); 
        }
        
    }

    private void SwapPlayerPosition(Transform p1, Transform p2)
    {
        Vector3 temp = p1.position;
        p1.SetPositionAndRotation(p2.position, Quaternion.identity);
        p2.SetPositionAndRotation(temp, Quaternion.identity);
    }

    private void SwapPlayerInfo(PlayerInfo p1, PlayerInfo p2)
    {
        //Swap player karma's
        float temp = p1.GetKarma();
        p1.SetKarma(p2.GetKarma());
        p2.SetKarma(temp);

    }

    private void SwapPlayerInventory(Inventory i1, Inventory i2)
    {
        List<String> temp = i1.inventory;
        i1.inventory = i2.inventory;
        i2.inventory = temp;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("myself"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
            return;
        }
        isActive = false;
        Destroy(gameObject);
    }
}
