using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake_old : MonoBehaviour
{
    public Camera main_cam;
    float shake_amount = 0;

    void Awake()
    {
        if (main_cam == null)
            main_cam = Camera.main;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            shake(.1f, .1f);
        }
    }

    public void shake(float magnitude, float duration)
    {
        shake_amount = magnitude;
        InvokeRepeating("beginShake", 0, .01f);
        Invoke("endShake", duration);
    }
    void beginShake()
    {
        if(shake_amount > 0)
        {
            Vector3 cam_pos = main_cam.transform.position;
            float offset_x = Random.value * shake_amount * 2 - shake_amount;
            float offset_y = Random.value * shake_amount * 2 - shake_amount;
            cam_pos.x += offset_x;
            cam_pos.y += offset_y;
            main_cam.transform.position = cam_pos;
        }
    }
    void endShake()
    {
        CancelInvoke("beginShake");
        main_cam.transform.localPosition = Vector3.zero;
    }
}
