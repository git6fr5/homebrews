// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class MarkerButton : Button {

        // The DAW that's being worked in.
        [SerializeField] private Channel m_Channel;
        private Marker m_Marker => GetComponent<Marker>();

        // On hover.
        protected override void Hover(bool hover, float dt) {
            
        }

        // On activation.
        protected override void Activate() {
            if (m_Channel.Editing) {
                m_Channel.Timeline.SetActiveMarker(m_Marker);
            }
        }

        // While active.
        protected override void Active(bool active, float dt) {
            Highlight(active);
        }

        // The condition to check if this is active.
        protected override bool ActiveCondition() {
            return m_Channel.Timeline.ActiveMarker == m_Marker;
        }

    }

}