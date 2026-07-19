using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inactive : AIBaseStateClass
{
    private bool startGame;
    private bool hasSetupTimer;

    private void Start()
    {
        ghost = GetComponent<GhostsAIController>();

    }
    public override void UpdateLogic()
    {
        if (Input.anyKey && !startGame)
        {
            startGame = true;
            StartCoroutine(ActivateGhost(ghost.setUpTimer));
        }
    }
    IEnumerator ActivateGhost(float timer)
    {
        yield return new WaitForSeconds(timer);
        ghost.SwitchState(GhostsAIController.States.Idle);
    }

}
