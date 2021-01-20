using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SelectState
{
    PLAYERONE,
    PLAYERTWO,
    FINAL
}

public class CharacterSelectPVPController : MonoBehaviour
{
    // character pick 
    // current picker
    // when clicked, change character picker sprite
    // confirm finalized
    // change picker

    SelectState state;
    public Text announcerText;

    public GameObject confirmBtn;
    public GameObject fightBtn;

    public Sprite[] charArt = new Sprite[3];

    public SpriteRenderer playerOne;
    public SpriteRenderer playerTwo;

    string selected = "";

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
        selected = "";
        announcerText.text = "Pick your hero: P1";
        confirmBtn.SetActive(true);
        fightBtn.SetActive(false);

        if (state == SelectState.PLAYERONE)
        {

        }
    }
    public void AssignCharacter()
    {
        selected = EventSystem.current.currentSelectedGameObject.tag;
        if (state == SelectState.PLAYERONE)
        {
            PlayerPrefs.SetString("PlayerOne", selected);
            playerOne.sprite = GetSprite(selected);
        }
        else if (state == SelectState.PLAYERTWO)
        {
            PlayerPrefs.SetString("PlayerTwo", selected);
            playerTwo.sprite = GetSprite(selected);
        }
    }

    public void ConfirmChoice()
    {
        if (state == SelectState.PLAYERONE)
        {
            confirmBtn.SetActive(false);
            fightBtn.SetActive(true);
            announcerText.text = "Pick your hero: P2";
            state = SelectState.PLAYERTWO;
        }
        else if (state == SelectState.PLAYERTWO)
        {
            announcerText.text = "Pick your hero: P2";
            state = SelectState.FINAL;
        }
    }

    Sprite GetSprite(string target)
    {
        Sprite currentSprite = charArt[0];
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
