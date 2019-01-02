using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class NetPrediction_FateLogic : NetworkBehaviour
{
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonController;
    public NavMeshAgent agent { get; private set; }
    public Transform target;                                    // target to aim for
    public NetAICharacterControl AICharControl;
    public GameObject owner;

    public bool m_activated = false;
    [Range(0.1f, 1.0f)]
    public float m_activated_delay = 0.2f;

    private float timeLeft;

    // Use this for initialization
    void Start()
    {
        thirdPersonController = gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
    }

    public void activateTransition()
    {
        m_activated = true;
    }

    public void SetTimeLeft(float time)
    {
        timeLeft = time;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (m_activated)
        {
            m_activated_delay -= Time.deltaTime;
            if (m_activated_delay < 0)
            {
                print("teleport");
                Vector3 predictionPos = transform.position;
                owner.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(predictionPos);
                owner.transform.position = predictionPos + Vector3.up * 0.1f;
                owner.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                NetworkServer.Destroy(gameObject);
                return;
            }
        }
        if (timeLeft < 0 && !m_activated)
        {
            DestroySpell();
            return;
        }
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            thirdPersonController.Move(agent.desiredVelocity, false, false);
        else
            thirdPersonController.Move(Vector3.zero, false, false);
    }
    
    public void SetDestination(Vector3 ownerHitPoint)
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;
        agent.updatePosition = true;

        print(ownerHitPoint);
        agent.SetDestination(ownerHitPoint);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetOwner(GameObject thatOwns)
    {
        owner = thatOwns;
    }

    public void DestroySpell()
    {
        owner.GetComponent<AudioSource>().Stop();

        NetworkServer.Destroy(gameObject);
    }

}
