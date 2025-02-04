using System;
using UnityEngine;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    public NavMeshAgent agent;
    public Collider floor;
    public float radius = 2f;
    public float offset = 3f;

    void Start()
    {
        Wander();
    }

    void Update()
    {
        if (agent.remainingDistance < .2f) 
        {
            Wander();
        }
    }

    void Seek(Vector3 pos)
    {
        agent.destination = pos; 
    }

    void Wander()
    {
        Vector3 localTarget =  UnityEngine.Random.insideUnitSphere;
        localTarget.y = 0f;
        localTarget.Normalize();
        localTarget *= radius;
        localTarget += new Vector3(0, 0, offset);

        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        if (floor.bounds.Contains(worldTarget))
        {
            Seek(worldTarget);
        }
        else
        { 
            Seek(transform.TransformPoint(-localTarget));
        }
    }
}
