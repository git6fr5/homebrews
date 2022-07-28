// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class PlayButton : Button {

        // The DAW that's being worked in.
        [SerializeField] private DAW m_DAW;

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1.05f, !hover);
        }

        // On activation.
        protected override void Activate() {
            m_DAW.Play(!m_DAW.Playing);
        }

        // While active.
        protected override void Active(bool active, float dt) {
            Highlight(active);
        }

        // The condition to check if this is active.
        protected override bool ActiveCondition() {
            return m_DAW.Playing;
        }

    }
}