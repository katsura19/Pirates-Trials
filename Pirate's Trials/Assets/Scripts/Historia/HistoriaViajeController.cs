using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaViajeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Obtención del nombre de la escena actual
		string scene = SceneManager.GetActiveScene ().name;
		// Verificación de si es la primera pantalla del modo historia
		if (scene == "Historia_Inicio"){
			// Carga de vida durante el juego con valor almacenado en vidas por defecto
			Variables_Globales.variables_Globales.playingLives = 
				Variables_Globales.variables_Globales.globalLives;
			// Inicialización de niveles finalizados
			Variables_Globales.variables_Globales.playingLevels = 0;
			// Inicialización de barcos a superar en nivel viaje
			Variables_Globales.variables_Globales.shipGoals = 0;
			// Carga de barcos a superar en nivel viaje de modo aleatorio
			Variables_Globales.variables_Globales.shipGoals = Random.Range (5, 15);
			// Inicialización de puntos almacenados durante la partida
			Variables_Globales.variables_Globales.pointsLevels = 0;
		}
		// Si es cualquier otra escena
		else {
			// Inicialización de barcos a superar en nivel viaje
			Variables_Globales.variables_Globales.shipGoals = 0;
			// Carga de barcos a superar en nivel viaje de modo aleatorio
			Variables_Globales.variables_Globales.shipGoals = Random.Range (5, 15);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Si se pulsa espacio se carga el nivel de viaje
		if (Input.GetKeyDown ("space")){
			SceneManager.LoadScene ("Viaje");
		}
	}
}
