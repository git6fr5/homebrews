// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.IO;

namespace Monet.IO {

    [System.Serializable]
    public class ModifierData : Data {

        // Data.
        [SerializeField, ReadOnly] public string Name;
        [SerializeField, ReadOnly] public float Ratio;
        [SerializeField, ReadOnly] public float MaxValue;

        // Constructor.
        public ModifierData(Modifier modifier, string name) {
            // Settings.
            Name = name;
            Ratio = modifier.Ratio;
            MaxValue = modifier.MaxValue;
        }

    }

}