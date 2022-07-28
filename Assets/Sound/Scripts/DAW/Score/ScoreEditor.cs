/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class ScoreEditor : MonoBehaviour {

        // public GameObject noteNode;
        // public Transform trebleNodesOrigin;
        // public Transform bassNodesOrigin;
        // public NoteNode[] trebleNodes;
        // public NoteNode[] bassNodes;

        // public float scale;

        // [Range(0.1f, 1f)] public float updateRate = 0.25f;

        // public bool isEditing;

        // public float a;
        // public float b;

        // public void Instantiate() {
        //     if (!isEditing) { return; }

        //     float subdivision = LengthMultipliers[NoteLength.EIGTH];
        //     trebleNodes = new NoteNode[(int)(m_Bars * 4f / subdivision)];
        //     bassNodes = new NoteNode[(int)(m_Bars * 4f / subdivision)];

        //     CreateNodes(trebleNodes, (Vector2)trebleNodesOrigin.position, subdivision);
        //     CreateNodes(bassNodes, (Vector2)bassNodesOrigin.position, subdivision);

        //     SetNodesFromScore(treble, trebleNodes);
        //     SetNodesFromScore(bass, bassNodes);

        //     StartCoroutine(IEUpdateScore(updateRate, treble, trebleNodes));
        //     StartCoroutine(IEUpdateScore(updateRate, bass, bassNodes));
        // }

        // void CreateNodes(NoteNode[] nodes, Vector2 origin, float subdivision) {
        //     for (int i = 0; i < m_Bars * 4f / subdivision; i++) {
        //         NoteNode newNoteNode = Instantiate(noteNode, new Vector3(i * scale + origin.x, origin.y, 0f), Quaternion.identity, transform).GetComponent<NoteNode>();
        //         newNoteNode.gameObject.SetActive(true);
        //         nodes[i] = newNoteNode;
        //     }
        // }

        // IEnumerator IEUpdateScore(float delay, Clef clef, NoteNode[] nodes) {
        //     yield return new WaitForSeconds(delay);

        //     float subdivision = LengthMultipliers[NoteLength.EIGTH];
        //     clef.tones = new List<Tone>();
        //     clef.lengths = new List<NoteLength>();

        //     int j = 0;
        //     for (int i = 0; i < m_Bars * 4f / subdivision; i++) {
        //         // Tone tone = noteNodes[i].isActive ? Tone.P1 : Tone.REST;
        //         if (j == 0) {
        //             nodes[i].gameObject.SetActive(true);
        //             clef.tones.Add(nodes[i].tone);
        //             clef.lengths.Add(nodes[i].length);
        //             j = nodes[i].skipCount;
        //         }
        //         else {
        //             nodes[i].gameObject.SetActive(false);
        //             j--;
        //         }
        //     }

        //     for (int i = 0; i < nodes.Length; i++) {
        //         nodes[i].a = this.a;
        //         nodes[i].b = this.b;
        //     }

        //     StartCoroutine(IEUpdateScore(updateRate, clef, nodes));
        //     yield return null;
        // }

        // public void SetNodesFromScore(Clef clef, NoteNode[] nodes) {

        //     float subdivision = LengthMultipliers[NoteLength.EIGTH];

        //     int skip = 0;
        //     int index = 0;

        //     for (int i = 0; i < m_Bars * 4f / subdivision; i++) {

        //         if (skip == 0) {
        //             // find the tone in the major scale
        //             for (int j = 0; j < MajorScale.Length; j++) {
        //                 if ((int)clef.tones[index] == (int)MajorScale[j]) {
        //                     nodes[i].UpdateTone(j);
        //                     break;
        //                 }
        //             }
        //             skip = (int)(Score.LengthMultipliers[clef.lengths[index]] / subdivision) - 1;
        //             nodes[i].length = clef.lengths[index];
        //             index++;
        //         }
        //         else {
        //             nodes[i].UpdateTone(0);
        //             nodes[i].length = NoteLength.EIGTH;
        //             skip--;
        //         }

        //     }

        // }

    }
}