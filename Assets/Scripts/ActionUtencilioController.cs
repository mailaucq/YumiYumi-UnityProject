using UnityEngine;
using System.Collections;

public class ActionUtencilioController : MonoBehaviour {
	//GUI
	public Texture2D imagenFrente;
	public Texture2D imagenFondo;
	public Texture2D imagenFondo2;
	public float WIDTH = 100;
	public float WIDTHBOX = 4;
	public float HEIGHTBOX = 4;
	public string MEDIDANAME;
	public GUIText medidaTextGui;
	//Calculate Points
	public int PUNTOSGANADOS = 1;
	public float MAXCANTIDAD=100;
	public float RANGEMEDIDA=10;
	public float SCALAMEDIDA=0.1f;
	private float maxCantidadTmp;
	private float incrementValue;
	private float medida;
	private bool flagPuntoSumado;
	private bool flagPuntoRestado;
	private float WIDTHTmp;
	private float REALMEDIDA;
	private string medidaTxt = "";
	void Start(){
		medida = 0;
		maxCantidadTmp = MAXCANTIDAD;
		incrementValue = SCALAMEDIDA;
		WIDTHTmp = WIDTH;
		flagPuntoSumado = false;
		flagPuntoRestado = false;
		UpdateMedidaText ();
		NotificationCenter.DefaultCenter().AddObserver(this, "ActionMedida");
		NotificationCenter.DefaultCenter().AddObserver(this, "ActionPoints");
	}
	void FixedUpdate(){
		UpdateMedidaText ();
	}
	void UpdateMedidaText(){
		medidaTxt = System.Math.Round (medida, 2).ToString ();
		if (REALMEDIDA<=1 && RANGEMEDIDA != 0) {
			medidaTxt = System.Math.Round (medida/WIDTH, 2).ToString ();
		}
		medidaTextGui.text = "" + medidaTxt + " / "+REALMEDIDA;
	}
	void OnGUI () {
		GUI.BeginGroup (new Rect (Screen.width / 2 - Screen.width / WIDTHBOX,Screen.height / 2 + Screen.height / HEIGHTBOX,256,WIDTH));
		if (imagenFondo != null) {
			GUI.Box (new Rect (0, 0, 256, WIDTH), imagenFondo2, "");
			GUI.BeginGroup (new Rect (0, 0, 256, WIDTHTmp));
			GUI.Box (new Rect (0, 0, 256, WIDTH), imagenFondo, "");
			GUI.BeginGroup (new Rect (0, 0, 256, WIDTH-MAXCANTIDAD));
			GUI.Box (new Rect (0, 0, 256, WIDTH), imagenFrente, "");
			GUI.EndGroup ();
			GUI.EndGroup ();
		} else {
			GUI.Box (new Rect (0, 0, 256, WIDTH), imagenFrente, "");
		}
		GUI.EndGroup ();
	}
	
	void ActionMedida(Notification notification){
		bool flag =(bool)notification.data;
		if (flag) {
			if (WIDTHTmp == medida) {
				MAXCANTIDAD -= incrementValue;
			}
			WIDTHTmp -= incrementValue;
			medida += incrementValue;
		}
	}
	
	void ActionPoints (Notification notification) {
		bool flagAction = (bool)notification.data;
		if (flagAction) {
			NotificationCenter.DefaultCenter ().PostNotification (this, "ActionMedida", flagAction);
			NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaAngry", false);
			NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaFire", false);
			NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSurprise", false);
			NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaHappy", false);
			NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSerio", false);
			if (maxCantidadTmp == medida) {
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaHappy", true);
			} else if (medida < RANGEMEDIDA) {
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSerio", true);
			} else if (medida > maxCantidadTmp - RANGEMEDIDA && medida < maxCantidadTmp) {
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaSurprise", true);
			} else if (medida < maxCantidadTmp + RANGEMEDIDA && medida > maxCantidadTmp) {
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaAngry", true);
			} else if (medida > maxCantidadTmp + RANGEMEDIDA) {
				NotificationCenter.DefaultCenter ().PostNotification (this, "MaestroEstaFire", true);
			}
			if(maxCantidadTmp + RANGEMEDIDA >= medida && maxCantidadTmp - RANGEMEDIDA <= medida && flagPuntoSumado == false){
				NotificationCenter.DefaultCenter ().PostNotification (this, "CalculatePoints", PUNTOSGANADOS);
				flagPuntoSumado = true;
			} 
			else if(maxCantidadTmp + RANGEMEDIDA < medida && flagPuntoRestado == false){
				NotificationCenter.DefaultCenter ().PostNotification (this, "CalculatePoints", -PUNTOSGANADOS);
				flagPuntoRestado = true;
			} 
		}
	}
	public void resetValues(float cantidad,string medidaName,float scaleMedida,float rangeMedida){
		REALMEDIDA = cantidad;
		if (REALMEDIDA <= 1 && rangeMedida!=0) {
			MAXCANTIDAD = REALMEDIDA * WIDTH;
		} else {
			MAXCANTIDAD = REALMEDIDA;
		}
		MEDIDANAME = medidaName;
		SCALAMEDIDA = scaleMedida;
		RANGEMEDIDA = rangeMedida;
		medida = 0;
		maxCantidadTmp = MAXCANTIDAD;
		incrementValue = SCALAMEDIDA;
		WIDTHTmp = WIDTH;
		flagPuntoSumado = false;
		flagPuntoRestado = false;
	}
}