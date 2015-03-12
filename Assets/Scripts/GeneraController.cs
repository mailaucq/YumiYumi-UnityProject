using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GeneraController : MonoBehaviour {
	public string ingredienteSpriteName;
	public bool onlyTime = false;
	public bool generarEstatico = false;
	private bool generar = true;
	private bool enabledGenera = false;
	public string tipo="Objeto";
	private string before;
	void Start(){
		NotificationCenter.DefaultCenter().AddObserver(this, "EnabledGeneraDeIngrediente");
	}
	public void GenerarObject(){
		if (gameObject.name != before) {
			enabledGenera = false;
		}
		if (enabledGenera) {
			if (generar) {
					NotificationCenter.DefaultCenter ().PostNotification (this, "Genera"+tipo, ingredienteSpriteName);
					if (onlyTime) {
							generar = false;
					}
			}
		} else {
			NotificationCenter.DefaultCenter().PostNotification(this, "PlayReceta",gameObject.name);
			NotificationCenter.DefaultCenter ().PostNotification (this, "Genera"+tipo, ingredienteSpriteName);
			if (onlyTime) {
				generar = false;
			}
			before = gameObject.name;
		}
	}
	void EnabledGeneraDeIngrediente(Notification notification){
		enabledGenera = (bool)notification.data;
	}
}