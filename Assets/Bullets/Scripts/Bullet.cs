/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BulletPattern;

namespace BulletPattern {

    /// <summary>
    /// The actual bullet.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour {

        /* --- Variables --- */
        #region Variables

        public static float LifeTime = 10f;

        // Components.
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public Rigidbody2D body;

        // Settings.
        [HideInInspector] public List<Vector2> kinematics;

        #endregion

        /* --- Unity --- */
        #region Unity

        void FixedUpdate() {
            if (kinematics.Count <= 1) { return; }
            
            float deltaTime = Time.deltaTime;
            for (int i = 1; i < kinematics.Count; i++) {
                kinematics[i - 1] += kinematics[i] * deltaTime;
            }
            body.velocity = kinematics[0];

        }

        #endregion

        /* --- Initialization --- */
        #region Initialization

        public void Create(List<Vector2> kinematics) {
            if (kinematics.Count <= 0) { return; }

            Bullet newBullet = Instantiate(gameObject, transform.position, Quaternion.identity, null).GetComponent<Bullet>();
            newBullet.Init(kinematics);
        }

        public void Init(List<Vector2> kinematics) {
            body = GetComponent<Rigidbody2D>();
            body.gravityScale = 0f;
            spriteRenderer = GetComponent<SpriteRenderer>();

            gameObject.SetActive(true);
            body.velocity = kinematics[0];
            this.kinematics = kinematics;

            Destroy(gameObject, LifeTime);
        }

        #endregion
        
    }

}

