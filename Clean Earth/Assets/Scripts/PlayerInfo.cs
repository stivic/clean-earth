using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public bool isBadGuy = false;
    public bool wasBadGuy = false;
    public bool insideTrashCanArea = false;
    private float karma;
    private static Dictionary<string, float> karmaIncrease = new Dictionary<string, float>(){
                                                                {"PlasticBottle", 0.05f},
                                                                {"Battery", 0.07f}
                                                            };
    private static Dictionary<string, float> karmaDecrease = new Dictionary<string, float>(){
                                                                {"PlasticBottle", 0.1f},
                                                                {"Battery", 0.12f}
                                                            };
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
  
    public void IncreaseKarma(string garbageName, float coef = 1f)
    {
        karma += karmaIncrease[garbageName]*coef;
        karma = Mathf.Clamp(karma, 0f, 1f);
    }
    
    public void DecreaseKarma(string garbageName, float coef = 1f)
    {
        karma -= karmaDecrease[garbageName]*coef;
        karma = Mathf.Clamp(karma, 0f, 1f);
    }
}
