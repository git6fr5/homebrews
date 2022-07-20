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

        public static List<Input> DiatonicMajor = new List<Input> {
            new Input(KeyCode.Alpha1, Score.Tone.P1),
            new Input(KeyCode.Alpha2, Score.Tone.Maj2),
            new Input(KeyCode.Alpha3, Score.Tone.Maj3),
            new Input(KeyCode.Alpha4, Score.Tone.P4),
            new Input(KeyCode.Alpha5, Score.Tone.P5),
            new Input(KeyCode.Alpha6, Score.Tone.Maj6),
            new Input(KeyCode.Alpha7, Score.Tone.Maj7),
            new Input(KeyCode.Alpha8, Score.Tone.P8),
            new Input(KeyCode.Alpha9, Score.Tone.Maj9),
            new Input(KeyCode.Alpha0, Score.Tone.Maj10),
        };

        [SerializeField] private UnityEngine.KeyCode m_KeyCode;
        [SerializeField] private Score.Tone m_Tone;
        [SerializeField, ReadOnly] private bool m_Pressed;
        public bool Pressed => m_Pressed;
        [SerializeField, ReadOnly] private int m_HoldTime = 0;
        public int HoldTime => m_HoldTime;

        public Input(UnityEngine.KeyCode keyCode, Score.Tone tone) {
            m_KeyCode = keyCode;
            m_Tone = tone;
        }

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