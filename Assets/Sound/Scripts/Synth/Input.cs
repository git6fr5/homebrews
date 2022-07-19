/* --- Libraries --- */
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    [System.Serializable]
    public class Input {

        [SerializeField] private KeyCode m_KeyCode;
        [SerializeField] private Score.Tone m_Tone;
        [SerializeField, ReadOnly] private bool m_Pressed;
        public bool Pressed => m_Pressed;
        [SerializeField, ReadOnly] private int m_HoldTime = 0;
        public int HoldTime => m_HoldTime;

        public void OnUpdate() {
            bool down = UnityEngine.Input.GetKeyDown(m_KeyCode);
            bool up = UnityEngine.Input.GetKeyUp(m_KeyCode);
            m_Pressed = down ? true : up ? false : m_Pressed;
        }

        public void Held(int dt, bool held = true) {
            if (!held) {
                m_HoldTime = 0;
            }
            else {
                m_HoldTime += dt;
            }
        }

        public float Frequency(Score.Key key) {
            return Score.Frequency(m_Tone, key);
        }

    }
}