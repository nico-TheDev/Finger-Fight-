using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayController : MonoBehaviour
{

    public GameObject pageOne;
    public GameObject pageTwo;

    public void ShowPageOne()
    {
        pageOne.SetActive(true);
        pageTwo.SetActive(false);
    }
    public void ShowPageTwo()
    {
        pageOne.SetActive(false);
        pageTwo.SetActive(true);
    }

}
