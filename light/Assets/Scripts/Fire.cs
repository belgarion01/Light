using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public bool Active = true;
    public ParticleSystem VFX_Feu;

    private void Start()
    {
        VFX_Feu.gameObject.SetActive(Active);
    }

    private void Update()
    {
        VFX_Feu.gameObject.SetActive(Active);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Active)
        {
            IFire obj = collision.GetComponent<IFire>();
            if (obj != null) obj.SetFire(true);
        }
        else
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                if (bullet.onFire) SetActive(true);
            }
        }
    }

    public void SetActive(bool value) {
        Active = value;
    }
}
