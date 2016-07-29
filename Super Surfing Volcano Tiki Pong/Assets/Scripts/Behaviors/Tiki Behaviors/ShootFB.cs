using UnityEngine;
using System.Collections;

public class ShootFB : MonoBehaviour {

    [SerializeField]
    private GameObject Fireball;
    [SerializeField]
    private GameObject FBSpawnPos;
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.GameState == 0) {
            if (GameManager.Instance.Reset) {
                Instantiate(Fireball, FBSpawnPos.transform.position, Quaternion.identity);
                GameManager.Instance.Reset = false;
            }
        }
	}
}
