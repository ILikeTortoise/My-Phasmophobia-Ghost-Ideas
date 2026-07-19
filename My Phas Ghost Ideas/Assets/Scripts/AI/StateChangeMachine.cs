using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateChangeMachine : MonoBehaviour
{
    private GhostsAIController ghost;
    //Hunting Checks
    private int huntCheck;
    private float huntAttemptFailTimer = 10f;
    private float currentHuntCoolDown;
    private float huntCooldownTimer = 25f;
    public float huntTimer = 20f;
    private bool attemptHunt;
    //Ghost State Changes
    private int ghostToDo;
    private int ghostSwitchStateChance;
    private bool switchingState;
    private float ghostSwitchStateDelay = 1f;
    private int ghostSwitchStateSuccessRate = 30;
   
    private void Start()
    {
        ghost = GetComponent<GhostsAIController>();
        ghost.sphereCol.enabled = false;
        ghostToDo = Random.Range(1, 4);
        ghostSwitchStateChance = Random.Range(0, 101);
        currentHuntCoolDown = huntCooldownTimer;
    }
    private void Update()
    {
        if(ghost.currentState != GhostsAIController.States.Inactive && ghost.currentState != GhostsAIController.States.Event)
        {
            if (!switchingState) StartCoroutine(HandleSwitchState());
            
            if (!attemptHunt)
            {
                currentHuntCoolDown -= Time.deltaTime;
                if (ghost.chosenGhost.huntThreshhold >= ghost.player.sanity && currentHuntCoolDown <= 0)
                {
                    attemptHunt = true;
                    huntCheck = Random.Range(0, 101);
                    if (huntCheck <= ghost.chosenGhost.huntChance)
                    {
                        ghost.SwitchState(GhostsAIController.States.Hunt);
                        StartCoroutine(HandleHuntState());
                    }
                    else StartCoroutine(HandleHuntAttemptFail());
                }
            }
        }
    }

    public IEnumerator HandleHuntState()
    {
        yield return new WaitForSeconds(huntTimer);
        ghost.agent.ResetPath();
        ghost.agent.velocity = Vector3.zero;
        ghost.sphereCol.enabled = false;
        ghost.body.SetActive(false);
        currentHuntCoolDown = huntCooldownTimer;
        attemptHunt = false;
        ghost.SwitchState(GhostsAIController.States.Idle);
    }

    IEnumerator HandleHuntAttemptFail()
    {
        yield return new WaitForSeconds(huntAttemptFailTimer);
        currentHuntCoolDown = huntCooldownTimer;
        attemptHunt = false;
    }

    IEnumerator HandleSwitchState()
    {
        switchingState = true;
        yield return new WaitForSeconds(ghostSwitchStateDelay);
        if (!attemptHunt)
        {
            ghostSwitchStateChance = Random.Range(0, 101);
            ghostToDo = Random.Range(1, 5);
            
            if (ghostSwitchStateChance <= ghostSwitchStateSuccessRate)
            {
                switch (ghostToDo)
                {
                    case 1:
                        ghost.SwitchState(GhostsAIController.States.Idle);
                        break;
                    case 2:
                        float wonderSuccess = Random.Range(1, 101);
                        if(wonderSuccess <= ghost.chosenGhost.wonderChance) ghost.SwitchState(GhostsAIController.States.Wonder);
                        break;
                    case 3:
                        float interactSuccess = Random.Range(1, 101);
                        if (interactSuccess <= ghost.chosenGhost.interactChance) ghost.SwitchState(GhostsAIController.States.Interact);
                        break;
                    case 4:
                        float eventSuccess = Random.Range(1, 101);
                        if (eventSuccess <= ghost.chosenGhost.eventChance) ghost.SwitchState(GhostsAIController.States.Event);
                        break;

                    default:
                        break;
                }
            }
        }
        switchingState = false;
    }
}
