﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Goomba : Ennemy
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamageAction();
        }
    }
}
