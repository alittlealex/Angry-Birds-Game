using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int starNum = 0;
    private bool isSelcet = false;

    public GameObject levelLock;
    public GameObject levelStars;

    public GameObject panel;
    public GameObject maps;

    public Text starsText;

    public int startNum;
    public int endNum;

    private void Start()
    {
        if(PlayerPrefs.GetInt("totalStars",0) >= starNum)
        {
            isSelcet = true;
        }

        if (isSelcet)
        {
            levelLock.SetActive(false);
            levelStars.SetActive(true);

            int sum = 0;
            for(int i = startNum; i <= endNum; i++)
            {
                sum += PlayerPrefs.GetInt("level" + i.ToString(),0);
            }
            starsText.text = sum.ToString() + "/" + ((endNum - startNum + 1) * 3).ToString();
        }
    }

    public void Selected()
    {
        if (isSelcet)
        {
            maps.SetActive(false);
            panel.SetActive(true);
        }
    }

}
