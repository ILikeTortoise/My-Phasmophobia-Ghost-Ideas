using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundGO : MonoBehaviour
{
    private AudioSource audio;
    public float soundRange = 5f;
    public bool hasJiangshiTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        GetComponent<SphereCollider>().radius = soundRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Ghost" && hasJiangshiTouched != true)
            other.GetComponent<JiangshiAI>().DetectSound(transform.position, null, this);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }
}
