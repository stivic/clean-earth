using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public bool insideTrashCanArea = false;
    private float karma;
    private static Dictionary<string, float> karmaIncrease = new Dictionary<string, float>(){
                                                                {"PlasticBottle(Clone)", 0.05f},
                                                                {"Battery(Clone)", 0.07f}
                                                            };
    private static Dictionary<string, float> karmaDecrease = new Dictionary<string, float>(){
                                                                {"PlasticBottle(Clone)", 0.1f},
                                                                {"Battery(Clone)", 0.12f}
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
        Mathf.Clamp(karma, 0f, 1f);
    }
    
    public void DecreaseKarma(string garbageName, float coef = 1f)
    {
        karma -= karmaDecrease[garbageName]*coef;
        Mathf.Clamp(karma, 0f, 1f);
    }
}
