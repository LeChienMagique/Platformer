using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour {
	public float      CameraSpeed = 1f;
	public GameObject Player;
	
	void Update() {
		Vector3 pos = transform.position;
		pos.x              = Player.transform.position.x;
		transform.position = pos;

		// if (Input.GetKey(KeyCode.LeftArrow)) {
		// 	transform.Translate(Vector3.left * (CameraSpeed * Time.deltaTime));
		// }
		// if (Input.GetKey(KeyCode.RightArrow)) {
		// 	transform.Translate(Vector3.right * (CameraSpeed * Time.deltaTime));
		// }

	}
}