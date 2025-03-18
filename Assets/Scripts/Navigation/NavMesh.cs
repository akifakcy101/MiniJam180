using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public List<Transform> targets;
    public Transform target;

    private NavMeshAgent agent;
    public bool isCatched = false;
    private bool isCatchedBefore = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = targets[Random.Range(0, targets.Count)];
    }

    void Update()
    {
        if (!isCatched)
        {
            if (isCatchedBefore)
            {
                isCatchedBefore = false;
                GetComponent<Rigidbody>().useGravity = false;
            }
            agent.destination = target.position;
            if (Vector3.Distance(transform.position, target.position) < 1f)
            {
                target = targets[Random.Range(0, targets.Count)];
            }
        }
        else
        {
            isCatchedBefore = true;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
