using UnityEngine;
using System.Collections;

public class BatirEnabledController : MonoBehaviour {
	public bool enabledGenera = true;
	public bool enabledBatir = true;
	void FixedUpdate () {
		NotificationCenter.DefaultCenter ().PostNotification (this, "EnabledGeneraDeIngrediente", enabledGenera);
		NotificationCenter.DefaultCenter ().PostNotification (this, "EnabledBatirCoctelera", enabledBatir);
	}
}
