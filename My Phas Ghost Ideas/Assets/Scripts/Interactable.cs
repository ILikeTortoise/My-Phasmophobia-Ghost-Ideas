using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private Rigidbody rbody;
    public float throwSpeed;
    public float floatDuration;

    public void Start()
    {
        rbody = GetComponent<Rigidbody>();
        //FloatingThrow();
    }
    private void Update()
    {
        floatDuration -= Time.deltaTime;
        //if (Input.GetKey(KeyCode.V)) StartCoroutine(FloatingThrow());
    }
    public void Throw()
    {
       // this.gameObject
    }

    public void Answer()
    {

    }

    public void StaticInteract()
    {

    }

   /* IEnumerator FloatingThrow()
    {
        floatDuration = 5f;
        rbody.AddForce(transform.up * throwSpeed, ForceMode.Impulse);
        if(transform.position.y >=)
    }*/
}
