using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPathController : MonoBehaviour {
    public List<Transform> m_patrolPoints;
    private int m_currentPatrolPointIndex;
    public Transform lastPoint;

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            m_patrolPoints.Add(child);
            RaycastHit raycastHit;
            Physics.Raycast(child.position + new Vector3(0, 1), Vector3.down, out raycastHit);
            child.position = raycastHit.point + Vector3.up * 0.1f;
        }
        m_currentPatrolPointIndex = m_patrolPoints.Count;
        lastPoint = m_patrolPoints[0];
	}

    public int getPointsNumber()
    {
        return m_patrolPoints.Count;
    }

    // Chooses either last patrol point or 
    // Picks next point depending on distance to last point 
    public Transform getRealPatrolPoint(Vector3 fromPosition)
    {
        //print(Vector3.Distance(fromPosition, lastPoint.position));
        if (Vector3.Distance(fromPosition, lastPoint.position) < 3)
        {
            lastPoint = getPatrolPoint();
        }
            
        return lastPoint;
    }

    // Пока что не используется
    public Transform getClosestPoint(Vector3 fromPosition)
    {
        float minDistance = Vector3.Distance(fromPosition, m_patrolPoints[0].transform.position);
        Transform minPoint = m_patrolPoints[0];
        foreach (var point in m_patrolPoints)
        {
            float dist = Vector3.Distance(fromPosition, point.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                minPoint = point;
            }
        }
        return minPoint;
    }

    private Transform getPatrolPoint()
    {
        m_currentPatrolPointIndex++;
        if (m_currentPatrolPointIndex >= m_patrolPoints.Count)
            m_currentPatrolPointIndex = 0;
        return m_patrolPoints[m_currentPatrolPointIndex];
    }
	
}
