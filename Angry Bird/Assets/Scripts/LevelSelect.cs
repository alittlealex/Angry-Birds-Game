using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public bool isSelect = false;
    public Sprite levelIcon;
    private Image image;
    public GameObject[] stars;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        transform.Find("Num").GetComponent<Text>().text = gameObject.name;
    }

    private void Start()
    {
        //如果是第一个就可以选择
        if(gameObject.transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {
            int beforeNum = int.Parse(gameObject.name) - 1;
            if (beforeNum == 24) Debug.Log(PlayerPrefs.GetInt("level" + beforeNum.ToString()));
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString()) > 0)
            {
                isSelect = true;
            }
        }

        //如果可以选择，则把锁图标换成关卡图标
        if (isSelect)
        {
            image.overrideSprite = levelIcon;
            transform.Find("Num").gameObject.SetActive(true);

            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            if(count > 0)
            {
                for(int i = 0; i < count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// 点击关卡事件
    /// </summary>
    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
        }
    }
}
