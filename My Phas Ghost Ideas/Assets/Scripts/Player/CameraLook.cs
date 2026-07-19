using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float sens = 3f;
    protected float rotY;
    private Camera cam;
    public GameObject player;
    private float rotX;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -45, 45);
        player.transform.Rotate(0, mouseX, 0);
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);
    }
}
