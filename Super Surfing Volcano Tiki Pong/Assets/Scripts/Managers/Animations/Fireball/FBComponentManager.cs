using UnityEngine;
using System.Collections;

public class FBComponentManager : MonoBehaviour {


    private Collider2D _collider2d;

	// Use this for initialization
	void Awake () {
        _collider2d = GetComponent<Collider2D>();
	}

    void OnEnable() {
        FBCollisionEvent.onDamagePlayerCollision += DisableCollider;
    }

    void OnDisable() {
        FBCollisionEvent.onDamagePlayerCollision -= DisableCollider;
    }

    private void DisableCollider() {
        _collider2d.enabled = false;
    }
}
