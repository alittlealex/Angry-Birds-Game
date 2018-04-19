using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour {

    public GameObject begin;
    public GameObject loading;

    IEnumerator Load()
    {
        //PlayerPrefs.DeleteAll();
        Screen.SetResolution(800, 500, false);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadSceneAsync(1);
    }

    public void BeginGame()
    {
        begin.SetActive(false);
        loading.SetActive(true);
        PlayerPrefs.SetString("nowLevel", "");
        StartCoroutine("Load");
    }
}
