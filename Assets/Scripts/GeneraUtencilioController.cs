using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GeneraUtencilioController : MonoBehaviour {
	public float X;
	public float Y;
	public List<GameObject> list;
	void Start(){
		list = new List<GameObject> ();
		NotificationCenter.DefaultCenter().AddObserver(this, "DestroyAllGenerated");
		NotificationCenter.DefaultCenter().AddObserver(this, "GeneradoDestroy");
		NotificationCenter.DefaultCenter().AddObserver(this, "GeneraObjeto");
		NotificationCenter.DefaultCenter().AddObserver(this, "GeneraObjetoEstatico");
	}
	void GeneraObjeto(Notification notification){
		GameObject gameObject = (GameObject)notification.data;
		Vector3 spawnPosition = new Vector3 (X, Y, 0);
		Quaternion spawnRotation = Quaternion.identity;
		list.Add (Instantiate (gameObject, spawnPosition, spawnRotation) as GameObject);
		NotificationCenter.DefaultCenter ().PostNotification (this, "ActionPoints", true);
	}
	void GeneraObjetoEstatico(Notification notification){
		GameObject gameObject1 = (GameObject)notification.data;
		Vector3 spawnPosition = new Vector3 (gameObject1.transform.localPosition.x, gameObject1.transform.localPosition.y, 0);
		Quaternion spawnRotation = Quaternion.identity;
		list.Add (Instantiate (gameObject, spawnPosition, spawnRotation) as GameObject);
		NotificationCenter.DefaultCenter ().PostNotification (this, "ActionPoints", true);
	}
	void GeneradoDestroy(Notification notification){
		string name = (string)notification.data;
		foreach(GameObject o in list){
			if(o != null){
				if(o.name.Contains(name)){
					Destroy(o);
				}
			}
		}
	}
	void DestroyAllGenerated(Notification notification){
		bool flag = (bool)notification.data;
		if (flag) {
			foreach (GameObject o in list) {
				if (o != null) {
					Destroy (o);
				}
			}
		}
	}
}