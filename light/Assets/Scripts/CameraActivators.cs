using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivators : MonoBehaviour
{
    public GameObject Camera;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))Camera.SetActive(true);
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) Camera.SetActive(false);
    }
}
