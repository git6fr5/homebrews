/* --- Libraries --- */
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

        public void Held(bool held, float dt) {
            if (!held) {
                m_HoldTime = 0f;
            }
            else {
                m_HoldTime += dt;
            }
        }

        public float Frequency(Key key) {
            return Score.Frequency(m_Tone, key);
        }

        // public static Dictionary<KeyCode, Tone> MajorInstrument = new Dictionary<KeyCode, Tone>(){
        //     // { KeyCode.Alpha0, Tone.REST },
        //     { KeyCode.Alpha1, Tone.P1 },
        //     { KeyCode.Alpha2, Tone.M2 },
        //     { KeyCode.Alpha3, Tone.M3 },
        //     { KeyCode.Alpha4, Tone.P4 },
        //     { KeyCode.Alpha5, Tone.P5 },
        //     { KeyCode.Alpha6, Tone.M6 },
        //     { KeyCode.Alpha7, Tone.M7 },
        //     { KeyCode.Alpha8, Tone.P8 },
        //     { KeyCode.Alpha9, Tone.M9 },
        //     { KeyCode.Alpha0, Tone.M10 }
        // };

    }
}