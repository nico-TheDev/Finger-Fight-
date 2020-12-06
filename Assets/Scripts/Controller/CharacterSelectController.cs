using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectController : MonoBehaviour
{
    public void SelectCharacter()
    {
        string selected = EventSystem.current.currentSelectedGameObject.tag;
        PlayerPrefs.SetString("playerChar", selected);
        PlayerPrefs.SetString("CutsceneState", "intro");
    }
}
