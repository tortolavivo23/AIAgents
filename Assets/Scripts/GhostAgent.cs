using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class GhostAgent : Agent
{
    public float speed = 1.5f;

    public Transform target;
    public float maxX, maxZ, minX, minZ;

    private CharacterController controller;

    public override void Initialize()
    {
        controller = GetComponent<CharacterController>();
        target.transform.position = generateWanderPoint(5f, 0f);
    }

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(transform.position.x, 0.08f, transform.position.z);
        target.transform.position = generateWanderPoint(2f, 1f);
    }

    private Vector3 generateWanderPoint(float radius, float offset){
        Vector3 localTarget = UnityEngine.Random.insideUnitSphere;
        localTarget.y = 0f;
        localTarget.Normalize();
        localTarget *= radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
        if (worldTarget.x > maxX || worldTarget.x < minX || worldTarget.z > maxZ || worldTarget.z < minZ){
            worldTarget = transform.TransformPoint(-localTarget);
        }
        return worldTarget;
    }

    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];
        switch (action)
        {
            case 1:
                dirToGo = transform.forward;
                break;
            case 2:
                rotateDir = -transform.up;
                break;
            case 3:
                rotateDir = transform.up;
                break;
        }

        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        controller.Move(dirToGo * speed * Time.fixedDeltaTime);
       
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)

    {
        controller.Move(Vector3.down * 9.8f * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.transform.position) < 1.5f)
        {
            Debug.Log("Reached target");
            SetReward(1f);
            EndEpisode();
        }
        if(this.transform.position.y < 0){
            Debug.Log("Fell off the map");
            transform.position = new Vector3(0, 0.08f, 0);
        }
        SetReward(-1f / MaxStep);
        MoveAgent(actionBuffers.DiscreteActions);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector2 from = new Vector2(transform.position.x, transform.position.z);
        Vector2 to = new Vector2(target.position.x, target.position.z);
        sensor.AddObservation(from);
        sensor.AddObservation(to);
        sensor.AddObservation(transform.rotation.y);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        if(Input.GetKey(KeyCode.W)){
            discreteActions[0] = 1;
        }
        if(Input.GetKey(KeyCode.A)){
            discreteActions[0] = 2;
        }
        if(Input.GetKey(KeyCode.D)){
            discreteActions[0] = 3;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target.transform.position, 1.5f);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("wall"))
        {
            Debug.Log("Hit a wall");
            transform.position = new Vector3(0, 0.08f, 0);
            EndEpisode();
        }
    }
}
