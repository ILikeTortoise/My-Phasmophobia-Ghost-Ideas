using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Event : AIBaseStateClass
{
    private float eventLength;
    // Start is called before the first frame update
    void Start()
    {
        ghost.agent.ResetPath();
        ghost.agent.velocity = Vector3.zero;
        eventLength = Random.Range(3, 10);
        ghost.sphereCol.radius = 1;
        ghost.sphereCol.enabled = true;
        StartCoroutine(EventCountDown());
    }


    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
       {
           ghost.player.sanity -= 10;
           ghost.body.SetActive(false);
           ghost.SwitchState(GhostsAIController.States.Idle);
       }
    }

    IEnumerator EventCountDown()
    {
        ghost.body.SetActive(true);
        yield return new WaitForSeconds(eventLength);
        ghost.body.SetActive(false);
        ghost.SwitchState(GhostsAIController.States.Idle);
    }

}
