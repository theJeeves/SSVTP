using UnityEngine;
using System.Collections;

public class FBCollisionEvent : MonoBehaviour {

    public delegate void FBCollisionAction();

    public static event FBCollisionAction onEnvironmentCollision;
    public static event FBCollisionAction onDamagePlayerCollision;
    public static event FBCollisionAction onTikiCollision;
    public static event FBCollisionAction onSGCollision;

    void OnCollisionEnter2D(Collision2D collider) {

        string collisionObject = collider.gameObject.tag;

        if (collisionObject == "Player") {
            if (onSGCollision != null) {
                onSGCollision();
            }
        }
        else if (collisionObject == "Sky" || collisionObject == "Ocean") {
            if (onEnvironmentCollision != null) {
                onEnvironmentCollision();
            }
        }
        else if (collisionObject == "DamagePlayer") {
            if (onDamagePlayerCollision != null) {
                onDamagePlayerCollision();
            }
        }
        else if (collisionObject == "Tiki") {
            if (onTikiCollision != null) {
                onTikiCollision();
            }
        }
    }
}
