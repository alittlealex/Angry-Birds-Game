using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {
    
    /// <summary>
    /// 显示星星
    /// </summary>
    public void Show()
    {
        GameManager._instance.ShowStars();
    }
}
