using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    private Animator anim;
    public GameObject background;
    public GameObject pauseBtn;

    public void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// 暂停面板收入动画播放完后调用
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void ResumeAnimEnd()
    {
        background.SetActive(false);
        pauseBtn.SetActive(true);
    }

    /// <summary>
    /// 暂停面板弹出动画播放完后调用
    /// </summary>
    public void Pause()
    {
        background.SetActive(true);
        pauseBtn.SetActive(false);
        anim.SetBool("isPause", true);
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 点击暂停面板中的重新开始调用
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// 点击暂停面板中的返回主菜单调用
    /// </summary>
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
