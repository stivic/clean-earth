using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportationOrb : MonoBehaviour
{
    public float speed;
    public float duration;
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
        Destroy(this.gameObject, duration);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            SwapPlayers(other.gameObject);
        }
    }
    
    private void SwapPlayers(GameObject ai)
    {
        Vector3 temp = player.transform.position;
        player.transform.SetPositionAndRotation(ai.gameObject.transform.position, Quaternion.identity);
        ai.transform.SetPositionAndRotation(temp, Quaternion.identity);
        SwapPlayerInfo(player.GetComponent<PlayerInfo>(), ai.GetComponent<PlayerInfo>());
    }

    private void SwapPlayerInfo(PlayerInfo p1, PlayerInfo p2)
    {
        //Swap player karma's
        float temp = p1.GetKarma();
        p1.SetKarma(p2.GetKarma());
        p2.SetKarma(temp);
        print("Karma: " + p1.GetKarma());
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }

   

}
