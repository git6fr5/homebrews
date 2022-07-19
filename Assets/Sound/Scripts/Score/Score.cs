/* --- Libraries --- */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.IO;

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

        public void Save(string filename) {
            ScoreData data = new ScoreData(this, filename);
            Monet.IO.Data.SaveJSON(data, filename, ScoreData.Directory, ScoreData.Format);
        }

        public void Open(string filename) {
            ScoreData data = Monet.IO.Data.OpenJSON(filename, ScoreData.Directory, ScoreData.Format) as ScoreData;
            if (data != null) {
                Load(data);
            }
        }

        public void Load(ScoreData data) {
            
        }

    }

}