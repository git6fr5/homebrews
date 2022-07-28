// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class EditChannelButton : Button {

        #region Variables

        // The DAW that's being worked in.
        [SerializeField] private DAW m_DAW;
        // The channel that this edit button relates to.
        [SerializeField] private Channel m_Channel;

        // The editing windows that this opens.
        [SerializeField] private GameObject[] m_EditingWindows;
        [SerializeField, ReadOnly] private Vector3 m_EditingWindowOrigin;

        // The duration that this takes to open.
        [SerializeField] private float m_Duration;
        [SerializeField, ReadOnly] private float m_Ticks;

        #endregion

        // Initialize the button.
        void Start() {
            m_EditingWindowOrigin = transform.parent.position;
            for (int i = 0; i < m_EditingWindows.Length; i++) {
                m_EditingWindows[i].transform.SetParent(null);
            }
            m_Channel.SetEditing(false);
        }

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1.05f, !hover);
        }

        // On activation.
        protected override void Activate() {
            // Itterate through the channels.
            for (int i = 0; i < m_DAW.Channels.Count; i++) {
                // Toggle the selected channel.
                if (m_DAW.Channels[i] == m_Channel) {
                    m_DAW.Channels[i].SetEditing(!m_DAW.Channels[i].Editing);
                }
                // Deselect all the others.
                else {
                    m_DAW.Channels[i].SetEditing(false);
                }
            }
        }

        // While active.
        protected override void Active(bool active, float dt) {
            Highlight(active);
            Rotate(45f, dt, !active);
            Tick(ref m_Ticks, m_Duration, dt, active, false);
            MoveWindows(m_EditingWindows, m_EditingWindowOrigin, m_Ticks / m_Duration, 19f/16f);
        }

        // The condition to check if this is active.
        protected override bool ActiveCondition() {
            return m_Channel.Editing;
        }

        private static void MoveWindows(GameObject[] windows, Vector3 origin, float ratio, float distance) {
            for (int i = 0; i < windows.Length; i++) {
                windows[i].SetActive(ratio > 0f);
                windows[i].transform.position = origin + (i + 1) * ratio * distance * Vector3.down;
            }
        }

    }
}