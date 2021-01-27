using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour
{
    Vector3 origin;
    bool isRotating = false;

    public float targetX = 0;
    public float targetY = 0;
    public float RotateSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        origin = gameObject.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {

            transform.Rotate(Vector3.forward, Time.deltaTime * RotateSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToCenter();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveToOrigin();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SpinOn();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Grow();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Shrink();
        }
    }

    public void MoveToCenter()
    {
        LeanTween.moveX(gameObject, targetX, 0.4f);
        LeanTween.moveY(gameObject, targetY, 0.4f);
    }

    public void MoveToOrigin()
    {
        LeanTween.moveX(gameObject, origin.x, 0.4f);
        LeanTween.moveY(gameObject, origin.y, 0.4f);
    }

    public void SpinOn()
    {
        print("Spin On");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        isRotating = true;
    }
    public void SpinOff()
    {
        print("Spin Off");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        isRotating = false;
    }

    public void Grow()
    {
        LeanTween.scaleX(gameObject, 1.5f, 0.4f).setEaseInElastic();
        LeanTween.scaleY(gameObject, 1.5f, 0.4f).setEaseInElastic();
    }
    public void Shrink()
    {
        LeanTween.scaleX(gameObject, 1f, 0.4f);
        LeanTween.scaleY(gameObject, 1f, 0.4f);
    }
}
