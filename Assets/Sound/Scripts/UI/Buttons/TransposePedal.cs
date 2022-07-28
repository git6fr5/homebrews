// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class TransposePedal : Button {

        [SerializeField] private DAW m_DAW;
        [SerializeField] private int m_Value;

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1.15f, !hover);
        }

        // On activation.
        protected override void Activate() {
            m_DAW.ShiftKey(m_Value);
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