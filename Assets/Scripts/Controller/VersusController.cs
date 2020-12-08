using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersusController : MonoBehaviour
{
    public Image player;
    public Image enemy;

    public SceneController scene;

    AudioSource currentAudio;

    public Sprite[] characters = new Sprite[3];
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoad;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoad;
        StopCoroutine("Setup");
    }

    void OnLoad(Scene scene, LoadSceneMode mode)
    {
        FindObjectOfType<AudioManager>().StopAll();
        currentAudio = FindObjectOfType<AudioManager>().Play("VersusScene");
        Setup();
    }
    void Setup()
    {
        print("LOAD BATTLE");
        string[] chars = { "Knight", "Samurai", "Alien" };
        string enemyChar = chars[PlayerPrefs.GetInt("currentStage")];
        string playerChar = PlayerPrefs.GetString("playerChar");

        GetCharacter(enemy, enemyChar);
        GetCharacter(player, playerChar);

    }

    void Update()
    {
        if (!currentAudio.isPlaying)
        {
            scene.LoadBattle();

        }
    }


    void GetCharacter(Image cur, string character)
    {
        if (character == "Knight")
        {
            cur.sprite = characters[0];
        }
        else if (character == "Samurai")
        {
            cur.sprite = characters[1];

        }
        else if (character == "Alien")
        {
            cur.sprite = characters[2];
        }
    }
}
