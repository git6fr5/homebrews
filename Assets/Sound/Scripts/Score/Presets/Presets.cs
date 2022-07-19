/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    public partial class Score {

        // public class Presets {

        //     public static Clef Random(int bars = 1, Clef clef = null) {

        //         float barLengthLeft = 4f * bars;
        //         List<Tone> tones = new List<Tone>();
        //         List<NoteLength> lengths = new List<NoteLength>();

        //         while (barLengthLeft > 0f) {
        //             Tone tone = MajorScale[Random.Range(0, 8)];
        //             tones.Add(tone);

        //             NoteLength noteLength = (NoteLength)Random.Range((int)NoteLength.HALF, (int)NoteLength.noteLengthCount);
        //             while (LengthMultipliers[noteLength] > barLengthLeft) {
        //                 noteLength = (NoteLength)Random.Range((int)noteLength, (int)NoteLength.noteLengthCount);
        //             }

        //             lengths.Add(noteLength);
        //             barLengthLeft -= LengthMultipliers[noteLength];//
        //         }

        //         if (clef == null) {
        //             clef = new Clef(tones, lengths);
        //         }
        //         else {
        //             clef.tones = tones;
        //             clef.lengths = lengths;
        //         }
        //         PrintClef(clef);
        //         return clef;

        //     }

        //     public static Clef Simple(int bars = 1) {

        //         List<Tone> tones = new List<Tone>();
        //         List<NoteLength> lengths = new List<NoteLength>();

        //         for (int i = 0; i < 4 * bars; i++) {
        //             tones.Add(Tone.P1);
        //             lengths.Add(NoteLength.EIGTH);
        //             tones.Add(Tone.REST);
        //             lengths.Add(NoteLength.EIGTH);
        //         }
            
        //         Clef clef = new Clef(tones, lengths);
        //         PrintClef(clef);
        //         return clef;

        //     }

        //     public static Clef Offbeat() {

        //         List<Tone> tones = new List<Tone>();
        //         List<NoteLength> lengths = new List<NoteLength>();

        //         for (int i = 0; i < 4; i++) {
        //             tones.Add(Tone.REST);
        //             lengths.Add(NoteLength.EIGTH);
        //             tones.Add(Tone.P1);
        //             lengths.Add(NoteLength.EIGTH);
        //         }

        //         Clef clef = new Clef(tones, lengths);
        //         return clef;

        //     }

        // }

    }

}