using UnityEngine;
using System.Collections;

public class Flock : MonoBehaviour {
    public FlockManager myManager;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 direction;
    public GameObject leader;
    private BeeState state;
    private Quaternion prevRotation;

    void Start() {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        direction = transform.forward * speed;
        state = BeeState.Idle;
        StartCoroutine("Flocking");
    }

    void Update() {
        switch (state) {
            case BeeState.Idle:
                Idle();
                break;
            case BeeState.Pursue:
                Pursuing();
                break;
        }
    }

    void Idle() {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(direction),
                                              myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
        if(Vector3.Distance(leader.transform.position, myManager.transform.position) > 1.5) {
            prevRotation = transform.rotation;
            state = BeeState.Pursue;
        }
    }

    void Pursuing() {
        Vector3 target = leader.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, leader.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.normalized), Time.deltaTime * 5f);
        if(Vector3.Distance(leader.transform.position, myManager.transform.position) < 1.5 && Vector3.Distance(transform.position, myManager.transform.position) < 1.5) {
            transform.rotation = prevRotation;
            state = BeeState.Idle;
        }
    }

    IEnumerator Flocking()
    {
        while (true)
        {
            float waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds (waitTime);

            FlockingRules();
        }
    }

    void FlockingRules() {
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 align = Vector3.zero;
        int num = 0;

        foreach (GameObject go in myManager.allFlocks) {
            if (go != gameObject) {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance) {
                    cohesion += go.transform.position;
                    align += go.GetComponent<Flock>().direction;
                    num++;
                    separation -= (transform.position - go.transform.position) / (distance * distance);
                }
            }
        }

        if (num > 0) {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
            cohesion = (cohesion / num - transform.position).normalized * speed;

            direction = (cohesion + align + separation).normalized * speed;
        }
    }
}
