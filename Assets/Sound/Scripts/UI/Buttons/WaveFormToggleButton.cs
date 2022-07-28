// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class WaveFormToggleButton : Button {

        // The display that this sets the form for.
        [SerializeField] private Display m_Display;
        // The form this sets the display to.
        [SerializeField] private bool m_Macro;

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1f, !hover);
        }

        // On activation.
        protected override void Activate() {
            m_Display.SetMode(m_Macro);
        }

        // While active.
        protected override void Active(bool active, float dt) {
            Highlight(active);
        }

        // The condition to check if this is active.
        protected override bool ActiveCondition() {
            return m_Display.Macro == m_Macro;
        }

    }

}

