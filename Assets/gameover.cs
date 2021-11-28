using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public bool isover = false;
    public GameObject gameoverUI;
    public GameObject worldCamera;

    void Start() {
        gameoverUI.SetActive(false);
    }
    void Update() {
        if (worldCamera.activeSelf) {
            isover = true;
        }

        if (isover == true) {
            gameoverUI.SetActive(true);
        } else {
            gameoverUI.SetActive(false);
        }
    }

    public void restart() {
        isover = false;
        SceneManager.LoadScene(0);
        gameoverUI.SetActive(false);
        Debug.Log("re");
    }

    public void exit() {
        Application.Quit();
        Debug.Log("quit");
    }
}
