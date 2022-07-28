// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class WaveShapeButton : Button {

        // The wave that this sets the shape for.
        [SerializeField] private Wave m_Wave;
        // The shape this sets the wave to.
        [SerializeField] private WaveShape m_Shape;

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1.05f, !hover);
        }
            
        // On activation.
        protected override void Activate() {
            m_Wave.SetShape(m_Shape);
        }

        // While active.
        protected override void Active(bool active, float dt) {
            Highlight(active);
        }

        // The condition to check if this is active.
        protected override bool ActiveCondition() {
            return m_Wave.Shape == m_Shape;
        }

    }

}

