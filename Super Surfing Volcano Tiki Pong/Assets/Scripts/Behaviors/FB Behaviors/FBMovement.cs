using UnityEngine;
using System.Collections;

public class FBMovement : MonoBehaviour {

    [SerializeField]
    private float defaultSpeed = 300.0f;

    [SerializeField]
    private float horiztonalDirection = -1.0f;
    [SerializeField]
    private float verticalDirection = 1.0f;

    private float _fbSpeed;
    private GameManager _gameManager;
    private Rigidbody2D _body2D;
    private float _tan;
    private float _angle;

    void Awake() {
        _gameManager = GameManager.Instance;
        _body2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        FBCollisionEvent.onSGCollision += BounceFromSG;
        FBCollisionEvent.onTikiCollision += FBHitTiki;
        FBCollisionEvent.onDamagePlayerCollision += FBHitLeftWall;
        FBCollisionEvent.onEnvironmentCollision += FBHitEnvironment;
    }

    void OnDisable() {
        FBCollisionEvent.onSGCollision -= BounceFromSG;
        FBCollisionEvent.onTikiCollision -= FBHitTiki;
        FBCollisionEvent.onDamagePlayerCollision -= FBHitLeftWall;
        FBCollisionEvent.onEnvironmentCollision -= FBHitEnvironment;
    }

    // Use this for initialization
    void Start () {
        _fbSpeed = defaultSpeed;
        verticalDirection = Random.Range(-1.0f, 1.0f);
        CalculateTan();

        _angle = Mathf.Rad2Deg * Mathf.Atan(_tan);

        if (_angle > 45) {
            _angle = 90 - _angle;
        }
        else if (_angle < -45) {
            _angle = 90 + _angle;
        }

        transform.localRotation = Quaternion.Euler(0, 0, _angle);
    }
	
	// Update is called once per frame
	void Update () {
        if (_gameManager.GameState == GameStates.Won ||
            _gameManager.GameState == GameStates.GameOver) {
            _body2D.velocity = Vector2.zero;
        }
        else {
            NewVelocity();
        }
    }

    private void NewVelocity() {
        _body2D.velocity = new Vector2(_fbSpeed * horiztonalDirection, _fbSpeed * verticalDirection);
    }

    public void BounceFromSG(GameObject ignore) {
        if (_gameManager.GameState == GameStates.Playing) {
            if (horiztonalDirection <= 0) {
                CalculateTan();
                horiztonalDirection *= -1.0f;
                ChangeFacingDirection();
                ChangeRotation();
                _gameManager.HitSurferGirl = false;
                NewVelocity();
            }
        }
    }

    private void FBHitTiki(GameObject ignore) {

        if (_gameManager.GameState == GameStates.Playing) {
            verticalDirection = Random.Range(-1.0f, 1.0f);
            CalculateTan();
            horiztonalDirection *= -1.0f;
            ChangeFacingDirection();
            ChangeRotation();
            _gameManager.HitTiki = false;
            NewVelocity();
        }
    }

    private void FBHitEnvironment(GameObject ignore) {

        CalculateTan();
        verticalDirection *= -1.0f;
        ChangeRotation();
    }

    private void FBHitLeftWall(GameObject ignore) {
        _fbSpeed = 0.0f;
        NewVelocity();
    }

    private void CalculateTan() {

        if (verticalDirection > 0) {
            _tan = verticalDirection / horiztonalDirection;
        }
        else if (verticalDirection < 0) {
            _tan = horiztonalDirection / verticalDirection;
        }
    }

    private void ChangeFacingDirection() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
    }

    private void ChangeRotation() {

        _angle = -1 * Mathf.Rad2Deg * Mathf.Atan(_tan);

        if (_angle > 45) {
            _angle = 90 - _angle > 0 ? 90 - _angle : _angle;
        }
        else if (_angle < -45) {
            _angle = -1 * (90 + _angle);
        }

        transform.localRotation = Quaternion.Euler(0, 0, _angle);
    }
}
