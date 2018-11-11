using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Bounds should be specified!
public class NavMeshChangesScript : MonoBehaviour {
    public float bounds;
    public List<NavMeshBuildSource> watchedSources = new List<NavMeshBuildSource>();
    public AsyncOperation status;
    public LayerMask mask = -1;
    private NavMeshData nvData;
    public List<GameObject> notWalkable;
    private List<NavMeshBuildMarkup> objectsNotTreated = new List<NavMeshBuildMarkup>();

	void Awake () {
        nvData = new NavMeshData();
        foreach(var obj in notWalkable)
        {
            NavMeshBuildMarkup navMeshBuildMarkup = new NavMeshBuildMarkup();
            navMeshBuildMarkup.area = NavMesh.GetAreaFromName("Not Walkable");
            navMeshBuildMarkup.overrideArea = true;
            navMeshBuildMarkup.root = obj.transform;
            objectsNotTreated.Add(navMeshBuildMarkup);
        }
        NavMesh.AddNavMeshData(nvData);
        NavMeshBuilder.CollectSources(
            new Bounds(gameObject.transform.position, new Vector3(bounds, bounds, bounds)),
            mask.value,
            NavMeshCollectGeometry.PhysicsColliders, 
            0, 
            objectsNotTreated, 
            watchedSources);

        status = NavMeshBuilder.UpdateNavMeshDataAsync(
            nvData,
            NavMesh.GetSettingsByID(0),
            watchedSources,
            new Bounds(gameObject.transform.position, new Vector3(bounds, bounds, bounds)));
    }

    public void RebuildNavMesh()
    {
        NavMeshBuilder.CollectSources(
            new Bounds(gameObject.transform.position, new Vector3(bounds, bounds, bounds)),
            mask.value, // LayerMask.GetMask("MovingNavMesh"),
            NavMeshCollectGeometry.PhysicsColliders,
            0,
            objectsNotTreated,
            watchedSources);

        var nSet = NavMesh.GetSettingsByID(0);
        nSet.agentRadius = 0.5f;
        nSet.agentHeight = 1.5f;
        nSet.agentSlope = 60f;
        nSet.agentClimb = 1.0f;

        status = NavMeshBuilder.UpdateNavMeshDataAsync(
            nvData,
            nSet,
            watchedSources,
            new Bounds(gameObject.transform.position, new Vector3(bounds, bounds, bounds)));
    }
	
}
