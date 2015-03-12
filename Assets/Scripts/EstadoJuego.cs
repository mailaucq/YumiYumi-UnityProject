using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class EstadoJuego : MonoBehaviour {
	//public GoogleAnalyticsV3 googleAnalytics;
	public int puntuacionMaxima = 0;
	public int recetaIndex = 0;
	public string idUser = "Yumi";
	public int[] puntuacionPerfecta = {75,90,35};
	public string[] recetaName = {"Adobo","Ceviche","Pisco Sour"};
	public string [] pictureLink = {"http://yumiyumi.site90.com/images/cuyes/cuy1.png",
		"http://yumiyumi.site90.com/images/cuyes/cuy2.png",
		"http://yumiyumi.site90.com/images/cuyes/cuy3.png"};
	public static EstadoJuego estadoJuego;
	private String nombreArchivo;
	//private GooglePlayCloud cloud;
	void Awake(){
		nombreArchivo = Application.persistentDataPath + "/datos.dat";
		if (estadoJuego == null) {
			estadoJuego = this;
			DontDestroyOnLoad (gameObject);
			//cloud = new GooglePlayCloud();
			//PlayGamesPlatform.DebugLogEnabled = false;
			//PlayGamesPlatform.Activate();
		} else if (estadoJuego != this) {
			Destroy(gameObject);
		}
		
	}
	// Use this for initialization
	void Start () {
		Cargar ();
		//NotificationCenter.DefaultCenter ().PostNotification (this,"GameStart");
		//InicioSesionGooglePlay (true);
	}
	/*public void InicioSesionGooglePlay(bool silencioso){
		((PlayGamesPlatform)Social.Active).Authenticate ((bool success) => {
			if(success){
				googleAnalytics.LogSocial("Google", "login", "Usuario "+EstadoJuego.estadoJuego.idUser+" logueado");
				googleAnalytics.LogEvent("Google", "Login", "Usuario "+EstadoJuego.estadoJuego.idUser +" logueado", 1);
				cloud.CloudLoad();
			}
		}, silencioso);
	}*/
	// Update is called once per frame
	void Update () {
	}
	
	public void Guardar(bool online){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(nombreArchivo);
		DatosAGuardar datos = new DatosAGuardar ();
		datos.puntuacionMaxima = puntuacionMaxima;
		bf.Serialize (file, datos);
		file.Close ();
		
		if (online) {
			//cloud.CloudSave (datos);
		}
	}
	void Cargar(){
		if (File.Exists (nombreArchivo)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (nombreArchivo, FileMode.Open);
			DatosAGuardar datos = (DatosAGuardar)bf.Deserialize (file);
			puntuacionMaxima = datos.puntuacionMaxima;
			file.Close ();
		} else {
			puntuacionMaxima = 0;
		}
	}
}
[Serializable]
class DatosAGuardar{
	public int puntuacionMaxima;
}
/*class GooglePlayCloud : GooglePlayGamesCloudHelper<DatosAGuardar>{
	protected override DatosAGuardar ConflictoAlGuardar(int slot, DatosAGuardar local, DatosAGuardar server){
		if (local.puntuacionMaxima > server.puntuacionMaxima) {
			return local;		
		} else {
			return server;
		}
	}
	protected override void DatosDescargados (int slot, DatosAGuardar data){
		if (data == null) return;
		if (data.puntuacionMaxima > EstadoJuego.estadoJuego.puntuacionMaxima) {
			EstadoJuego.estadoJuego.puntuacionMaxima = data.puntuacionMaxima;
			EstadoJuego.estadoJuego.Guardar(false);
		}
	}
}*/