using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI
{
    public class Iconography : MonoBehaviour
    {
        private static Iconography _instance;

        private const float DIST_FROM_WORLD = 1024f;
        private static int _index;

        private static Camera Camera => _instance.RenderCamera;
        public const int RENDER_SIZE = 128;

        public Camera RenderCamera;

        private void Awake()
        {
            _instance = this;
            gameObject.SetActive(false);
        }

        private static Vector3 GetPosition ()
        {
            Quaternion rot = Quaternion.Euler(0f, 0f, ++_index);
            return rot * (Vector3.one * DIST_FROM_WORLD);
        }

        public static GameObject InstantiateModel(GameObject source)
        {
            return UnityUtils.InstantiateMockGO(source);
        }

        public static Sprite GenerateSprite (GameObject obj)
        {
            Texture2D tex = GenerateIcon(obj);
            Sprite sprite = Sprite.Create(tex, new Rect(0f, 0f, RENDER_SIZE, RENDER_SIZE), Vector2.one / 2f);
            return sprite;
        }

        public static Texture2D GenerateIcon(GameObject obj)
        {
            _instance.gameObject.SetActive(true);
            _instance.transform.position = GetPosition();

            Camera.enabled = true;
            Camera.aspect = 1f;

            GameObject model = InstantiateModel(obj);

            model.transform.position = _instance.transform.position;
            model.SetActive(true);

            RenderTexture renderTexture = RenderTexture.GetTemporary(RENDER_SIZE, RENDER_SIZE, 24);

            Bounds bounds = GetObjectBounds(model);

            float camSize = Mathf.Abs(bounds.extents.y);
            Camera.orthographicSize = camSize;

            RenderTexture.active = renderTexture;
            GL.Clear(true, true, Color.clear);

            Camera.targetTexture = renderTexture;
            Camera.transform.position = _instance.transform.position + bounds.center + Camera.transform.forward * -100f;
            Camera.transform.LookAt(_instance.transform);
            Camera.Render();

            Texture2D texture = new Texture2D(RENDER_SIZE, RENDER_SIZE, TextureFormat.ARGB32, false);
            texture.ReadPixels(new Rect(0f, 0f, RENDER_SIZE, RENDER_SIZE), 0, 0);
            texture.Apply();

            RenderTexture.active = null;

            RenderTexture.ReleaseTemporary(renderTexture);
            Camera.targetTexture = null;

            Camera.enabled = false;
            _instance.gameObject.SetActive(false);

            model.SetActive(false);
            DestroyImmediate(model);
            return texture;
        }

        public static Bounds GetObjectBounds(GameObject obj)
        {
            Vector3 prevPos = obj.transform.position;
            Quaternion prevRot = obj.transform.rotation;

            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;

            Bounds bounds = new Bounds();
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }

            obj.transform.position = prevPos;
            obj.transform.rotation = prevRot;

            return bounds;
        }
    }
}
