using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{

    public Sprite[] introStory = new Sprite[3];
    public Sprite[] outroStory = new Sprite[3];

    public Button button;

    public GameObject clickMe;

    // Start is called before the first frame update
    void Start()
    {
        // Alien 
        // Knight
        // Samurai
        clickMe.SetActive(false);
        button.enabled = false;
        Image btnImage = button.GetComponent<Image>();
        string currentState = PlayerPrefs.GetString("CutsceneState");
        string playerChar = PlayerPrefs.GetString("playerChar");

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
        button.enabled = true;
        clickMe.SetActive(true);

    }
}
