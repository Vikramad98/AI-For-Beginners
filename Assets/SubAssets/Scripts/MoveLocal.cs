using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] wayPoints;
    int currWaypoint=0;
    //public Transform goal;
    public float speed = 0.5f;
    public float accuracy = 0.1f;
    public float rotSpeed = 0.5f;
    void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (wayPoints.Length == 0) return;

        Vector3 lookAtGoal = new Vector3(wayPoints[currWaypoint].transform.position.x, this.transform.position.y, wayPoints[currWaypoint].transform.position.z);
        //this.transform.LookAt(lookAtGoal);
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        if (direction.magnitude<accuracy)
        {
            currWaypoint++;
            if (currWaypoint >= wayPoints.Length)
            {
                currWaypoint = 0;
            }
            

        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
