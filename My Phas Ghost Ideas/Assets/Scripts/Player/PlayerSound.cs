using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public GameObject sound;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MakeASound();
        }
    }
    void MakeASound()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if ((Physics.Raycast(ray, out hit)))
            Instantiate(sound, hit.point, Quaternion.identity);
    }
}
