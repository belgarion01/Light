using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    Vector3 mousePosition;
    public Transform pointOfGun;
    public float rotationOffset;

    public GameObject Bullet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 direction = new Vector3(difference.x, difference.y, 0f).normalized;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ+rotationOffset);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.z += 90f;
            GameObject bul = Instantiate(Bullet, pointOfGun.position, Quaternion.Euler(targetRotation));
            bul.GetComponent<Bullet>().direction = direction;
        }
    }
}
