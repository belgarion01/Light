using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectiles : MonoBehaviour
{
    public GameObject VFX_Death;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            player.TakeDamageAction();
        }
        Instantiate(VFX_Death, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
