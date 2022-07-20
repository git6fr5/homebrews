/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    public class Note {

        public Score.Interval Interval;
        public Score.Tone Tone;

    }

    public class Clef {

        public Note[] Notes;

    }

}