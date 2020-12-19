using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{

    public Sprite[] introStory = new Sprite[3];
    public Sprite[] outroStory = new Sprite[3];

    string currentState;
    string playerChar;

    public Button storyBtn;
    public GameObject continueBtn;
    public GameObject menuBtn;
    Image btnImage;
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
        btnImage = storyBtn.GetComponent<Image>();
        menuBtn.SetActive(false);
        currentState = PlayerPrefs.GetString("CutsceneState");

        if (currentState == "outro")
        {
            storyBtn.enabled = false;
            menuBtn.SetActive(true);
            continueBtn.SetActive(false);
        }
        Setup();
    }
    void Setup()
    {
        // Alien 
        // Knight
        // Samurai
        currentState = PlayerPrefs.GetString("CutsceneState");
        playerChar = PlayerPrefs.GetString("playerChar");

        if (currentState == "intro")
        {
            if (playerChar == "Alien") btnImage.sprite = introStory[0];
            else if (playerChar == "Knight") btnImage.sprite = introStory[1];
            else if (playerChar == "Samurai") btnImage.sprite = introStory[2];
        }
        else if (currentState == "outro")
        {
            if (playerChar == "Alien") btnImage.sprite = outroStory[0];
            else if (playerChar == "Knight") btnImage.sprite = outroStory[1];
            else if (playerChar == "Samurai") btnImage.sprite = outroStory[2];
        }

        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        // CHANGE TIME ON PROD
        yield return new WaitForSeconds(1f);
        print("CLICK TO CONTINUE");

    }
}
