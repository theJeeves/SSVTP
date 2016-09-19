using UnityEngine;
using System.Collections;

public class TitleAnimation : MonoBehaviour {

    [SerializeField]
    private float _animationSpeed = 0.0f;
    [SerializeField]
    private float _animationDistance = 0.0f;

    private Vector3 _startPosition = new Vector3(0, -11.0f, 0);

    void Awake() {
        _startPosition = transform.position;
    }
	
	void OnEnable() {
        transform.position = _startPosition;
        StartCoroutine(Animation());
    }

    private IEnumerator Animation() {
        while (true) {
            yield return new WaitForSeconds(_animationSpeed);

            transform.position = new Vector3(transform.position.x - _animationDistance, 
                transform.position.y - _animationDistance, 0);

            yield return new WaitForSeconds(_animationSpeed);

            transform.position = new Vector3(transform.position.x + _animationDistance, 
                transform.position.y + _animationDistance, 0);
        }
    }
}
