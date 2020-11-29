using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void LoadSelect()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }
    public void LoadSelectPVP()
    {
        SceneManager.LoadScene("CharacterSelectScenePVP");
    }
    public void LoadBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void LoadBattlePVP()
    {
        SceneManager.LoadScene("BattleScenePVP");
    }
    public void LoadCutscene()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

}
