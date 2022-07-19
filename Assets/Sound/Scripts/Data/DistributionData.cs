// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.IO;

namespace Monet.IO {

    [System.Serializable]
    public class DistributionData : Data {

        // Data.
        [SerializeField, ReadOnly] public string Name;
        [SerializeField, ReadOnly] public float[] Ratios;
        [SerializeField, ReadOnly] public float MaxValue;

        // Constructor.
        public DistributionData(Distribution distribution, string name) {
            // Settings.
            Name = name;
            Ratios = distribution.Ratios;
            MaxValue = distribution.MaxValue;
        }

    }

}