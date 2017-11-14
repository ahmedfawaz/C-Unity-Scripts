using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrub : MonoBehaviour
{
    public Animation anim;
    
    [Range(-1.0f, 1.0f)]
    public float inertia = -1.0f;
    public float speed = 5.0f;

    public float scrubVal = 0;

    public bool isNoTouch = false;

    Touch tch;

    void Start()
    {
        anim["CameraPath"].speed = 0;
        anim["CameraPath"].time = 1.5f;
    }

    void Update()
    {
        TouchInput();

        if (isNoTouch)
        {
            if (scrubVal > 0)
            {
            scrubVal = Mathf.Clamp(scrubVal + inertia * Time.deltaTime, 0, 1);
            anim["CameraPath"].speed = scrubVal;
            }
            if (scrubVal < 0)
            {
                scrubVal = Mathf.Clamp(scrubVal - inertia * Time.deltaTime, -1, 0);
                anim["CameraPath"].speed = scrubVal;
            }

        }
    }

    void HandleTouch(Touch tch)
    {
        scrubVal += tch.deltaPosition.x / speed * Time.deltaTime;
        scrubVal = Mathf.Clamp(scrubVal, -1, 1);
        anim["CameraPath"].speed = scrubVal;
    }

    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            isNoTouch = false;

            Debug.Log("Touch started");

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                isNoTouch = false;
                HandleTouch(Input.GetTouch(0));
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //Slowly stops animation when touch is ended
                isNoTouch = true;
                Debug.Log("Touch ended");
            }
        }
    }

}
