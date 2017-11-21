using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Variables_Globales : MonoBehaviour {

	// Instancia para verificación de destrucción de objeto
	public static Variables_Globales variables_Globales;

	// Variable globales a utilizar en la aplicación
	public int globalLives = 5;		// Número de vidas que tendrá el jugador inicialmente
	public int playingLives;		// Número de vidas que tendrá el jugador durante la historia
	public int globalLevels = 4;	// Número de minijuegos que tendrá que superar el jugador
	public int playingLevels;		// Número de minijuegos superados durante la historia
	public int shipGoals;			// Número de barcos a superar durante la historia
	public int pointsLevels;        // Puntos alcanzados en los niveles
	public bool currentPlatformAndroid = false;	// Indicador de si la plataforma actual es Android

	// Esta función se lee antes que cualquier otra, incluido el Start
	void Awake () {
		// Verificamos si el objeto no está instanciado
		if (variables_Globales == null){
			// Instanciamos el objeto
			variables_Globales = this;

			// Verificación de plataforma Android			
			#if UNITY_ANDROID
			currentPlatformAndroid = true;
			#else 
			currentPlatformAndroid = false;
			#endif

			// Indicamos la no destrucción de este objeto durante la vida de la aplicación
			DontDestroyOnLoad (gameObject);
		} 
		// Si variables_Globales es distinto de la instancia actual se destruye
		else if (variables_Globales!=this){
			Destroy (gameObject);
		}
	}
}
