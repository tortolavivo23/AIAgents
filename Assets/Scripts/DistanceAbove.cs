using System;
using UnityEngine;
using Unity.Muse.Behavior;
using Action = Unity.Muse.Behavior.Action;

[Serializable]
[NodeDescription(name: "distanceAbove", story: "Is the distance between [Agent] and [Target] above [Threshold]", category: "Action/Conditional", id: "20efc9576981b23a5be7270ecc7f4bab")]
public class distanceAbove: Action
{
    public BlackboardVariable<GameObject> Agent;
    public BlackboardVariable<GameObject> Target;
    public BlackboardVariable<float> Threshold;
    public BlackboardVariable<bool> StrictCheck = new BlackboardVariable<bool>(false);

    protected override Status OnStart()
    {
        if (Agent?.Value == null || Target?.Value == null)
        {
            return Status.Failure;
        }

        return CheckIsValid(Vector3.Distance(
            Agent.Value.transform.position,
            Target.Value.transform.position
        )) ? Status.Success : Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent?.Value == null || Target?.Value == null)
        {
            return Status.Failure;
        }

        return CheckIsValid(Vector3.Distance(
            Agent.Value.transform.position,
            Target.Value.transform.position
        )) ? Status.Success : Status.Failure;
    }

    private bool CheckIsValid(float a)
    {
        float b = Threshold.Value;
        if (StrictCheck.Value) return a > b;
        return a >= b;
    }
}

