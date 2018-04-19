using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    private void Awake()
    {
        GameObject.Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
    }
}
