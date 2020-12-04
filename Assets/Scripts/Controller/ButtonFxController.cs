using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFxController : MonoBehaviour
{
    AudioSource btnFx;

    public AudioClip clickFx;

    void Start()
    {
        btnFx = gameObject.GetComponent<AudioSource>();
    }

    public void ClickFX()
    {
        btnFx.PlayOneShot(clickFx);
    }
}
