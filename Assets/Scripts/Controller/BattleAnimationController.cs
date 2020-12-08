using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAnimationController : MonoBehaviour
{
    public Image roundFight;
    public Image roundFinger;
    public Image knockout;

    public Animator fightIntro;
    public Animator fingerIntro;

    public Animator knockoutIntro;
    public void PlayFinger()
    {
        fingerIntro.SetTrigger("isPlaying");
        FindObjectOfType<AudioManager>().Play("FingerIntro");
    }
    public void PlayFight()
    {
        fightIntro.SetTrigger("isPlaying");
        FindObjectOfType<AudioManager>().Play("FightIntro");
    }
    public void PlayKO()
    {
        knockoutIntro.SetTrigger("isPlaying");
        FindObjectOfType<AudioManager>().Play("Knockout");
    }

}
