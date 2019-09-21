using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Goomba : Ennemy
{
    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }
}
