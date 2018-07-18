using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prediction_FateLogic : MonoBehaviour {

    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonController;
    public NavMeshAgent agent { get; private set; }
    public Transform target;                                    // target to aim for

    // Use this for initialization
    void Start () {
        thirdPersonController = gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        agent = gameObject.GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            thirdPersonController.Move(agent.desiredVelocity, false, false);
        else
            thirdPersonController.Move(Vector3.zero, false, false);
    }

    public void SetDestination(RaycastHit hit)
    {
        agent.SetDestination(hit.point);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
