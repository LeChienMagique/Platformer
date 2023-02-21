using TMPro;
using UnityEngine;

namespace Platformer.Scripts {
	public class BrickBreaker: MonoBehaviour {
		public  Camera          mainCam;
		public  TextMeshProUGUI coinCount;
		private int             coins;

		private void Update() {
			if (!Input.GetMouseButtonDown(0)) return;
			Ray        ray = mainCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo)) {
				switch (hitInfo.collider.gameObject.tag) {
					case "Brick":
						Destroy(hitInfo.collider.gameObject);
						break;
					case "?Block":
						coinCount.text = $"x{(++coins):000}";
						break;
				}
			}
		}
	}
}