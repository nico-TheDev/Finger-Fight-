using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject PanelOne;
    public GameObject PanelTwo;

    public void ShowPanelOne()
    {
        PanelOne.SetActive(true);
        PanelTwo.SetActive(false);
    }
    public void ShowPanelTwo()
    {
        PanelOne.SetActive(false);
        PanelTwo.SetActive(true);
    }
}
