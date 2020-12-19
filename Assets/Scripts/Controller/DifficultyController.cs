using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{

    SceneController sceneManager;

    void Start()
    {
        sceneManager = GetComponent<SceneController>();
    }
    void SetDifficulty(string diff)
    {
        if (diff == "easy") PlayerPrefs.SetFloat("difficulty", 1f);
        else if (diff == "medium") PlayerPrefs.SetFloat("difficulty", 1.25f);
        else if (diff == "hard") PlayerPrefs.SetFloat("difficulty", 1.5f);

        sceneManager.LoadSelect();
    }

    public void setEasy()
    {
        SetDifficulty("easy");
    }
    public void setMedium()
    {
        SetDifficulty("medium");
    }
    public void setHard()
    {
        SetDifficulty("hard");
    }
}
