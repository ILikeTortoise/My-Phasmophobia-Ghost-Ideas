using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wonder : AIBaseStateClass
{
    void Start()
    {
        ghost.chosenGhost.ghostSpeed = ghost.agent.speed;
        ghost.sphereCol.enabled = false;
        GhostWonder();
    }

    public override void UpdateLogic()
    {
        if(ghost.agent.remainingDistance <= ghost.agent.stoppingDistance)
        ghost.SwitchState(GhostsAIController.States.Idle);
    }

    void GhostWonder()
    {
        if (!ghost.agent.pathPending && (!ghost.agent.hasPath || ghost.agent.remainingDistance <= ghost.agent.stoppingDistance))
        {
            Vector3 randomDestination = Random.insideUnitSphere * 10f;
            randomDestination.y = 0;
            randomDestination += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDestination, out hit, 3f, 1))
            {
                ghost.agent.SetDestination(hit.position);
            }
        }
    }
}
