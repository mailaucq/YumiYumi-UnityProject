using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GeneraController : MonoBehaviour {
	public GameObject ingredienteAction;
	public bool onlyTime = false;
	public bool generarEstatico = false;
	private bool generar = true;
	private bool enabledGenera = false;
	void Start(){
		NotificationCenter.DefaultCenter().AddObserver(this, "EnabledGeneraDeIngrediente");
	}
	public void GenerarObject(){
		if (enabledGenera) {
			if (generar) {
				NotificationCenter.DefaultCenter ().PostNotification (this, "GeneraObjeto", ingredienteAction);
				if (onlyTime) {
					generar = false;
				}
			}
		}
	}
	void EnabledGeneraDeIngrediente(Notification notification){
		enabledGenera = (bool)notification.data;
	}
}