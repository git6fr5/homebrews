/* --- Libraries --- */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    [System.Serializable]
    public partial class Score {

        // Path.
        public static string Directory = "DataFiles/Scores/";

        // Settings.
        public Key m_Key;
        public int m_Bars;
        public Clef[] m_Clefs;

        public static void Save(ref Score score, string filepath) {
            string jsonStr = JsonUtility.ToJson(score);
        }

        public static void Open(ref Score score, string filepath) {
            // if (setNodes) {
            //     SetNodesFromScore(treble, trebleNodes);
            //     SetNodesFromScore(bass, bassNodes);
            // }
        }

    }
    
}