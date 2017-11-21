using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoriaFinalController : MonoBehaviour {

	// Elementos texto
	public Text livesText;
	public Text pointsText;
	public Text recordText;

	// Use this for initialization
	void Start () {
		// Carga del elemento texto con las vidas finales almacenadas
		livesText.text = "Vidas finales: " + Variables_Globales.variables_Globales.playingLives.ToString ();
		// Carga del elemento texto con la puntuación final almacenada
		pointsText.text = "Puntuación final: " + Variables_Globales.variables_Globales.pointsLevels.ToString ();
		// Verificamos si la puntuación actual es mayor que el record
		if (Variables_Globales.variables_Globales.pointsLevels >= GetMaxPoints ()){
			// Mostramos por pantalla mensaje informativo
			recordText.text = "¡ Has superado la puntuación máxima ! ";
			// Almacenamos el record
			SetMaxPoints (Variables_Globales.variables_Globales.pointsLevels);
		}
		else {
			// No mostramos por pantalla mensaje informativo
			recordText.text = " ";
		}
	}

	// Update is called once per frame
	void Update () {
		// Si se pulsa espacio se pasa a la pantalla de menú
		if (Input.GetKeyDown ("space")){
			SceneManager.LoadScene ("Menu");
		}
	}

	// Método para devolver la puntuación máxima
	public int GetMaxPoints(){
		// Devuelve el valor almacenado en History, y por defecto se muestra 0
		return PlayerPrefs.GetInt ("History", 0);
	}

	// Método para almacenar la puntuación máxima
	public void SetMaxPoints(int currentPoints){
		// Graba el valor almacenado en currentPoints en el registro History
		PlayerPrefs.SetInt ("History", currentPoints);
	}
}
