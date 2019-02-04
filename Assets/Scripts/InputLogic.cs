using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCrane;
using UnityEngine.EventSystems;

namespace TCrane
{
    public class InputLogic : MonoBehaviour
    {
        //config
        public RectTransform bttn, bttnCenter, spawnPoint;

        //state
        public enum States { Idle, Pressing, Draging };
        public States state = States.Idle;
        public bool pressed;

        //bookkeeping
        public Vector3 dir, touchPos, center;
        public static InputLogic self;

        public delegate void UpDelegate();
        public static event UpDelegate OnPointerUpEvent = () => { };

        void OnEnable()
        {
            self = this;
        }

        void Update()
        {
            switch (state)
            {
                case States.Idle:
                    bttn.position = spawnPoint.position;
                    bttnCenter.anchoredPosition = Vector2.zero;
                    if (pressed) state = States.Pressing;
                    break;
                case States.Pressing:
                    UpdateTouchPos();
                    center = touchPos;
                    bttn.anchoredPosition = touchPos;
                    state = States.Draging;
                    break;
                case States.Draging:
                    UpdateTouchPos();
                    dir = (touchPos - center);
                    bttnCenter.anchoredPosition = dir.normalized * bttn.sizeDelta.x / 3;
                    if (!pressed) state = States.Idle;
                    break;
            }
        }

        void UpdateTouchPos()
        {
            
            #if UNITY_EDITOR || UNITY_STANDALONE_WIN
                touchPos = Input.mousePosition;
            #else
                touchPos = Input.GetTouch(0).position;
            #endif

            // needs scaling to 1280x720, because canvas scaleMode = "scaleWithScreenSize 1280x720"
            float ratioX = (float)Screen.width / (float)1280;
            float ratioY = (float)Screen.height / (float)720;
            touchPos.x /= ratioX;
            touchPos.y /= ratioY;
        }

        // Events of button "UI/PlayCanvas/ClickArea"
        public void OnPointerDown() { pressed = true; }
        public void OnPointerUp() { OnPointerUpEvent(); pressed = false; }

    }
}
