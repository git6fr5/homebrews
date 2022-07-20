// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.IO;

namespace Monet.IO {

    ///<summary>
    ///
    ///<summary>
    public class ScoreData : Data {

        // Settings.
        public static string Directory = "Scores/";
        public static string Format = ".score";

        // Data.
        [SerializeField, ReadOnly] private string Name;

        // Constructor.
        public ScoreData(Score score, string filename) {
            // Settings.
            Name = filename;
        }

    }
}