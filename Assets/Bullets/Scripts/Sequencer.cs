/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using BulletPattern;

namespace BulletPattern {

    /// <summary>
    /// Sequences bullet patterns together.
    /// </summary>
    [System.Serializable]
    public class Sequencer : MonoBehaviour {

        /* --- Data --- */
        #region Data.

        /// <summary>
        /// Data Structure for a single bullet pattern.
        /// </summary>
        [System.Serializable]
        public class BulletPattern {

            public int count;
            public float interval;
            public List<Vector2> kinematics;
            public int kinematicDepth;
            public int repeats;

            public BulletPattern(int count, float interval, List<Vector2> kinematics, int repeats) {
                this.count = count;
                this.interval = interval;
                this.kinematics = kinematics;
                this.repeats = repeats;
                this.kinematicDepth = kinematics.Count;
            }

            public void SetValues(Display display, List<Draggable> draggables) {
                count = display.Count;
                interval = display.Interval;

                kinematics = new List<Vector2>();
                for (int i = 0; i < kinematicDepth; i++) {
                    if (i < draggables.Count) {
                        kinematics.Add(draggables[i].transform.position);
                    }
                }
            }

        }

        #endregion

        /* --- Variables --- */
        #region Variables.

        // Components.
        public Indicator indicator;
        public Display display;
        public Bullet bullet;
        public Draggable draggable;

        // Settings.
        [SerializeField] private List<BulletPattern> patterns = new List<BulletPattern>();
        [SerializeField, ReadOnly] private int activePattern = -1;
        public int ActivePatternIndex => activePattern;
        public int ActiveCount => activePattern < patterns.Count && activePattern >= 0 ? patterns[activePattern].count : -1;
        public float ActiveInterval => activePattern < patterns.Count && activePattern >= 0 ? patterns[activePattern].interval : -1f;


        private List<Draggable> draggables = new List<Draggable>();
        private float ticks;
        private int count;


        #endregion

        /* --- Unity --- */
        #region Unity

        void Start() {

        }

        void Update() {
            CheckEdits();
        }

        void FixedUpdate() {
            float deltaTime = Time.fixedDeltaTime;
            Fire(deltaTime);
        }

        #endregion

        /* --- Initialization --- */
        #region Initialization

        public void Init() {

        }

        #endregion

        /* --- Pattern Editing --- */
        #region Pattern Editing

        private void CheckEdits() {
            if (activePattern < 0 || activePattern >= patterns.Count) { return; }
            patterns[activePattern].SetValues(display, draggables);
        }

        public void Add() {
            patterns.Add(new BulletPattern(1, 1f, new List<Vector2>() { Vector2.right }, 1));
            indicator.Create(this, patterns.Count - 1);
            SetActivePattern(patterns.Count - 1);
        }

        public void Remove(int index) {
            patterns.RemoveAt(index);

            // Reset the indicators.
            Indicator[] indicators = (Indicator[])Sequencer.GetAll<Indicator>();
            for (int i = 0; i < indicators.Length; i++) {
                Destroy(indicators[i].gameObject);
            }

            for (int i = 0; i < patterns.Count; i++) {
                indicator.Create(this, i);
            }

            // Reset the active pattern.
            int newActivePattern = activePattern;
            if (activePattern >= index) {
                newActivePattern -= 1;
            }
            SetActivePattern(newActivePattern);
        }

        public void SetActivePattern(int index) {
            activePattern = index;
            Indicator[] indicators = (Indicator[])Sequencer.GetAll<Indicator>();
            for (int i = 0; i < indicators.Length; i++) {
                indicators[i].Highlight(indicators[i].Index == index);
            }
            display.Set(this);

            for (int i = 0; i < draggables.Count; i++) {
                Destroy(draggables[i].gameObject);
            }

            draggables = new List<Draggable>();
            for (int i = 0; i < patterns[activePattern].kinematicDepth; i++) {
                Vector2 position = Vector3.zero;
                if (i < patterns[activePattern].kinematics.Count) {
                    position = patterns[activePattern].kinematics[i];
                }
                Draggable newDraggable = draggable.Create(position);
                draggables.Add(newDraggable);
            }

            count = 0;
            ticks = 0f;
        }

        #endregion

        /* --- Firing --- */
        #region Firing

        public void Fire(float deltaTime) {
            if (activePattern < 0 || activePattern >= patterns.Count) { return; }
            if (patterns[activePattern].count <= 0) { return; }

            float cooldown = patterns[activePattern].interval / patterns[activePattern].count;
            if (cooldown < 0.001f) { return; }

            ticks += Time.deltaTime;
            while (ticks > cooldown) {
                List<Vector2> offsetKinematics = GetOffsetKinematics();
                bullet.Create(offsetKinematics);
                count = count >= patterns[activePattern].count ? 0 : count + 1;
                ticks -= cooldown;
            }

        }

        private List<Vector2> GetOffsetKinematics() {
            List<Vector2> offsetKinematics = new List<Vector2>();
            for (int i = 0; i < patterns[activePattern].kinematics.Count; i++) {
                //if (i < draggables.Length) {
                //    Vector2 v = Quaternion.Euler(0f, 0f, count * draggables[i].OffsetAngle) * patterns[activePattern].kinematics[i];
                //    offsetKinematics.Add(v);
                //}
                offsetKinematics.Add(patterns[activePattern].kinematics[i]);
            }
            return offsetKinematics;
        }

        #endregion

        /* --- Generics --- */
        #region Generics

        public static object[] GetAll<T>() {
            return GameObject.FindObjectsOfType(typeof(T));
        }

        #endregion

    }

}

