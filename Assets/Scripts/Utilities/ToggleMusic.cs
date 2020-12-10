using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleMusic : MonoBehaviour
{
    Toggle toggle;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoad;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoad;
    }

    void OnLoad(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.GetInt("isMuted") != 1)
        {
            toggle.isOn = true;

        }
        else
        {
            toggle.isOn = false;

        }
    }

    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void ManageSounds()
    {
        if (toggle.isOn)
        {

            AudioListener.volume = 0;
            PlayerPrefs.SetInt("isMuted", 0);
        }
        else
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("isMuted", 1);
        }
    }


}
