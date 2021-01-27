using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{

    public Sprite currentSprite;
    public SpriteRenderer winnerSprite;
    public Text winnerText;

    public Sprite[] charArt = new Sprite[3];

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
        string winner;
        winner = PlayerPrefs.GetString("winner");
        if (winner == "PlayerOne")
        {
            winnerSprite.sprite = GetSprite(PlayerPrefs.GetString("PlayerOne"));
            winnerText.text = "Player One Wins!";
        }
        else if (winner == "PlayerTwo")
        {
            winnerSprite.sprite = GetSprite(PlayerPrefs.GetString("PlayerTwo"));
            winnerText.text = "Player Two Wins!";
        }
    }

    Sprite GetSprite(string target)
    {

        if (target == "Knight")
        {
            currentSprite = charArt[0];
        }
        else if (target == "Samurai")
        {
            currentSprite = charArt[1];
        }
        else if (target == "Alien")
        {
            currentSprite = charArt[2];
        }
        return currentSprite;
    }
}
