using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayUI : MonoBehaviour
{
    public Text karmaTxt;
    public Text numInvItemTxt;
    public Text currGarbageNumTxt;
    public Text allowedGarbageNumTxt;
    public Text badBoysNumTxt;
    public Text currTimeLeftTxt;
    public Text currReportNumTxt;
    public Text averageKarmaTxt;
    public Image minimap;
    public Text scoreTxt;
    GameObject myself;
    GameObject[] finishObjects;
    GameObject[] pauseObjects;

    private Text[] labels;
   // GameObject[] playObjects;
    GameObject waveTxt;

    public void Start()
    {
        
        waveTxt = GameObject.FindGameObjectWithTag("waveText");
        myself = GameObject.FindGameObjectWithTag("myself");

        labels = GetComponentsInChildren<Text>();

        foreach (var label in labels)
        {
            if (label.name.Equals("KarmaTxt"))
            {
                karmaTxt = label;
            }
        }
       // playObjects = GameObject.FindGameObjectsWithTag("showOnPlay");
        finishObjects = GameObject.FindGameObjectsWithTag("showOnFinish");
        pauseObjects = GameObject.FindGameObjectsWithTag("showOnPause");
        waveTxt.SetActive(false);
        hidePause();
        hideFinished();
        Time.timeScale = 1;
    }



    void FixedUpdate()
    {   // uvijek se osvjezavaju stvari, score se jos treba povuc iz playera
        if (myself == null)
        {
            myself = GameObject.FindGameObjectWithTag("myself");
            return;
        }
        karmaTxt.text = "Karma : " + myself.GetComponent<PlayerInfo>().GetKarma().ToString("f2"); ;
        numInvItemTxt.text = "Inventory : " + myself.GetComponent<Inventory>().inventory.Count + "/" + myself.GetComponent<Inventory>().inventory.Capacity;
        currGarbageNumTxt.text = "Garbage Count: " + WorldInit.Instance.currentGarbageCount;
        badBoysNumTxt.text = "Bad Guys: " + WorldInit.Instance.badAIPlayers.Count + "/" + WorldInit.Instance.waveNumber;
        allowedGarbageNumTxt.text = "Allowed Garbage: " + WorldInit.Instance.allowedGarbageCount;
        currTimeLeftTxt.text = "Time Left: " + Timer.Instance.timeRemaining;
        currReportNumTxt.text = "Reports Left: " + WorldInit.Instance.currentReportCount;
        averageKarmaTxt.text = "Avg Karma: " + WorldInit.Instance.GetAvgKarma().ToString("f2");
        scoreTxt.text = "Score: " + (int)WorldInit.Instance.score;
        //ako je igra u toku onda prikazi playObjects, ako nije onda finishObjects
        if (WorldInit.Instance.waveCompleted)
        {
            WorldInit.Instance.waveCompleted = false;
            ShowWaveTxt((int)WorldInit.Instance.waveNumber);
        }

        if (WorldInit.Instance.gameOver)
        {
            WorldInit.Instance.gameOver = false;
            showFinished();
        }
        

    }

    public void ShowWaveTxt(int waveNum)
    {
        waveTxt.SetActive(true);
        waveTxt.GetComponent<Text>().text = "Wave " + waveNum;
        Invoke("DisableWaveTxt", 3f);
    }

    void DisableWaveTxt()
    {
        waveTxt.SetActive(false);
    }

    private void hidePause()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        minimap.gameObject.SetActive(true);
    }

    private void showPause()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
        minimap.gameObject.SetActive(false);
    }

    private void showFinished()
    {
        foreach (GameObject g in finishObjects)
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

    //void showPlay()
    //{
    //    foreach (GameObject g in playObjects)
    //    {
    //        g.SetActive(true);
    //    }
    //}

    //void hidePlay()
    //{
    //    foreach (GameObject g in playObjects)
    //    {
    //        g.SetActive(false);
    //    }
    //}

    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPause();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePause();
        }
    }

    public void Reload()
    {
        ExitGame();
        
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        CleanEarth.GameManager.Instance.LeaveRoom();
    }

}