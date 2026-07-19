using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JiangshiAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float hearingRange;
    private bool heardSound;
    private Vector3 soundPos;
    private SpiritBox spiritBoxRef;
    private PlayerSoundGO playerFoundRef;
    private float ghostSpeed;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetComponent<SphereCollider>().radius = hearingRange;
        agent.autoBraking = true;
        agent.acceleration = 20f;
        agent.angularSpeed = 720f;
        ghostSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (heardSound)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance 
                && (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f))
            {
                if (spiritBoxRef != null)
                    spiritBoxRef.hasJiangshiTouched = true;

                if (playerFoundRef != null)
                    playerFoundRef.hasJiangshiTouched = true;

                spiritBoxRef = null;
                playerFoundRef = null;
                heardSound = false;
                agent.speed = ghostSpeed;

            }
        }

        else
        {
            Vector3 randomDestination = Random.insideUnitSphere * 10f;
            randomDestination += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDestination, out hit, 10f, 1) &&
                agent.remainingDistance <= agent.stoppingDistance &&
                heardSound != true)
            {
                agent.SetDestination(hit.position);
            }
        }
       
    }

    public void DetectSound(Vector3 soundPosition, SpiritBox spiritBox, PlayerSoundGO player)
    {
        
        if (spiritBox != null)
        {
            agent.SetDestination(soundPosition);
            agent.speed = ghostSpeed + 2.5f;
            heardSound = true;
            soundPos = soundPosition;
            spiritBoxRef = spiritBox;
            return;
        }
       
        if (player != null && spiritBox == null && playerFoundRef == null)
        {
            agent.SetDestination(soundPosition);
            agent.speed = ghostSpeed +2.5f;
            heardSound = true;
            soundPos = soundPosition;
            playerFoundRef = player;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
