using UnityEngine;
using System.Collections;

public abstract class AbstractBehavior : MonoBehaviour {

    public Buttons[] inputButtons;

    protected InputState controllableCharacter;
    protected CollisionState collisionState;
    protected Rigidbody2D body2d;


	protected virtual void Awake() {
        controllableCharacter = GetComponent<InputState>();
        collisionState = GetComponent<CollisionState>();
        body2d = GetComponent<Rigidbody2D>();
    }
}
