using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sanity;
    public float speed = 10f;
    public float footStepInterval = 0.5f;
    private float maxFootStepInterval;
    private CharacterController charCont;
    public GameObject sound;
    public Camera playerCam;
    private GameObject soundGO;

    private void Start()
    {
        charCont = GetComponent<CharacterController>();
        maxFootStepInterval = footStepInterval;
    }

    private void Update()
    {
        //sanity -= Time.deltaTime;
        footStepInterval -= Time.deltaTime;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ);
        movement = (moveX * transform.right) + (moveZ * transform.forward);
        charCont.Move(movement * Time.deltaTime * speed);

        if ((moveX > 0 || moveZ > 0 || moveX < 0 || moveZ < 0) && footStepInterval <= 0)
        {
            footStepInterval = maxFootStepInterval;
            soundGO = Instantiate(sound, transform.position, Quaternion.identity);
        }

    }
}
