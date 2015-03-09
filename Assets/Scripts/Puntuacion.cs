using UnityEngine;
using System.Collections;
public class Puntuacion : MonoBehaviour {
	public GUIText marcador;
	public GameObject scoreCenter;
	private int _puntuacion = 0;
	private int key	= 0;
	public int puntuacion{
		get{return _puntuacion ^ key;}
		set {
			key = Random.Range(0, int.MaxValue);
			_puntuacion = value ^ key;
		}
	}

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "CalculatePoints");
		NotificationCenter.DefaultCenter().AddObserver(this, "GameOver");
		ActualizarMarcador ();
	}
	
	void GameOver(Notification notification){
		if(puntuacion > EstadoJuego.estadoJuego.puntuacionMaxima){
			EstadoJuego.estadoJuego.puntuacionMaxima = puntuacion;
			EstadoJuego.estadoJuego.Guardar(true);
		}
		Social.ReportScore (puntuacion, "CgkIgIim25sfEAIQBg", (bool success) => {});
		if (puntuacion >= 25) {
			Social.ReportProgress("CgkIgIim25sfEAIQAQ",100.0, (bool success) => {});
		}
		if (puntuacion >= 50) {
			Social.ReportProgress("CgkIgIim25sfEAIQAg",100.0, (bool success) => {});
		}
		if (puntuacion >= 100) {
			Social.ReportProgress("CgkIgIim25sfEAIQAw",100.0, (bool success) => {});
		}
		if (puntuacion >= 150) {
			Social.ReportProgress("CgkIgIim25sfEAIQBA",100.0, (bool success) => {});
		}
		if (puntuacion >= 200) {
			Social.ReportProgress("CgkIgIim25sfEAIQBQ",100.0, (bool success) => {});
		}
	}

	void CalculatePoints(Notification notification){
		int puntosAIncrementar = (int)notification.data;
		puntuacion += puntosAIncrementar;
		Debug.Log ("Incremento " + puntuacion + " Total" + puntuacion);
		ActualizarMarcador ();
		ShowScoreCenter(puntosAIncrementar);
	}

	void ActualizarMarcador(){
		marcador.guiText.text = "Score: " + puntuacion;
	}
	void ShowScoreCenter(int puntosAIncrementar){		
		string sign = puntosAIncrementar > 0 ? "+":"";
		scoreCenter.guiText.text = sign + puntosAIncrementar.ToString();
		StartCoroutine(ShowObject(scoreCenter,3));
	}
	IEnumerator ShowObject(GameObject obj, float timeInSeconds)	{
		obj.SetActive (true);
		yield return new WaitForSeconds(timeInSeconds);
		obj.SetActive (false);
	}
}