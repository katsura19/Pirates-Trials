using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaGratisController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Llamada al método VidaGratis al pulsar espacio
		if (Input.GetKeyDown ("space")) {
			VidaGratis ();
		}
	}

	// Método para obtención de 1 vida, 10 puntos, y pasar a la pantalla de historia correspondiente
	public void VidaGratis(){
		// Añadimos 1 vida a nuestro contador de vidas
		Variables_Globales.variables_Globales.playingLives++;
		// Añadimos 10 puntos a nuestra puntuación
		Variables_Globales.variables_Globales.pointsLevels = Variables_Globales.variables_Globales.pointsLevels + 10;
		// Se incrementa en 1 los niveles jugados
		Variables_Globales.variables_Globales.playingLevels++;
		// Control de fin de juego
		// Si los niveles jugados son iguales o mayores a los niveles objetivo
		if (Variables_Globales.variables_Globales.playingLevels >=
			Variables_Globales.variables_Globales.globalLevels){
			// Carga de fin de juego óptimo
			SceneManager.LoadScene ("Historia_Final"); 
		}
		// Si no se ha alcanzado el número de niveles objetivo
		else {
			// Se evalúa el número de niveles superados y se carga la escena correspondiente
			switch (Variables_Globales.variables_Globales.playingLevels)
			{
			case 1:
				SceneManager.LoadScene ("Historia_Viaje_2"); 
				break;
			case 2:
				SceneManager.LoadScene ("Historia_Viaje_3"); 
				break;
			case 3:
				SceneManager.LoadScene ("Historia_Viaje_4"); 
				break;
			default:
				// Si no es ningún valor válido se cargará la pantalla de error
				SceneManager.LoadScene ("Pantalla_Error"); 
				break;
			}
		}
	}
}
