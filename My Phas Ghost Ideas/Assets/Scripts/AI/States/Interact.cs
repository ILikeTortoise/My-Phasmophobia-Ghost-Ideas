using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : AIBaseStateClass
{
    private float interactRange = 10f;
    private float throwForce = 1.25f;
    private bool switchState;
    private bool thrownObject;

    private void Start()
    {
        switchState = false;
        ghost.sphereCol.enabled = true;
        ghost.sphereCol.radius = interactRange;
        StartCoroutine(scanForObjectsTimer(5f));
    }

    public override void UpdateLogic()
    {
        if (switchState)
        {
            ghost.SwitchState(GhostsAIController.States.Idle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") && !thrownObject)
        {
            thrownObject = true;
            ThrowObject(other.gameObject);
        }
    }

    void ThrowObject(GameObject throwableObject)
    {
        float randX = Random.Range(-5f, 5f) * throwForce;
        float randY = Random.Range(3f, 5f);
        float randZ = Random.Range(-5f, 5f) * throwForce;
        Rigidbody throwableRbRef = throwableObject.GetComponent<Rigidbody>();
        if (throwableRbRef != null)
        {
            throwableRbRef.AddForce(randX, randY, randZ, ForceMode.Impulse);
        }
    }

    IEnumerator scanForObjectsTimer (float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        switchState = true;
        ghost.sphereCol.enabled = false;
    }

}
    
