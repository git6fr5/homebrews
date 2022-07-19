/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    public enum Interval {
        Whole, 
        Half, 
        Quarter, 
        Eigth, 
        Sixteenth, 
        Count
    }

    public enum Tone {
        Rest, 
        P1, 
        m2, 
        M2, 
        m3, 
        M3, 
        P4, 
        b5, 
        P5, 
        m6, 
        M6, 
        m7, 
        M7, 
        P8, 
        m9, 
        M9, 
        m10, 
        M10, 
        P11, 
        b12, 
        P12, 
        Count
    }

    public enum Key { 
        A, 
        B, 
        C, 
        D, 
        E, 
        F, 
        G, 
        Count 
    }

    public static Dictionary<Tone, float> JustTemperedRatios = new Dictionary<Tone, float>(){
        { Tone.REST, 0f },
        { Tone.P1 , 1f },
        { Tone.M2 , 9f/8f },
        { Tone.M3 , 5f/4f },
        { Tone.P4 , 4f/3f },
        { Tone.P5 , 3f/2f },
        { Tone.M6 , 5f/3f },
        { Tone.M7 , 15f/8f },
        { Tone.P8 , 2f },
        { Tone.M9 , 18f/8f },
        { Tone.M10 , 10f/4f },
        { Tone.P11 , 8f/3f },
        { Tone.P12 , 6f / 2f }
    };

    public static Dictionary<Note, float> KeyFrequencies = new Dictionary<Note, float>() {
        { Note.A, 440f },
        { Note.B, 493.88f},
        { Note.C, 523.25f},
        { Note.D, 587.33f},
        { Note.E, 659.25f},
        { Note.F, 698.46f},
        { Note.G, 783.99f },
    };

    public static Tone[] MajorScale = new Tone[] { 
        Tone.REST, 
        Tone.P1, 
        Tone.M2, 
        Tone.M3, 
        Tone.P4, 
        Tone.P5, 
        Tone.M6, 
        Tone.M7, 
        Tone.P8, 
        Tone.M9, 
        Tone.M10, 
        Tone.P11, 
        Tone.P12 
    };

}