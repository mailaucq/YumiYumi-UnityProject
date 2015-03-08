using UnityEngine;
using System.Collections;

public class CocteleraController : MonoBehaviour {
	private bool estaBatiendo = false;
	private string patternBatir = "";
	private bool enabledBatir = false;
	
	private float scaleX=0.0f;
	private float scaleY=0.0f;
	public float scaleRangeX = 0.001f;
	public float scaleRangeY = 0.001f;
	
	public GameObject vasoVacio;
	//private bool colisiono = false;
	
	void Start(){
		NotificationCenter.DefaultCenter ().AddObserver (this, "EnabledBatirCoctelera");
		NotificationCenter.DefaultCenter ().AddObserver (this, "LiquidoEnVaso");
	}
	void Awake(){
		
	}
	void FixedUpdate(){
		if (enabledBatir) {
			if (Input.acceleration.y > 1.0f) {
				patternBatir += "1";
				estaBatiendo = true;
			} else if (Input.acceleration.y < 0) {
				patternBatir += "0";
				estaBatiendo = true;
			} else {
				patternBatir = "";
				estaBatiendo = false;
			}
		}
	}
	void Update () {
		if (estaBatiendo && patternBatir.Contains("10")) {
			NotificationCenter.DefaultCenter ().PostNotification (this, "ActionPoints", estaBatiendo);
			patternBatir = "";
			estaBatiendo = false;
		}
	}
	
	void EnabledBatirCoctelera(Notification notification){
		enabledBatir = (bool)notification.data;
	}
	
	void LiquidoEnVaso (Notification notification) {
		if (vasoVacio.transform.localScale.y >= 0.0f) {
			vasoVacio.transform.localScale -= new Vector3 (scaleX, scaleRangeY, 0);
			//Debug.Log (vasoVacio.transform.localScale);
			//positionY = positionY + positionRangeY;
			//Vector3 vectorPosition = new Vector3 (0, positionY, 0);
			//cocteleraVacia.transform.localPosition = cocteleraVacia.transform.localPosition + vectorPosition;
		}
	}
}