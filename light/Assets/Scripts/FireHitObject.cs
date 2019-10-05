using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireHitObject : MonoBehaviour, IHitable
{
    public UnityEvent OnFireHit;

    public void OnHit(int damage, bool onFire)
    {
        if (!onFire) return;
        Hit();
    }

    public virtual void Hit() {
        OnFireHit?.Invoke();
    }
}
