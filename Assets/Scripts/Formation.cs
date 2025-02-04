
using UnityEngine;
using UnityEngine.AI;

public class Formation : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Vector3 pos;
    public Quaternion rot;

    private Vector3 initialPos;

    void Start()
    {
        pos = transform.position - target.transform.position;
        initialPos = pos;
    }

    void Update()
    {
        agent.destination = target.transform.TransformPoint(pos);
    }

    public void SetPosition(Vector3 position)
    {
        pos = position;
    }
    public void ResetPosition()
    {
        pos = initialPos;
    }
}
