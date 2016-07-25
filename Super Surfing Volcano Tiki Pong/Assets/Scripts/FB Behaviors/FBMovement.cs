using UnityEngine;
using System.Collections;

public class FBMovement : MonoBehaviour {

    public float fbSpeed = 100.0f;

    [SerializeField]
    private float horiztonalDirection = -1.0f;
    [SerializeField]
    private float verticalDirection = 1.0f;

    private Rigidbody2D body2D;
    private float tan;
    private float angle;

	// Use this for initialization
	void Start () {

        body2D = GetComponent<Rigidbody2D>();
        verticalDirection = Random.Range(-1.0f, 1.0f);
        CalculateTan();

        angle = Mathf.Rad2Deg * Mathf.Atan(tan);

        if (angle > 45) {
            angle = 90 - angle;
        }
        else if (angle < -45) {
            angle = 90 + angle;
        }

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
	
	// Update is called once per frame
	void Update () {

        body2D.velocity = new Vector2(fbSpeed * horiztonalDirection, fbSpeed * verticalDirection);
    }

    void OnEnable() {
        FBCollisionEvent.onSGCollision += FBHitSG;
        FBCollisionEvent.onEnvironmentCollision += FBHitEnvironment;
        FBCollisionEvent.onDamagePlayerCollision += FBHitDamagePlayer;
        FBCollisionEvent.onTikiCollision += FBHitTiki;
    }

    void OnDisable() {
        FBCollisionEvent.onSGCollision -= FBHitSG;
        FBCollisionEvent.onEnvironmentCollision -= FBHitEnvironment;
        FBCollisionEvent.onDamagePlayerCollision -= FBHitDamagePlayer;
        FBCollisionEvent.onTikiCollision -= FBHitTiki;
    }

    private void FBHitSG(GameObject player) {

        StartCoroutine(BlockAnimationDuration(player));
        CalculateTan();
        horiztonalDirection *= -1.0f;
        ChangeFacingDirection();
        ChangeRotation();
    }

    private IEnumerator BlockAnimationDuration(GameObject player) {

        player.GetComponent<CollisionState>().BlockedFB = true;
        yield return new WaitForSeconds(player.GetComponent<PlayerAnimationManager>().blockAnimationDuration);
        player.GetComponent<CollisionState>().BlockedFB = false;
    }

    private void FBHitEnvironment(GameObject sky) {

        CalculateTan();
        verticalDirection *= -1.0f;
        ChangeRotation();
    }

    private void FBHitDamagePlayer(GameObject playerDamage) {

        CalculateTan();
        horiztonalDirection *= -1.0f;
        ChangeFacingDirection();
        ChangeRotation();

    }

    private void FBHitTiki(GameObject tiki) {

        verticalDirection = Random.Range(-1.0f, 1.0f);
        CalculateTan();
        horiztonalDirection *= -1.0f;
        ChangeFacingDirection();
        ChangeRotation();
    }

    private void CalculateTan() {

        if (verticalDirection > 0) {
            tan = verticalDirection / horiztonalDirection;
        }
        else if (verticalDirection < 0) {
            tan = horiztonalDirection / verticalDirection;
        }
    }

    private void ChangeFacingDirection() {
        transform.localScale = new Vector3(-horiztonalDirection, 1, 1);
    }

    private void ChangeRotation() {

        angle = -1 * Mathf.Rad2Deg * Mathf.Atan(tan);

        if (angle > 45) {
            angle = 90 - angle > 0 ? 90 - angle : angle;
        }
        else if (angle < -45) {
            angle = -1 * (90 + angle);
        }

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
