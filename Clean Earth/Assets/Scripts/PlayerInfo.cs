using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private float karma;
    // Start is called before the first frame update
    void Start()
    {
        karma = Random.Range(0f, 1f);
    }

    public float GetKarma()
    {
        return karma;
    }

    public void SetKarma(float karma)
    {
        this.karma = karma;
    }
    
}
