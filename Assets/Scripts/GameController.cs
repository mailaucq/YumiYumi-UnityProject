using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
	//public GUISkin skin;
	public string action;
	public float WIDTHBOX = 2;
	public float HEIGHTBOX = 4;
	private float actual_width;
	private float actual_height;
	public float native_width = 640.0f;
	public float native_height = 400.0f;
	public float resolution_ration_x;
	public float resolution_ration_y;
	
	public float baseWidth = 60f;
	public float baseHeight = 60f;
	
	private int countStep = 0;
	private List<Receta> recetaList = new List<Receta>();
	private Receta currentReceta;
	void Awake(){
		CargarDatosReceta ();
		
	}
	void Start(){
		LoadReceta ();
		countStep = 0;
		NotificationCenter.DefaultCenter().AddObserver(this, "PlayReceta");
	}
	
	/*void OnGUI () {
		if (currentReceta != null) {
			actual_height = Screen.height;
			actual_width = Screen.width;
			
			resolution_ration_x = actual_width / native_width;
			resolution_ration_y = actual_height / native_height;
			
			float height = baseHeight * resolution_ration_y;
			float width = baseWidth * resolution_ration_x;
			
			float i_height = actual_height / HEIGHTBOX;
			float i_width = actual_width / WIDTHBOX;
			
			if (GUI.Button (new Rect (i_width, i_height, width, height), "",
			                skin.GetStyle (action + "_button"))) {
				if (countStep < currentReceta.recetaMedidaList.Count) {
					RecetaMedida recetaMedida = currentReceta.recetaMedidaList[countStep];
					NotificationCenter.DefaultCenter ().PostNotification (this, "StepRecetaIntruction", recetaMedida);
				}
				if (countStep == currentReceta.recetaMedidaList.Count) {
					NotificationCenter.DefaultCenter ().PostNotification (this, "GameOver", true);
				}
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaAngry", false);
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaFire", false);
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSurprise", false);
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaHappy", false);
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSerio", false);
				countStep++;
			}
		}
	}*/
	public void PlayReceta(Notification Notification){
		string key = (string)Notification.data;
		if (countStep < currentReceta.numberStep) {
			RecetaMedida recetaMedida = currentReceta.recetaMedidaMap[key];
			NotificationCenter.DefaultCenter ().PostNotification (this, "StepRecetaIntruction", recetaMedida);
		}
		if (countStep == currentReceta.numberStep) {
			NotificationCenter.DefaultCenter ().PostNotification (this, "GameOver", true);
		}
		/*NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaAngry", false);
		NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaFire", false);
		NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSurprise", false);
		NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaHappy", false);
		NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSerio", false);*/
		countStep++;
	}
	void LoadReceta(){
		int index = EstadoJuego.estadoJuego.recetaIndex;
		if (index >= 0 && index < recetaList.Count) {
			currentReceta = recetaList[index];
		}
	}
	void CargarDatosReceta(){
		Receta receta = new Receta ();
		receta.addRecetaMedida ("Hielo", new RecetaMedida (5,"unidades","Hielo","MedidaCesta","UtencilioDestinoCoctelera","Poner",1,0));
		receta.addRecetaMedida ("Pisco", new RecetaMedida (0.6f,"onza","Pisco","MedidaJarra","UtencilioDestinoCoctelera","Vertir",0.1f,5));
		receta.addRecetaMedida ("Jugo de Limon", new RecetaMedida (0.3f,"onza","Jugo de Limon","MedidaJarra","UtencilioDestinoCoctelera","Vertir",0.1f,5));
		receta.addRecetaMedida ("Jarabe de Goma", new RecetaMedida (0.3f,"onza","Jarabe de Goma","MedidaJarra","UtencilioDestinoCoctelera","Vertir",0.1f,5));
		receta.addRecetaMedida ("Clara de Huevo", new RecetaMedida (0.25f,"onza","Clara de Huevo","MedidaJarra","UtencilioDestinoCoctelera","Vertir",0.1f,5));
		//receta.add (new RecetaMedida (0.25f,"onza","Clara de Huevo","UtencilioOrigenJarra","UtencilioDestinoCoctelera","Vertir",0.1f,5));
		receta.addRecetaMedida ("Coctelera Tapa", new RecetaMedida (10,"segundos","Coctelera Tapa","MedidaCoctelera","UtencilioDestinoCoctelera","Batir",1,0));
		//receta3.add (new RecetaMedida (10,"segundos","Mezcla","UtencilioOrigenCoctelera","UtencilioDestinoCoctelera","Batir",0.1f,0));
		//receta3.add (new RecetaMedida (1,"onza","Limon Exprimido","UtencilioOrigenJarra","UtencilioDestinoCoctelera","Vertir",0.1f,0));
		//receta3.add (new RecetaMedida (2,"unidades","Clara de huevo","UtencilioOrigenJarra","UtencilioDestinoCoctelera","Vertir",0.1f,10));
		//receta3.add (new RecetaMedida (12,"cubos","Hielo","UtencilioOrigenTaza","UtencilioDestinoCoctelera","Vertir",0.1f,10));
		//receta3.add (new RecetaMedida (10,"segundos","Mezcla","UtencilioOrigenCoctelera","UtencilioDestinoCoctelera","Batir",0.1f,0));
		//receta.add (new RecetaMedida (1,"unidad","Pisco Sour","UtencilioOrigenCoctelera","UtencilioDestinoVaso","Servir",1,0));
		recetaList.Add (receta);
	}
}