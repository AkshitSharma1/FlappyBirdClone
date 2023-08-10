using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GETSCORESCRIPT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        gameObject.GetComponent<TextMeshProUGUI>().text = (GameObject.FindObjectOfType<GC>().HighScore()).ToString();

    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = (GameObject.FindObjectOfType<GC>().HighScore()).ToString();
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
