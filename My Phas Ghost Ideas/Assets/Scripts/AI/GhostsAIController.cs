using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class GhostsAIController : MonoBehaviour
{
    //Here to add more Ghosts in the future
    public enum States
    {
        Inactive,
        Idle,
        Wonder,
        Interact,
        Event,
        Hunt
    }

    [HideInInspector] public StateChangeMachine stateChangeMachineRef;
    //Player
    public PlayerMovement player;

    [Header("Ghost")]
    public GameObject body;
    public AIBaseStateClass stateBaseClass;
    public List<GhostType> ghostTypes;
    [HideInInspector] public GhostType chosenGhost;
    [HideInInspector] public States currentState;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public float ghostNormalSpeed;
    [HideInInspector] public SphereCollider sphereCol;
    [HideInInspector] public GameObject favouriteRoom; //For now this will just be it's starting position
    [Header("Difficulty Settings")]
    public int setUpTimer; //Would be in some kind of GameManager or Difficulty script but for the AI showcase's sake, it's here for now
    [Range(0, 5)] public int gracePeriod;
    [HideInInspector] public int randomChanceOfActivity;
    
    void Start()
    {
        body.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        sphereCol = GetComponent<SphereCollider>();
        stateChangeMachineRef = GetComponent<StateChangeMachine>();

        int choseGhostInList = Random.Range(0, ghostTypes.Count);
        chosenGhost = ghostTypes[choseGhostInList];
        ghostNormalSpeed = agent.speed;
        favouriteRoom = GameObject.FindWithTag("Favourite Room");
        SwitchState(States.Inactive);
    }

    
    void Update()
    {
        stateBaseClass.UpdateLogic();
    }

    public AIBaseStateClass CurrentState()
    {
        return stateBaseClass;
    }

    public void SwitchState(States state)
    {
        if(currentState != state || stateBaseClass == null)
        {
            if(stateBaseClass != null) Destroy(stateBaseClass);
            
            currentState = state;
            
            switch (currentState)
            {
                case States.Inactive:
                    stateBaseClass = transform.AddComponent<Inactive>();
                    break;
                case States.Idle:
                    stateBaseClass = transform.AddComponent<Idle>();
                    break;
                case States.Wonder:
                    stateBaseClass = transform.AddComponent<Wonder>();
                    break;
                case States.Interact:
                    stateBaseClass = transform.AddComponent<Interact>();
                    break;
                case States.Event:
                    stateBaseClass = transform.AddComponent<Event>();
                    break;
                case States.Hunt:
                    stateBaseClass = transform.AddComponent<Hunt>();
                    break;
                default:
                    break;
            }
            stateBaseClass.ghost = this;
        }
        
    }
    
}
