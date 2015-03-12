using UnityEngine;
using System.Collections;

public class JarraController : MonoBehaviour {
	private bool estaEchando = true;
	private Animator animator;
	private bool colisiono = false;
	private bool flagunique = true;
	// Use this for initialization
	void Start(){
		NotificationCenter.DefaultCenter().AddObserver(this, "JarraDestroy");
		NotificationCenter.DefaultCenter().AddObserver(this, "ColisionoOlla");
	}
	void Awake(){
		animator = GetComponent<Animator> ();
	}
	void FixedUpdate(){
		if (Input.acceleration.x > 0.2f) {
			estaEchando = true;
		} else {
			estaEchando = false;
		}
		estaEchando = true;
	}
	void Update () {
		if (colisiono) {
			NotificationCenter.DefaultCenter ().PostNotification (this, "ActionPoints", estaEchando);
		}
		animator.SetBool("estaEchando", estaEchando);
	}
	void JarraDestroy(Notification notification){
		bool flag = (bool)notification.data;
		if (flag) {
			Destroy(gameObject);
		}
	}
	
	void ColisionoOlla(Notification notification){
		if (flagunique) {
			Handheld.Vibrate ();
			flagunique=false;
		}
		colisiono = (bool)notification.data;
	}
}