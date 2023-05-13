using System;
using TMPro;
using UnityEngine;

namespace Platformer.Scripts {
	public class BrickBreaker: MonoBehaviour {
		public  Camera          MainCam;
		public  TextMeshProUGUI TimeText;
		private int             timeLeft;

		private void Start() {
			// timeLeft      = 100;
			// TimeText.text = timeLeft.ToString();
			// InvokeRepeating("DecrementTime", 0, 1);
		}

		private void DecrementTime() {
			if (timeLeft <= 0) {
				Debug.Log("You failed to clear the level.");
			}
			else {
				timeLeft--;
				TimeText.text = $"Time\n{timeLeft}";
			}
		}
		
		private void Update() {
			// if (!Input.GetMouseButtonDown(0)) return;
			// Ray        ray = MainCam.ScreenPointToRay(Input.mousePosition);
			// RaycastHit hitInfo;
			// if (Physics.Raycast(ray, out hitInfo)) {
			// 	switch (hitInfo.collider.gameObject.tag) {
			// 		case "Brick":
			// 			score          += 100;
			// 			ScoreText.text =  $"{score}:00000";
			// 			Destroy(hitInfo.collider.gameObject);
			// 			break;
			// 		case "?Block":
			// 			CoinCount.text = $"x{(++coins):000}";
			// 			break;
			// 	}
			// }
		}
	}
}