using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleMusic : MonoBehaviour
{
    Image icon;
    bool isSoundsOn = true;
    public Sprite soundsOn;
    public Sprite soundsOff;

    Image soundsOffImg;

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
        if (PlayerPrefs.GetInt("isSoundsOn") == 1)
        {
            isSoundsOn = true;
        }
        else
        {
            isSoundsOn = false;

        }
        ManageSounds();
    }


    void Start()
    {
        icon = GetComponent<Image>();
    }
    public void ManageSounds()
    {
        if (isSoundsOn)
        {

            AudioListener.volume = 0;
            icon.sprite = soundsOff;
            isSoundsOn = false;
            PlayerPrefs.SetInt("isSoundsOn", 0);
        }
        else
        {
            AudioListener.volume = 1;
            icon.sprite = soundsOn;
            isSoundsOn = true;
            PlayerPrefs.SetInt("isSoundsOn", 1);

        }
    }


}
