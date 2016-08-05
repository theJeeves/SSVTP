using UnityEngine;
using System.Collections;

public class VolcanoSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject volcanoSpawnPos;

    [SerializeField]
    private GameObject volcano;

    void Awake() {
        Instantiate(volcano, new Vector3(0, 82, 0), Quaternion.identity);
    }
}
