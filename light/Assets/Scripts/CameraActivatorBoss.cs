using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivatorBoss : CameraActivators
{
    public Boss boss;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            boss.BeginBoss();
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
