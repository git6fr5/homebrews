/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Monet;
using Monet.UI;

namespace Monet.UI {

    ///<summary>
    ///
    ///<summary>
    public class NoteMarker : Marker {

        [SerializeField] private Note m_CachedNote;
        public Note CachedNote => m_CachedNote;
        [SerializeField, ReadOnly] private float m_CachedWorkspaceHeight;
        [SerializeField, ReadOnly] private float m_CachedBeatSpacing;
        public float CachedBeatSpacing => m_CachedBeatSpacing;
        [SerializeField, ReadOnly] private float m_CachedSubdivisions;

        [SerializeField] private SpriteShapeController m_TailSprite;
        [SerializeField] private BoxCollider2D m_TailCollider;

        [SerializeField] private Draggable[] m_Draggables;

        public void SetTone(int tone, float workspaceHeight) {
            Vector3 position = transform.localPosition;
            float ratio = (float)tone / (float)(int)Score.Tone.Count;
            position.y = workspaceHeight * (ratio - 1f);
            transform.localPosition = position; 
        }

        public void SetDuration(int duration, float scale, float subdivisions) {
            DurationDraggable.Spline(m_TailSprite, m_TailCollider, duration * scale / subdivisions, 0.5f);
        }

        public NoteMarker DuplicateNote() {
            return Duplicate().GetComponent<NoteMarker>();
        }

        public void Delete() {
            m_CachedNote.Tone = Score.Tone.Rest;
        }

        public void Cache(Note note, float workspaceHeight, float beatSpacing, float subdivisions) {
            m_CachedNote = note;
            m_CachedWorkspaceHeight = workspaceHeight;
            m_CachedBeatSpacing = beatSpacing;
            m_CachedSubdivisions = subdivisions;
        }

        public void Ascend() {
            m_CachedNote.Ascend();
            SetTone((int)m_CachedNote.Tone, m_CachedWorkspaceHeight);
        }

        public void Descend() {
            m_CachedNote.Descend();
            SetTone((int)m_CachedNote.Tone, m_CachedWorkspaceHeight);
        }

        public void Longer() {
            m_CachedNote.Longer();
            SetDuration(m_CachedNote.Duration, m_CachedBeatSpacing, m_CachedSubdivisions);
        }

        public void Shorter() {
            m_CachedNote.Shorter();
            SetDuration(m_CachedNote.Duration, m_CachedBeatSpacing, m_CachedSubdivisions);
        }

        public void Move(float movement, Channel channel) {
            int index = channel.FindBar(m_CachedTime);
            float subdivisions = channel.ChannelClef.Bars[index].Subdivision;
            float endtime = channel.ChannelClef.GetEndTime(index) - channel.ChannelClef.GetStartTime(index) ;
            bool moved = m_CachedNote.Move(movement / subdivisions, endtime);
            if (moved) {
                SetTime(channel.ChannelClef.GetStartTime(index) + m_CachedNote.TimeID, m_CachedBeatSpacing);
            }
        }

        public override void SetEditing(bool editing) {
            for (int i = 0; i < m_Draggables.Length; i++) {
                m_Draggables[i].enabled = editing;
            }
            base.SetEditing(editing);
        }

    }

}