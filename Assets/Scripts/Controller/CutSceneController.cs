using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{

    public Sprite[] introStory = new Sprite[3];
    public Sprite[] outroStory = new Sprite[3];

    public Button button;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(5f);
        print("CLICK TO CONTINUE");

    }
}
