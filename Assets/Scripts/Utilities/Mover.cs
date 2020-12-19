using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float speed;
    public Vector3 direction = Vector3.zero;

    public Vector3 center;
    Transform currentTransform;
    public AnimationCurve AnimCurve;
    // Start is called before the first frame update
    void Start()
    {
        currentTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        GoToCenter();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentTransform.position = Vector3.zero;
        }
    }

    void GoToCenter()
    {

        if (currentTransform.position == center)
        {

        }
        else
        {
            currentTransform.position += direction.normalized * Time.deltaTime * speed * AnimCurve.Evaluate(Time.time);
        }
    }
}
