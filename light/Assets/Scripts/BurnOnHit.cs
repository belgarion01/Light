using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnOnHit : FireHitObject
{
    //public float burnTime;
    public AnimationCurve curve;

    public override void Hit()
    {
        base.Hit();
        StartCoroutine(Burn());
    }

    IEnumerator Burn() {
        Collider2D coll = GetComponent<Collider2D>();
        if (coll != null) coll.enabled = false;
        foreach (Transform child in transform) {
            Collider2D col = child.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        float value = 0f;
        while (value < 1f) {
            foreach (SpriteRenderer sprite in sprites) {
                sprite.material.SetFloat("_FadePercent", curve.Evaluate(value));
            }
            value += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
