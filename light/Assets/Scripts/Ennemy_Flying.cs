using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;


public class Ennemy_Flying : Ennemy
{
    //[BoxGroup("Ennemy Stats")]
    public float speed;
    //[BoxGroup("Ennemy Stats")]
    public float pauseTime;
    public enum MovingType { AllerRetour, Boucle };
    public MovingType movementType;
    public List<Vector3> waypoints = new List<Vector3>(1);
    int targetWaypointIndex = 1;
    public enum State { Standing, Walking, Dying }
    public bool facingRight = false;
    

    public override void Start()
    {
        base.Start();
        waypoints[0] = transform.position;
        targetWaypointIndex = 1;
        Activate();
    }

    IEnumerator WalkingTo(Vector3 destination) {
        Vector3 origin = transform.position;
        Vector3 end = destination;
        Vector3 pos = origin;
        float percentage = 0;
        if (((end - origin).x < 0&&facingRight)|| ((end - origin).x > 0 && !facingRight))
            Flip();
        while (Vector3.Distance(pos, end) > 0.1f) {
            pos = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
            transform.position = pos;
            percentage += speed * Time.deltaTime / 10f;
            yield return new WaitForFixedUpdate();
        }
        targetWaypointIndex++;
        if (movementType == MovingType.AllerRetour&& targetWaypointIndex > waypoints.Count - 1){
            ReverseIndex();
            targetWaypointIndex = 1;
        }
        if (movementType == MovingType.Boucle && targetWaypointIndex > waypoints.Count - 1)
        {
            targetWaypointIndex = 0;
        }
        yield return new WaitForSeconds(pauseTime);
        StartCoroutine(WalkingTo(GetTargetWaypoint()));
        yield return null;
    }

    public void Activate() {
        StartCoroutine(WalkingTo(GetTargetWaypoint()));
    }

    public void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    Vector3 GetTargetWaypoint() {
        return waypoints[targetWaypointIndex];
    }

    Vector3 GetWaypoint(int index) {
        return waypoints[index];
    }

    void ReverseIndex() {
        waypoints.Reverse();
    }

    private void Reset()
    {
        Debug.Log("RESET");
        waypoints = new List<Vector3>();
        waypoints.Add(transform.position);
        //waypoints[0] = transform.position;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null) {
            player.TakeDamageAction();
        }
    }
}


