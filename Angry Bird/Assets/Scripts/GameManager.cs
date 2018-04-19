using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    public LineRenderer right_Renderer;
    public LineRenderer left_Renderer;
    private Vector3 originPos;
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;

    private int starsNum;
    private int totalLevel = 63;

    private void Awake()
    {
        _instance = this;
        if(birds.Count > 0)
        {
            originPos = birds[0].gameObject.GetComponent<Transform>().position;
        }
        Initialized();
    }

    private void Initialized()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if(i == 0)
            {
                birds[i].GetComponent<Transform>().position = originPos;
                birds[i].enabled = true;
                birds[i].gameObject.GetComponent<SpringJoint2D>().enabled = true;
                right_Renderer.enabled = true;
                left_Renderer.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].gameObject.GetComponent<SpringJoint2D>().enabled = false;
            }
        }
    }

	public void NextBird()
    {
        if(pigs.Count > 0)
        {
            if(birds.Count > 0)
            {
                //下一只鸟上架
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else 
        {
            //获胜
            win.SetActive(true);
        }
    }

    public void ShowStars()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if(starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.5f);
            stars[starsNum].SetActive(true);
        }
    }

    public void Restart()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        int sum = 0;
        for(int i = 1; i <= totalLevel; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString(),0);
        }

        PlayerPrefs.SetInt("totalStars", sum);
    }
}
