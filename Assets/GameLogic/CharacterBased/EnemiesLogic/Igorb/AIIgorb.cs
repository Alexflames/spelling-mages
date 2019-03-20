using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AIIgorb : MonoBehaviour {

    private double actionPeriodicTime = 0;
    private double timeToAct = 2.0f;
    public double lambdaParam = 1.0f;
    public double minTTA = 0.5;
    public double maxTTA = 3.0;
    System.Random rand = new System.Random();
    SpellCreating scComp;

    // Movement-specific objects
    private PatrolPathController pathCtrl;
    private NavMeshAgent agent;
    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;
    public Transform target;

    // Attack-specific
    public Transform targetEnemy;
    public float detectionRange = 12;

    public AIStatus AI_status;

    public enum AIStatus
    {
        PATROL,
        ATTACK,
        DEFENSE,
        RETREAT,
        WAIT
    }

    void Awake ()
    {
        scComp = GetComponent<SpellCreating>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
    }
    
    void Start () {
        pathCtrl = transform.parent.GetComponentInChildren<PatrolPathController>();
        AI_status = pathCtrl ? AIStatus.PATROL : AIStatus.WAIT;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        actionPeriodicTime += Time.deltaTime;
        if (actionPeriodicTime > timeToAct)
        {
            ChooseAction();
            actionPeriodicTime = 0;
            timeToAct = Math.Max(0.5, exponential(rand, lambdaParam));
            timeToAct = Math.Min(3.0, timeToAct);
        }

        // ******** Obligatory actions ******** //
        Move();
    }

    public static double exponential(System.Random rand, double lambda)
    {
        double alpha = rand.NextDouble();
        return Math.Log(1 - alpha) / (-lambda);
    }

    #region Actions

    private void ChooseAction()
    {
        switch (AI_status)
        {
            case AIStatus.PATROL:
                SetMoveDir();
                break;
            case AIStatus.ATTACK:
                break;
            case AIStatus.DEFENSE:
                break;
            case AIStatus.RETREAT:
                break;
            case AIStatus.WAIT:
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        scComp.castSpell("high voltage");
    }

    // Movement
    private void SetMoveDir()
    {
        target = pathCtrl.getRealPatrolPoint(transform.position);
    }

    private void Move()
    {
        
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }

    #endregion


}
