using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour {

    public GameObject maps;

	public void BackToMaps()
    {
        gameObject.SetActive(false);
        maps.SetActive(true);

    }
}
