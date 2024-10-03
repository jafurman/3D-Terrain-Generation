using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    // Start is called before the first frame update

    public void processLook(Vector2 input)
    {
        float mousex = input.x;
        float mousey = input.y;

        xRotation -= (mousey * Time.deltaTime * ySensitivity);
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mousex * Time.deltaTime) * xSensitivity);
    }
}
