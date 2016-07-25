using UnityEngine;
using System.Collections;

public class FBCollisionEvent : MonoBehaviour {

    public delegate void FBCollisionAction(GameObject collisionObject);

    public static event FBCollisionAction onEnvironmentCollision;
    public static event FBCollisionAction onDamagePlayerCollision;
    public static event FBCollisionAction onTikiCollision;
    public static event FBCollisionAction onSGCollision;

    void OnCollisionEnter2D(Collision2D collider) {

        string collisionObject = collider.gameObject.tag;

        if (collisionObject == "Player") {
            if (onSGCollision != null) {
                onSGCollision(collider.gameObject);
            }
        }
        else if (collisionObject == "Sky" || collisionObject == "Ocean") {
            if (onEnvironmentCollision != null) {
                onEnvironmentCollision(collider.gameObject);
            }
        }
        else if (collisionObject == "DamagePlayer") {
            if (onDamagePlayerCollision != null) {
                onDamagePlayerCollision(collider.gameObject);
            }
        }
        else if (collisionObject == "Tiki") {
            if (onTikiCollision != null) {
                onTikiCollision(collider.gameObject);
            }
        }
    }
}
