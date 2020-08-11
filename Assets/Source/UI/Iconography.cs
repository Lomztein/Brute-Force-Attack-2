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
        private static Camera Camera => _instance.RenderCamera;
        public const int RENDER_SIZE = 128;

        public Camera RenderCamera;

        private void Awake()
        {
            _instance = this;
            gameObject.SetActive(false);
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

            Camera.enabled = true;
            Camera.aspect = 1f;

            GameObject model = InstantiateModel(obj);
            model.transform.position = _instance.transform.position;
            model.SetActive(true);

            RenderTexture renderTexture = new RenderTexture(RENDER_SIZE, RENDER_SIZE, 24);
            renderTexture.Create();

            Bounds bounds = GetObjectBounds(model);

            float camSize = Mathf.Abs(bounds.extents.y);
            Camera.orthographicSize = camSize;

            Camera.targetTexture = renderTexture;
            Camera.transform.position = _instance.transform.position + bounds.center + Camera.transform.forward * -2f;
            Camera.transform.LookAt(_instance.transform);
            Camera.Render();

            RenderTexture.active = renderTexture;

            Texture2D texture = new Texture2D(RENDER_SIZE, RENDER_SIZE, TextureFormat.ARGB32, false);
            texture.ReadPixels(new Rect(0f, 0f, RENDER_SIZE, RENDER_SIZE), 0, 0);
            texture.Apply();

            Camera.targetTexture = null;
            RenderTexture.active = null;

            Destroy(renderTexture);

            Camera.enabled = false;
            _instance.gameObject.SetActive(false);

            model.SetActive(false);
            Destroy(model);
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
