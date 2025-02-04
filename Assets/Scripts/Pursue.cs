using System;
using UnityEngine;
using Unity.Muse.Behavior;
using Action = Unity.Muse.Behavior.Action;
using UnityEngine.AI;

[Serializable]
[NodeDescription(name: "pursue", story: "[Agent] pursues to [Target]", category: "Action/Move", id: "25a174031dcbe74ebf35209db33894ab")]
public class Pursue : Action
{
    public BlackboardVariable<GameObject> Agent;
    public BlackboardVariable<GameObject> Target;

    public BlackboardVariable<float> Speed = new BlackboardVariable<float>(1.0f);
    public BlackboardVariable<float> DistanceThreshold = new BlackboardVariable<float>(0.2f);

    private NavMeshAgent m_NavMeshAgent;
    private float m_PreviousStoppingDistance;


    protected override Status OnStart()
    {
        if (ReferenceEquals(Agent?.Value, null) || ReferenceEquals(Target?.Value, null))
        {
            return Status.Failure;
        }

        if (GetDistanceToLocation(out Vector3 agentPosition, out Vector3 locationPosition) <= DistanceThreshold)
        {
            return Status.Success;
        }
        m_NavMeshAgent = Agent.Value.GetComponentInChildren<NavMeshAgent>();
        if (m_NavMeshAgent != null)
        {
            m_NavMeshAgent.speed = Speed;
            m_PreviousStoppingDistance = m_NavMeshAgent.stoppingDistance;

            // Add the extents of the colliders to the stopping distance.
            float collidersOffset = 0.0f;
            Collider targetCollider = Target.Value.GetComponentInChildren<Collider>();
            if (targetCollider != null)
            {
                Vector3 colliderExtents = targetCollider.bounds.extents;
                collidersOffset = Mathf.Max(colliderExtents.x, colliderExtents.z);
            }
            Collider agentCollider = Agent.Value.GetComponentInChildren<Collider>();
            if (agentCollider != null)
            {
                Vector3 colliderExtents = agentCollider.bounds.extents;
                collidersOffset += Mathf.Max(colliderExtents.x, colliderExtents.z);
            }

            m_NavMeshAgent.stoppingDistance = DistanceThreshold + collidersOffset;
            m_NavMeshAgent.SetDestination(locationPosition);
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (ReferenceEquals(Agent?.Value, null) || ReferenceEquals(Target, null))
        {
            return Status.Failure;
        }
        Vector3 agentPosition, locationPosition;
        float distance = GetDistanceToLocation(out agentPosition, out locationPosition);
        if (distance <= DistanceThreshold)
        {
            return Status.Success;
        }
        if (m_NavMeshAgent != null)
        {
            if (m_NavMeshAgent.IsNavigationComplete())
            {
                return Status.Success;
            }
            Vector3 targetDir = locationPosition - agentPosition;
            float lookAhead = targetDir.magnitude / m_NavMeshAgent.speed;
            m_NavMeshAgent.SetDestination(locationPosition + Target.Value.transform.forward * lookAhead);
        }
        else{
            float speed = Mathf.Min(Speed, distance);

            Vector3 toDestination = locationPosition - agentPosition;
            float lookAhead = toDestination.magnitude / speed;
            toDestination = locationPosition + Target.Value.transform.forward * lookAhead;
            toDestination.y = 0.0f;
            toDestination.Normalize();
            agentPosition += toDestination * (speed * Time.deltaTime);
            Agent.Value.transform.position = agentPosition;
            Agent.Value.transform.forward = toDestination;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private float GetDistanceToLocation(out Vector3 agentPosition, out Vector3 locationPosition)
    {
        agentPosition = Agent.Value.transform.position;
        locationPosition = Target.Value.transform.position;
        return Vector3.Distance(new Vector3(agentPosition.x, locationPosition.y, agentPosition.z), locationPosition);
    }
}

