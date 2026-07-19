using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Hunt : AIBaseStateClass
{
    public float hearingRange = 10;
    
    private bool heardSound;
    private Vector3 soundPos;
    private SpiritBox spiritBoxRef;
    private PlayerSoundGO playerFoundRef;

    // Start is called before the first frame update
    void Start()
    {
        ghost.body.SetActive(true);
        ghost.sphereCol.enabled = true;
        ghost.sphereCol.radius = hearingRange;
    }

    public override void UpdateLogic()
    {
        /*huntTimer -= Time.deltaTime;
        if (huntTimer < 0)
        {
            ghost.agent.speed = ghost.ghostNormalSpeed;
            ghost.body.SetActive(false);
            ghost.stateChangeMachineRef.triggerHunt = true;
            ghost.SwitchState(GhostsAIController.States.Idle);
        }*/


        if (heardSound)
        {
            
            float dist = Vector3.Distance(transform.position, soundPos);

            if (dist <= 2)
            {
                ghost.agent.ResetPath();
                ghost.agent.velocity = Vector3.zero;
                if (spiritBoxRef != null)
                {
                    spiritBoxRef.hasJiangshiTouched = true;
                    spiritBoxRef = null;
                }
                else if (playerFoundRef != null)
                {
                    //Replace with player death check logic
                    playerFoundRef.hasJiangshiTouched = true;
                    playerFoundRef = null;
                }
                ghost.agent.speed = ghost.ghostNormalSpeed;
                heardSound = false;
            }
            
        }

        else
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

    private void OnTriggerEnter(Collider other)
    {
        if(spiritBoxRef == null)
        {
            if (other.GetComponent<SpiritBox>()) DetectSound(other.transform.position, other.gameObject.GetComponent<SpiritBox>(), null);

            else if (other.GetComponent<PlayerSoundGO>()) DetectSound(other.transform.position, null, other.gameObject.GetComponent<PlayerSoundGO>());
        }

        
    }
    public void DetectSound(Vector3 soundPosition, SpiritBox spiritBox, PlayerSoundGO player)
    {
        if (spiritBox != null)
        {
            if (!spiritBox.hasJiangshiTouched)
            {
                ghost.agent.SetDestination(soundPosition);
                ghost.agent.speed = ghost.chosenGhost.ghostSpeed + 1.5f;
                heardSound = true;
                soundPos = soundPosition;
                spiritBoxRef = spiritBox;
                return;
            }
        }

        if (player != null)
        {
            if (!player.hasJiangshiTouched)
            {
                ghost.agent.SetDestination(soundPosition);
                ghost.agent.speed = ghost.chosenGhost.ghostSpeed + 1.5f;
                heardSound = true;
                soundPos = soundPosition;
                playerFoundRef = player;
                return;
            }
        }
    }
    private void OnDestroy()
    {
        ghost.agent.speed = ghost.ghostNormalSpeed;
        ghost.body.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
