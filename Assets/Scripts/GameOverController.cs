using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {
	public GoogleAnalyticsV3 googleAnalytics;
	public GUIText total;
	public GUIText record;
	public GUIText titleGameOver;
	public Puntuacion puntuacion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnEnable(){
		if(20<= puntuacion.puntuacion){
			titleGameOver.text = "You win!";
		}
		total.guiText.text = puntuacion.puntuacion.ToString ();
		record.guiText.text = EstadoJuego.estadoJuego.puntuacionMaxima.ToString ();
		googleAnalytics.LogEvent("GameOver", "Show", "Usuario "+ EstadoJuego.estadoJuego.idUser+" Puntuacion "+puntuacion.puntuacion.ToString (),  1);
	}
}
