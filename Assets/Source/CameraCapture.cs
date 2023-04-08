using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    // Stolen from https://forum.unity.com/threads/how-to-save-manually-save-a-png-of-a-camera-view.506269/ because I'm lazy.
    public class CameraCapture : MonoBehaviour
    {
        private const string ORTHO_CAMERA_PATH = "Prefabs/OrthoCaptureCamera";

        public Camera Camera;

        public Texture2D Capture(Vector2Int size)
        {
            Camera.enabled = true;
            RenderTexture activeRenderTexture = RenderTexture.active;
            RenderTexture renderTexture = RenderTexture.GetTemporary(size.x, size.y, 24);
            Camera.targetTexture = renderTexture;

            RenderTexture.active = Camera.targetTexture;

            Camera.aspect = size.x / size.y;
            Camera.Render();

            Texture2D image = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
            image.Apply();
            RenderTexture.active = activeRenderTexture;
            RenderTexture.ReleaseTemporary(renderTexture);

            Camera.targetTexture = null;
            Camera.enabled = false;

            return image;
        }

        public static Texture2D CaptureOrtho (Rect rect, Vector2Int size)
        {
            GameObject newCamera = Instantiate(Resources.Load<GameObject>(ORTHO_CAMERA_PATH));
            CameraCapture capture = newCamera.GetComponent<CameraCapture>();
            capture.Camera.orthographicSize = Mathf.Max(rect.width, rect.height) / 2f;
            newCamera.transform.position = (Vector3)rect.center + Vector3.back * 10;
            Texture2D tex = capture.Capture(size);
            Destroy(newCamera);
            return tex;
        }
    }
}
