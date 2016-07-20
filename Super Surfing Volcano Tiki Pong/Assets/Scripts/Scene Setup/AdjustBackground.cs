using UnityEngine;
using System.Collections;

public class AdjustBackground : MonoBehaviour {

    [SerializeField]
    private Vector2 bgSize = new Vector2(480, 360);
    [SerializeField]
    private bool scaleW = true;
    [SerializeField]
    private bool scaleH = true;

	// Use this for initialization
	void Start ()
    {
        float widthAdjustment = !scaleW ? 1 : Screen.width / (bgSize.x * PixelPerfectCam.scale);
        float heightAdjustment = !scaleH ? 1 : Screen.height / (bgSize.y * PixelPerfectCam.scale);

        transform.localScale = new Vector3(Mathf.CeilToInt(bgSize.x * widthAdjustment),
            Mathf.CeilToInt(bgSize.y * heightAdjustment), 1);
	}
}
