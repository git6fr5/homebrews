// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class OctavePedal : Button {

        [SerializeField] private Wave m_Wave;
        [SerializeField] private int m_Value;

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1.15f, !hover);
        }

        // On activation.
        protected override void Activate() {
            m_Wave.ShiftOctave(m_Value);
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