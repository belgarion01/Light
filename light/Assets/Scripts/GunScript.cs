using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunScript : MonoBehaviour
{
    Vector3 mousePosition;
    public Transform pointOfGun;
    public float rotationOffset;
    private PlayerController pController;

    public GameObject Bullet;

    public UnityEvent Shoot;

    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 direction = new Vector3(difference.x, difference.y, 0f).normalized;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = pController.isRight ? Quaternion.Euler(0, 0, rotZ+rotationOffset) : Quaternion.Euler(0, 0, rotZ + rotationOffset+180f);

        if (Input.GetMouseButtonDown(0)) {
            Shoot.Invoke();
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.z += 90f;
            GameObject bul = Instantiate(Bullet, pointOfGun.position, Quaternion.Euler(targetRotation));
            bul.GetComponent<Bullet>().direction = direction;
        }
    }
}
