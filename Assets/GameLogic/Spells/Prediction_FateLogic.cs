using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prediction_FateLogic : MonoBehaviour {

    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonController;
    public NavMeshAgent agent { get; private set; }
    public Transform target;                                    // target to aim for
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl AICharControl;
    public GameObject owner;

    // Use this for initialization
    void Start () {
        thirdPersonController = gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;

        AICharControl = owner.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        SetDestination();
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

    public void SetDestination()
    {
        agent.SetDestination(AICharControl.hit.point);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetOwner(GameObject thatOwns)
    {
        owner = thatOwns;
    }

}
