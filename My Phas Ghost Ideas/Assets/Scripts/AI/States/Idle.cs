using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AIBaseStateClass
{
    public float favouriteRoomDetectionRange = 6f;
    private void Start()
    {
        ghost.sphereCol.enabled = true;
        ghost.sphereCol.radius = 1f;
        ghost.agent.ResetPath();
        ghost.agent.velocity = Vector3.zero;
    }

    public override void UpdateLogic()
    {
        if (Vector3.Distance(transform.position, ghost.favouriteRoom.transform.position) > favouriteRoomDetectionRange)
        {
            ghost.agent.SetDestination(ghost.favouriteRoom.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ghost.favouriteRoom.transform.position, favouriteRoomDetectionRange);
    }
}
