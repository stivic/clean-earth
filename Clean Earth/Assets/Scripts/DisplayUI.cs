using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    public Text karmaTxt;
    public Text numInvItemTxt;
    GameObject myself;
    GameObject[] finishObjects;
    GameObject[] playObjects;
    GameObject waveTxt;

    public void Start()
    {
        waveTxt = GameObject.FindGameObjectWithTag("waveText");
        myself = GameObject.FindGameObjectWithTag("myself");
        playObjects = GameObject.FindGameObjectsWithTag("showOnPlay");
        finishObjects = GameObject.FindGameObjectsWithTag("showOnFinish");
        hidePlay();
        hideFinished();
    }

    void Update()
    {   // uvijek se osvjezavaju stvari, score se jos treba povuc iz playera
        karmaTxt.text = "Karma : " + myself.GetComponent<PlayerInfo>().GetKarma(); ;
        numInvItemTxt.text = "Inventory : " + myself.GetComponent<Inventory>().inventory.Count + "/" + myself.GetComponent<Inventory>().inventory.Capacity;
        //ako je igra u toku onda prikazi playObjects, ako nije onda finishObjects
        
    }

    public void ShowWaveTxt(int waveNum)
    {
        gameObject.SetActive(true);
        GetComponent<Text>().text = "Wave " + waveNum;
        Invoke("DisableTxt", 3);
    }

    void DisableWavetxt()
    {
        gameObject.SetActive(false);
    }

    void showFinished()
    {
        foreach(GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    void showPlay()
    {
        foreach (GameObject g in playObjects)
        {
            g.SetActive(true);
        }
    }

    void hidePlay()
    {
        foreach (GameObject g in playObjects)
        {
            g.SetActive(false);
        }
    }
}
