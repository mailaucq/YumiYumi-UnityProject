using UnityEngine;
using System.Collections;

public class ColliderController : MonoBehaviour {
	public string nameCollision;
	public string notificationCollision;
	void OnParticleCollision(GameObject other) {
		Debug.Log ("Colisionnnnnnnnnn"+other.name);
		if (other.name.Equals (nameCollision)) {
			NotificationCenter.DefaultCenter ().PostNotification (this, notificationCollision, true);
		}
	}
}
