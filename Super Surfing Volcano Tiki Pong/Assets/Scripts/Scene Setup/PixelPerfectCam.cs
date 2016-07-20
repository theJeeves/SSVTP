using UnityEngine;
using System.Collections;

public class PixelPerfectCam : MonoBehaviour {

    public static float pixelsToUnits = 1.0f;
    public static float scale = 1.0f;
    public Vector2 nativeResolution = new Vector2(1280, 720);

	void Awake()
    {
        Camera camera = GetComponent<Camera>();

        if (camera.orthographic)
        {
            scale = Screen.height / nativeResolution.y;
            pixelsToUnits *= scale;
            camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
        }
    }
}
