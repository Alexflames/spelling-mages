using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour {
    [Range (1, 100)]
    public int m_difficulty = 50;

    //private float m_timeToNextAction;
    //private bool m_reachedPatrolPoint = false;

    //private PatrolPathController pathController;
    private NavMeshAgent agent;
    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;
    public Transform target;

	// Use this for initialization
	void Start () {
        //pathController = GetComponentInChildren<PatrolPathController>();
    }
	
	// Update is called once per frame
	void Update () {
        // Movement if there is a target to move to
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }

    void ChooseAction()
    {

    }
    
    void SetTimeToNextAction()
    {
        //m_timeToNextAction = 0.5f + (100 - m_difficulty * Random.Range(0.01f, 0.025f)); // We need to think more about numbers
    }

    void MoveToPatrolPoint()
    {
        
    }
}
