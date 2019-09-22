using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        IFire obj = collision.GetComponent<IFire>();
        if (obj != null) obj.SetFire(true);
    }
}
