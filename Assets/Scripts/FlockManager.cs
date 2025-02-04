using UnityEngine;
public class FlockManager : MonoBehaviour {
    public GameObject flockPrefab;
    public int numFlocks = 20;
    public float limit = 5f;
    public GameObject[] allFlocks;

    public GameObject leader;

    [Header("Flocking Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    void Start() {
        allFlocks = new GameObject[numFlocks];
        for (int i = 0; i < numFlocks; ++i) {
            Vector3 pos = transform.position + new Vector3(Random.Range(-limit, limit),
                                                                Random.Range(-limit, limit),
                                                                Random.Range(-limit, limit));
            Vector3 randomize = new Vector3 (Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
            allFlocks[i] = Instantiate(flockPrefab, pos, Quaternion.LookRotation(randomize));
            allFlocks[i].GetComponent<Flock>().myManager = this;
            allFlocks[i].GetComponent<Flock>().leader = leader;
        }
    }
}
