using UnityEngine;
using System.Collections;

public class LiquidoCoctelera : MonoBehaviour {
	private bool colisiono=false;
	public string objectNameCollision = "UtencilioDestinoCoctelera";
	
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "LiquidoEnCoctelera");
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.name.Equals (objectNameCollision)) {
			colisiono=true;
		}
	}
	
	void LiquidoEnCoctelera (Notification notification) {
		bool flagAction = (bool)notification.data;
		if (flagAction) {
			NotificationCenter.DefaultCenter ().PostNotification (this, "ColisionoOlla", true);
			Debug.Log (colisiono);
			if (!colisiono) {
				NotificationCenter.DefaultCenter ().PostNotification (this, "LiquidoEnVaso");
			}
		}
	}
}