/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class LevelSelector : DungeonSelector {

    /* --- Variables --- */
    public int increment;

    /* --- Unity --- */
    void OnMouseOver() {
        spriteRenderer.material.SetFloat("_OutlineWidth", 0.05f);
    }

    void OnMouseExit() {
        spriteRenderer.material.SetFloat("_OutlineWidth", 0f);
    }

    /* --- Override --- */
    protected override void Select() {
        loader.id = loader.id + increment;
        if (loader.id < 0) { loader.id = 0; }
    }

}
