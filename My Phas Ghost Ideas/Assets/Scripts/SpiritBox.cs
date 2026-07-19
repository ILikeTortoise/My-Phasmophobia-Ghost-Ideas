using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBox : MonoBehaviour
{
    public float soundRange = 5f;
    private AudioClip audio;
    private bool isOn = true;
    public bool hasJiangshiTouched = false;
    public float throwForce;
    public float explosionRad;

    private void Start()
    {
        audio = GetComponent<AudioSource>().clip;
        GetComponent<SphereCollider>().radius = soundRange;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (isOn)
        {
            if(other.tag == "Ghost" && hasJiangshiTouched != true) 
                other.GetComponent<JiangshiAI>().DetectSound(transform.position, this, null);
        }
    }*/

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0)) ThrowObject(gameObject);
    }

    void ThrowObject(GameObject throwableObject)
    {
        float randX = Random.Range(-2, 2) * throwForce;
        float randY = Random.Range(-2, 2) * throwForce;
        float randZ = Random.Range(-2, 2) * throwForce;
        Rigidbody throwableRbRef = throwableObject.GetComponent<Rigidbody>();
        if (throwableRbRef != null)
        {
            throwableRbRef.AddForce(randX, randY, randZ, ForceMode.Impulse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }

}
