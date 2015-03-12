using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GeneraUtencilioController : MonoBehaviour {
	public float X;
	public float Y;
	public List<GameObject> list;
	public Canvas canvas;
	public List<Sprite> sprites;
	public Dictionary<string,Sprite> OtherSprite;
	public List<GameObject> objetosGeneradores;
	public Dictionary<string,GameObject> objetosGeneradoresMap;
	SpriteRenderer image;
	void Awake(){
		OtherSprite = new Dictionary<string, Sprite> ();
		foreach(Sprite sprite in sprites){
			OtherSprite.Add(sprite.name,sprite);
		}
		objetosGeneradoresMap = new Dictionary<string, GameObject> ();
		foreach(GameObject objeto in objetosGeneradores){
			objetosGeneradoresMap.Add(objeto.name,objeto);
		} 
	}
	void Start(){
		list = new List<GameObject> ();
		NotificationCenter.DefaultCenter().AddObserver(this, "DestroyAllGenerated");
		NotificationCenter.DefaultCenter().AddObserver(this, "GeneradoDestroy");
		NotificationCenter.DefaultCenter().AddObserver(this, "GeneraObjeto");
		NotificationCenter.DefaultCenter().AddObserver(this, "GeneraObjetoX");
		NotificationCenter.DefaultCenter().AddObserver(this, "GeneraJarra");
	}
	void GeneraJarra(Notification notification){
		string keyNameSprite = (string)notification.data;
		Debug.Log (keyNameSprite);
		Vector3 spawnPosition = new Vector3 (X, Y, 0);
		Quaternion spawnRotation = Quaternion.identity;
		GameObject go = Instantiate (objetosGeneradoresMap["GeneraJarra"], spawnPosition, spawnRotation) as GameObject;
		image = go.GetComponentInChildren<SpriteRenderer>();
		image.sprite = OtherSprite [keyNameSprite];
		list.Add (go);
		NotificationCenter.DefaultCenter ().PostNotification (this, "ActionPoints", true);
	}
	void GeneraObjeto(Notification notification){
		string keyNameSprite = (string)notification.data;
		Debug.Log (keyNameSprite);
		Vector3 spawnPosition = new Vector3 (X, Y, 0);
		Quaternion spawnRotation = Quaternion.identity;
		GameObject go = Instantiate (objetosGeneradoresMap["GeneraObjeto"], spawnPosition, spawnRotation) as GameObject;
		image = go.GetComponentInChildren<SpriteRenderer>();
		if (image != null) {
			image.sprite = OtherSprite [keyNameSprite];
			go.AddComponent<BoxCollider2D>();
		}
		list.Add (go);
		NotificationCenter.DefaultCenter ().PostNotification (this, "ActionPoints", true);
	}
	void GeneraObjetoX(Notification notification){
		string keyNameSprite = (string)notification.data;
		Debug.Log (keyNameSprite);
		Vector3 spawnPosition = new Vector3 (X, Y, 0);
		Quaternion spawnRotation = Quaternion.identity;
		GameObject go = Instantiate (objetosGeneradoresMap["Genera_" + keyNameSprite], spawnPosition, spawnRotation) as GameObject;
		image = go.GetComponentInChildren<SpriteRenderer>();
		if (image != null) {
			image.sprite = OtherSprite [keyNameSprite];
			go.AddComponent<BoxCollider2D>();
		}
		list.Add (go);
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