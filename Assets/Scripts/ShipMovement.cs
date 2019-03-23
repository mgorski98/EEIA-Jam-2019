﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipMode {
	Horizontal, Vertical
}

[RequireComponent(typeof(Rigidbody))]
public class ShipMovement : MonoBehaviour
{
	public Gyroscope gyro = null;
	public Rigidbody rb = null;
	public Rigidbody ufoRb = null;

	public float speed;

	public float tilt;
	public float clampValue;
	public float delta;

	public ShipMode movementMode;

	public Quaternion basicRotation;
	public Quaternion basicCameraRotation;

	public Camera cam;

	public Vector3 offset;

	public float rotateAngle;

	public Transform beamPoint;


    void Start()
    {
    	basicCameraRotation		= cam.transform.rotation;
    	basicRotation 			= this.transform.rotation;
    	movementMode 			= ShipMode.Horizontal;
        gyro 					= Input.gyro;
        gyro.enabled 			= true;
        rb 						= transform.parent.gameObject.GetComponent<Rigidbody>();
        ufoRb 					= GetComponent<Rigidbody>();
        beamPoint.gameObject.SetActive(false);
    }

    void Update()
    {
    	//rb.velocity = ufoRb.velocity = Vector3.zero;
    	switch (movementMode) {
    		case ShipMode.Horizontal:
		       /* rb.velocity += new Vector3(Input.acceleration.x, 0f, Input.acceleration.y) *Time.deltaTime * 3f * speed;
		        rb.velocity = Vector3.ClampMagnitude(rb.velocity, clampValue);
		        ufoRb.velocity = rb.velocity;
		        RotateShip();*/
		       	if (Input.acceleration.y > delta || Input.acceleration.y < -delta) {
		       		rb.velocity += transform.forward * speed * Input.acceleration.y;
		       	}
		       	if (Input.acceleration.x > delta || Input.acceleration.x < -delta) {
		       		rb.velocity += transform.right * Input.acceleration.x * speed;
		       	}
		       	rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
		        rb.velocity = Vector3.ClampMagnitude(rb.velocity, clampValue);
		        ufoRb.velocity = rb.velocity;
		        RotateShip();
    			break;
    		case ShipMode.Vertical:
    			rb.velocity = new Vector3(rb.velocity.x, Input.acceleration.y, rb.velocity.z) * speed;
    			ufoRb.velocity = rb.velocity;
    			break;
    	}
    	//Debug.Log(Input.acceleration.ToString());
    	
    }

    public void ToggleShipMode() {
    	rb.velocity = ufoRb.velocity = Vector3.zero;
    	if (movementMode == ShipMode.Horizontal) movementMode = ShipMode.Vertical;
    	else movementMode = ShipMode.Horizontal;
    }

    public void BeamStart() {
    	beamPoint.gameObject.SetActive(true);
    }

    public void BeamStop() {
    	beamPoint.gameObject.SetActive(false);
    }



    void RotateShip() {
    	this.transform.rotation = Quaternion.Lerp(basicRotation,Quaternion.Euler(new Vector3(ufoRb.velocity.z, 0f, -ufoRb.velocity.x) * 1.33f), tilt);
    }   
}
