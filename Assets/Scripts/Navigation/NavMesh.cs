using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public List<Transform> targets;
    public Transform target;

    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = targets[Random.Range(0, targets.Count)];
    }

    void Update()
    {
        agent.destination = target.position;
        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            target = targets[Random.Range(0, targets.Count)];
        }
    }
}
