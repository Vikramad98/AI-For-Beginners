using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAICar : MonoBehaviour {

	public Transform goal;
	public Text readout;
	float speed = 0;
	public float rotSpeed = 1.0f;
	public float acceleration = 5f;
	public float decceleration = 5f;
	public float minSpeed = 0f;
	public float maxSpeed = 100f;
	float BrakeAngle=20.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 lookAtGoal = new Vector3(goal.position.x, 
										this.transform.position.y, 
										goal.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;

		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
												Quaternion.LookRotation(direction), 
												Time.deltaTime*rotSpeed);

		if (Vector3.Angle(goal.forward, this.transform.forward) > BrakeAngle && speed>10)
		{
			speed = Mathf.Clamp(speed - (decceleration * Time.deltaTime), minSpeed, maxSpeed);

		}
		else
		{
			speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);

		}

		this.transform.Translate(0,0,speed);
		AnalogueSpeedConverter.ShowSpeed(speed, 0, 100);
		if(readout)
			readout.text = "" + speed;
	}
}
