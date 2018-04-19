using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMaps : MonoBehaviour {

    public GameObject maps;
    public GameObject[] panels;

    void Start () {
        string nowLevel = PlayerPrefs.GetString("nowLevel");

        if (nowLevel != "")
        {
            maps.SetActive(false);
            if (int.Parse(nowLevel.Substring(5)) <= 3)
            {
                panels[0].SetActive(true);
            }
            else if(int.Parse(nowLevel.Substring(5)) <= 24 && int.Parse(nowLevel.Substring(5)) >= 22)
            {
                panels[1].SetActive(true);
            }
            else
            {
                panels[2].SetActive(true);
            }
        }
    }
}
