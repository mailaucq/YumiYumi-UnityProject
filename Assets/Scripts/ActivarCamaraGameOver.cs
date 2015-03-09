using UnityEngine;
using System.Collections;

public class ActivarCamaraGameOver : MonoBehaviour {
	public GameObject camaraGameOver;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter ().AddObserver (this,"GameOver");
	}

	void GameOver(Notification notificacion){
		NotificationCenter.DefaultCenter ().PostNotification (this, "DestroyAllGenerated", true);
		NotificationCenter.DefaultCenter ().PostNotification (this, "DestroyAllObjectReceta", true);
		camaraGameOver.SetActive (true);
		canvas.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
