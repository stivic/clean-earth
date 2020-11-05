using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private float karma;

    private float karmaIncrease = 0.05f;

    private float karmaDecrease = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        karma = Random.Range(0f, 0.5f);
    }

    public float GetKarma()
    {
        return karma;
    }

    public void SetKarma(float karma)
    {
        this.karma = karma;
    }
    
    public void IncreaseKarma()
    {
        karma += karmaIncrease;
        Mathf.Clamp(karma, 0f, 1f);
    }
    
    public void DecreaseKarma()
    {
        karma -= karmaDecrease;
        Mathf.Clamp(karma, 0f, 1f);
    }
}
