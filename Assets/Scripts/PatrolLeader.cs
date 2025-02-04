using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolLeader : MonoBehaviour {

	public List<GameObject> waypoints;

	public float close = 1;

	public Formation[] Followers = new Formation[4];
	private int currentWaypointIndex = 0;
	
	private Vector3 currentWaypoint;

	private NavMeshAgent agent;

	private bool patrol;



	void Start() {
		agent = GetComponent<NavMeshAgent> ();
		patrol = true;
		currentWaypoint = waypoints[currentWaypointIndex].transform.position;
		agent.destination = currentWaypoint;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			patrol = false;
			RaycastHit hit;
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (camRay, out hit, 100)) {
				currentWaypoint = hit.point;
				agent.destination = hit.point;
			}
		}
		else if(IsCloseToCurrentWaypoint()) {
			if (patrol) currentWaypointIndex++;
			patrol = true;
			if (currentWaypointIndex >= waypoints.Count) {
				currentWaypointIndex = 0;
			}
			currentWaypoint = waypoints[currentWaypointIndex].transform.position;
			agent.destination = currentWaypoint;
		}

		if(Input.GetKeyDown(KeyCode.V)){
			Followers[0].SetPosition(new Vector3(-0.8f, 0, -1.516f));
			Followers[1].SetPosition(new Vector3(1.03f, 0, -1.516f));
			Followers[2].SetPosition(new Vector3(-1.18f, 0, -3.256f));
			Followers[3].SetPosition(new Vector3(1.66f, 0, -3.256f));
		}
		else if(Input.GetKeyDown(KeyCode.Z)){
			Followers[0].SetPosition(new Vector3(0, 0, 0));
			Followers[1].SetPosition(new Vector3(0, 0, 0));
			Followers[2].SetPosition(new Vector3(0, 0, 0));
			Followers[3].SetPosition(new Vector3(0, 0, 0));
		}
		else if(Input.GetKeyDown(KeyCode.I)){
			Followers[0].SetPosition(new Vector3(-0.8f, 0, -1.516f));
			Followers[1].SetPosition(new Vector3(1.03f, 0, -1.516f));
			Followers[2].SetPosition(new Vector3(0, 0, -4.3f));
			Followers[3].SetPosition(new Vector3(0, 0, -2.86f));
		}
		else if(Input.GetKeyDown(KeyCode.X)){
			foreach (Formation follower in Followers) {
				follower.ResetPosition();
			}
		}
	}

	private bool IsCloseToCurrentWaypoint() {
		return Vector3.Distance (transform.position, currentWaypoint) < close;
	}
}
