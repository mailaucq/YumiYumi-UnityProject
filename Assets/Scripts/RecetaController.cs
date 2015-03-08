using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecetaController : MonoBehaviour {
	//Calculate Points
	private float cantidad = 0;
	private string medidaName = "";
	private float scaleMedida = 0;
	private string ingredienteName = "";
	//private string accionStepName = "";
	private string utencilioOrigenName = "";
	private string utencilioDestinoName = "";
	private float rangeMedida = 0;
	private RecetaMedida recetaMedida;
	//public GUIText stepRecetaText;
	private ActionUtencilioController actionUtencilioController;
	public GameObject[] ingredientes;
	public GameObject[] utenciliosMedida;
	public GameObject[] utenciliosDestino;
	private string previewUtencilioDestino;
	private bool utencilioChanged;
	void Start(){
		recetaMedida = new RecetaMedida ();
		NotificationCenter.DefaultCenter().AddObserver(this, "StepRecetaIntruction");
		NotificationCenter.DefaultCenter().AddObserver(this, "DestroyAllObjectReceta");
	}
	void StepRecetaIntruction (Notification notification) {
		recetaMedida = (RecetaMedida)notification.data;
		cantidad = recetaMedida.cantidad;
		medidaName = recetaMedida.medidaName;
		scaleMedida = recetaMedida.scaleMedida;
		ingredienteName = recetaMedida.ingredienteName;
		utencilioOrigenName = recetaMedida.utencilioOrigenName;
		utencilioDestinoName = recetaMedida.utencilioDestinoName;
		//accionStepName = recetaMedida.accionStepName;
		rangeMedida = recetaMedida.rangeMedida;
		//stepRecetaText.guiText.text = recetaMedida.PrintRecetaMedida ().ToString ();
		if (previewUtencilioDestino == "") {
			previewUtencilioDestino = utencilioDestinoName;
			utencilioChanged = false;
		} else if (previewUtencilioDestino != utencilioDestinoName) {
			utencilioChanged = true;
			previewUtencilioDestino = utencilioDestinoName;
		} else {
			utencilioChanged = false;
		}
		for(int i = 0;i< utenciliosMedida.Length;i++){
			actionUtencilioController = utenciliosMedida[i].gameObject.GetComponent<ActionUtencilioController> ();
			if(utencilioOrigenName == utenciliosMedida[i].name){		
				actionUtencilioController.resetValues(cantidad,medidaName,scaleMedida,rangeMedida);
				utenciliosMedida[i].SetActive(true);
			} else {
				GeneraUtencilioController contr = gameObject.GetComponent<GeneraUtencilioController>();
				if(contr != null && contr.list != null){
					foreach(GameObject tmp in contr.list){
						if(tmp != null){
							Debug.Log(tmp.name);
							if(tmp.name.IndexOf("Jarra")>0){
								NotificationCenter.DefaultCenter ().PostNotification (this, "JarraDestroy", true);
							}
						}
					}
				}
				if(utencilioChanged){
					NotificationCenter.DefaultCenter ().PostNotification (this, "DestroyAllGenerated", true);
				}
				utenciliosMedida[i].SetActive(false);
			}
		}
		for(int i = 0;i< utenciliosDestino.Length;i++){
			if(utencilioDestinoName == utenciliosDestino[i].name){				
				utenciliosDestino[i].SetActive(true);
			} else {
				utenciliosDestino[i].SetActive(false);
			}
		}
		for(int i = 0;i< ingredientes.Length;i++){
			if(ingredienteName == ingredientes[i].name){
				ingredientes[i].SetActive(true);
			} else {
				ingredientes[i].SetActive(false);
			}
		}
	}
	void DestroyAllObjectReceta(Notification notification){
		bool flag = (bool)notification.data;
		if (flag) {
			for (int i = 0; i< ingredientes.Length; i++) {
				Destroy (ingredientes [i]);
			}
			for (int i = 0; i< utenciliosDestino.Length; i++) {
				Destroy (utenciliosDestino [i]);
			}
			for (int i = 0; i< utenciliosMedida.Length; i++) {
				Destroy (utenciliosMedida [i]);
			}
//			Destroy(stepRecetaText);
		}
	}
}