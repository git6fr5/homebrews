// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class MinusButton : Button {

        // The DAW that's being worked in.
        [SerializeField] private Channel m_Channel;

        // On hover.
        protected override void Hover(bool hover, float dt) {
            
        }

        // On activation.
        protected override void Activate() {
            if (m_Channel.Editing) {
                if (!IsNoteMarker(m_Channel.Timeline.ActiveMarker)) {
                    // m_Channel.AddNote(m_Channel.Timeline.ActiveMarker);
                }
                else {
                    m_Channel.Timeline.ActiveMarker.GetComponent<NoteMarker>().Delete();
                    m_Channel.ChannelClef.Clean();
                    m_Channel.Timeline.DrawClef(m_Channel.ChannelClef);
                }
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

        public bool IsNoteMarker(Marker marker) {
            return marker.GetComponent<NoteMarker>() != null;
        }

    }

}