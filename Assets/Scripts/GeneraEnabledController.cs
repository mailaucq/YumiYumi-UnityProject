using UnityEngine;
using System.Collections;

public class GeneraEnabledController : MonoBehaviour {
	public bool enabledGenera = true;
	// Use this for initialization
	void FixedUpdate () {
		NotificationCenter.DefaultCenter ().PostNotification (this, "EnabledGeneraDeIngrediente", enabledGenera);
	}
}