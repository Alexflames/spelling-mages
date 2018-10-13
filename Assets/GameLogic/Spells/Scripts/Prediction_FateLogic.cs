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

    public bool m_activated = false;
    [Range (0.1f, 1.0f)]
    public float m_activated_delay = 0.2f;

    private float timeLeft;

    // Use this for initialization
    void Start () {
        thirdPersonController = gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;

        AICharControl = owner.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        SetDestination(); 
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
                Vector3 predictionPos = transform.position;
                owner.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(predictionPos);
                owner.transform.position = predictionPos + Vector3.up * 0.1f;
                owner.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                Destroy(gameObject);
            }
        }
        if (timeLeft < 0 && !m_activated)
        {
            DestroySpell();
        }
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

    public void DestroySpell()
    {
        owner.GetComponent<AudioSource>().Stop();

        Destroy(gameObject);
    }

}
