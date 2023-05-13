using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController: MonoBehaviour {
	public  float RunForce                = 50f;
	public  float JumpImpulseForce        = 20f;
	public  float JumpSustainForce        = 7.5f;
	public  float MaxHorizontalSpeed      = 6f;
	public  float TurboMaxHorizontalSpeed = 8f;
	private float baseMaxHorizontalSpeed  = 6f;

	public bool FeetInContactWithGround;

	private Rigidbody rb;
	private Bounds    bounds;
	private Animator  animator;

	public  TextMeshProUGUI CoinCount;
	public  TextMeshProUGUI ScoreText;
	public  TextMeshProUGUI TimeText;
	private int             timeLeft;
	private int             coins;
	private int             score;

	private bool levelEnded = false;

	void Start() {
		rb            = GetComponent<Rigidbody>();
		bounds        = GetComponent<Collider>().bounds;
		animator      = GetComponent<Animator>();
		timeLeft      = 100;
		TimeText.text = timeLeft.ToString();
		InvokeRepeating("DecrementTime", 0, 1);
	}

	private void DecrementTime() {
		if (timeLeft <= 0) {
			Debug.Log("You failed to clear the level.");
			levelEnded = true;
		}
		else {
			timeLeft--;
			TimeText.text = $"Time\n{timeLeft}";
		}
	}

	private void AddCoin(int amount = 1) {
		coins          += amount;
		CoinCount.text =  $"x{coins:000}";

		AddScore(amount * 100);
	}

	private void AddScore(int amount) {
		score          += amount;
		ScoreText.text =  $"Mario\n{score:000000}";
	}

	private void OnCollisionEnter(Collision other) {
		switch (other.gameObject.tag) {
			case "Goal":
				levelEnded = true;
				Debug.Log("You won!!!");
				break;
			case "Water":
				levelEnded = true;
				Debug.Log("You died.");
				break;
		}

		if (FeetInContactWithGround) {
			return;
		}

		switch (other.gameObject.tag) {
			case "?Block":
				AddCoin();
				break;
			case "Brick":
				Destroy(other.gameObject, 0.1f);
				AddScore(100);
				break;
		}
	}

	void Update() {
		if (levelEnded) {
			return;
		}

		FeetInContactWithGround = Physics.Raycast(transform.position, Vector3.down, bounds.extents.y + 0.1f);

		if (Input.GetKey(KeyCode.LeftShift)) {
			MaxHorizontalSpeed = TurboMaxHorizontalSpeed;
		}
		else {
			MaxHorizontalSpeed = baseMaxHorizontalSpeed;
		}

		float      axis = Input.GetAxis("Horizontal");
		Quaternion rot  = transform.rotation;
		rot.eulerAngles    = new Vector3(rot.x, axis < 0 ? -90 : 90, rot.z);
		transform.rotation = rot;

		rb.AddForce(Vector3.right * (axis * RunForce), ForceMode.Force);

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (FeetInContactWithGround) {
				rb.AddForce(Vector3.up * JumpImpulseForce, ForceMode.Impulse);
			}
			else {
				rb.AddForce(Vector3.up * JumpSustainForce, ForceMode.Force);
			}
		}

		float xVel = Mathf.Clamp(rb.velocity.x, -MaxHorizontalSpeed, MaxHorizontalSpeed);
		if (Math.Abs(axis) < 0.1f) {
			xVel *= 0.9f;
		}

		rb.velocity = new Vector3(xVel, rb.velocity.y, rb.velocity.z);

		float speed = rb.velocity.magnitude;
		animator.SetFloat("Speed", speed);
		animator.SetBool("Jumping", !FeetInContactWithGround);
	}
}