/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class SynthIOButton : Button {

        [SerializeField] private Synth m_Synth;
        [SerializeField] private bool m_Save;
        [SerializeField] private bool m_Load;

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1.05f, !hover);
        }

        // On activation
        protected override void Activate() {
            if (m_Save) {
                m_Synth.Save(m_Synth.Filename);
            }
            if (m_Load) {
                m_Synth.Open(m_Synth.Filename);
            }
        }

        // While active.
        protected override void Active(bool active, float dt) {
            Highlight(active);
        }

        // The condition to check if this is active.
        protected override bool ActiveCondition() {
            return false;
        }

    }
}