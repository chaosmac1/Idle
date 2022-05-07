using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

#nullable enable
namespace Idle {
    public class Player : MonoBehaviour {
        public GameObject? GameObjectCamera;
        public float Speed = 4;
        public float ScrollSpeed = 3;
        public float SpeedDeep = .45f;
        public float MaxSpeed = 11;

        public string KeyUp = "w";
        public string KeyLeft = "a";
        public string KeyDown = "s";
        public string KeyRight = "d";
        public string ScrollOut = "Space";
        public string ScrollIn = "LeftShift";
        
        private Camera? Camera;
        void Start() {
            if (Speed == 0) Speed = 5;
            if (ScrollSpeed == 0) ScrollSpeed = 3;
            if (SpeedDeep == 0) SpeedDeep = 1;
            if (MaxSpeed == 0) MaxSpeed = 10;
            
            if (GameObjectCamera is null)
                throw new NullReferenceException(nameof(GameObjectCamera));

            Camera = GameObjectCamera.GetComponent<Camera>();
            
            if (Camera is null)
                throw new NullReferenceException(nameof(Camera));
        }

        void Update() {
            if (GameObjectCamera is null)
                throw new NullReferenceException(nameof(GameObjectCamera));
            
            if (Camera is null)
                throw new NullReferenceException(nameof(Camera));
            
            UpdateLocalPosition(this.GameObjectCamera, Camera);
            UpdateCammeraSize(this.Camera);
        }

        private void UpdateLocalPosition(GameObject gameObjectCamera, Camera camera) {
            var localPosition = gameObjectCamera.transform.localPosition;

            float speedDeepFix = Speed * (1f / math.pow(1f / (camera.orthographicSize * SpeedDeep), 1.3f));
            speedDeepFix = speedDeepFix > MaxSpeed ? MaxSpeed : speedDeepFix;
            
            // X -Left +Right
            // Y +Up -Down
            if (Input.GetKey(this.KeyUp))
                localPosition.y += (speedDeepFix * Time.deltaTime);
            if (Input.GetKey(this.KeyLeft))
                localPosition.x -= (speedDeepFix * Time.deltaTime);
            if (Input.GetKey(this.KeyDown))
                localPosition.y -= (speedDeepFix * Time.deltaTime);
            if (Input.GetKey(this.KeyRight))
                localPosition.x += (speedDeepFix * Time.deltaTime);

            gameObjectCamera.transform.localPosition = localPosition;
        }

        private void UpdateCammeraSize(Camera camera) {
            // LeftShift +
            // Space -

            if (Input.GetKey(Enum.Parse<KeyCode>(this.ScrollOut))) {
                camera.orthographicSize += (this.ScrollSpeed * Time.deltaTime);
            }

            
            if (Input.GetKey(Enum.Parse<KeyCode>(this.ScrollIn))) {
                camera.orthographicSize -= (this.ScrollSpeed * Time.deltaTime);
            }

            if (camera.orthographicSize < 1)
                camera.orthographicSize = 1;
        }
    }
}
