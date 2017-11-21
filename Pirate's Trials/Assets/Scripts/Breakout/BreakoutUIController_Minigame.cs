using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BreakoutUIController_Minigame : MonoBehaviour {

	// Objetos de UI
	public GameObject uiIdle;
	public GameObject uiPlaying;
	// Objeto correspondiente a los controles de Android
	public GameObject androidControls;

	// Objetos controlador
	public BallController_Minigame ballController;
	public BricksController bricksController;

	// Variable y elemento texto para puntos 
	private int points = 0;
	public Text pointsText;

	// Variable y elemento texto para vidas
	private int lives = 3; // Valor de vidas por defecto para minijuego
	public Text livesText;

	// Elemento texto para información de vidas restantes
	public Text infoText;

	// Use this for initialization
	void Start () {
		// Se recupera el valor de puntos que se corresponde con el número de bloques destruibles
		points = BricksController.numeroBloques;
		// Se carga el objeto texto con el valor de puntos
		pointsText.text = points.ToString ();
		// Verificación de plataforma actual
		if (Variables_Globales.variables_Globales.currentPlatformAndroid == false){
			// Si no es Android se desactivan los controles táctiles
			androidControls.SetActive (false);
		} else {
			// Si es Android se activan los controles táctiles
			androidControls.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Se verifica si el número de vidas es mayor que 0
		if (lives > 0) {
			// Si el estado de juego es jugando
			if (ballController.isPlaying) {
				// Se desactivan los elementos de UI de estado parado
				uiIdle.SetActive (false);
				// Se activan los elementos de UI de estado jugando
				uiPlaying.SetActive (true);
			} 
			// Si el estado de juego no es jugando
			else {
				// Se activan los elementos de UI de estado parado
				uiIdle.SetActive (true);
				// Se desactivan los elementos de UI de estado jugando
				uiPlaying.SetActive (false);
			}
		}
		// Si el número de vidas es igual o menor que 0
		else {
			// Reinicio de nivel
			Restart ();
		}
	}

	// Método de decremento de puntos
	public void DecreasePoints(){
		// Restamos 1 a los puntos
		points = points - 1;
		// Añadimos los puntos a nuestro marcador de texto
		pointsText.text = points.ToString ();
		// Si se terminan los bloques
		if (points <= 0){
			// Reinicio de nivel
			Restart ();
		}
	}

	// Método de decremento de vidas
	public void DecreaseLives(){
		// Se reduce en 1 el número de vidas restantes
		lives = lives - 1;
		// Si el número de vidas es menor que 1
		if (lives < 1) {
			// Reinicio de nivel
			Restart ();
		} 
		// Si el número de vidas es mayor que 0
		else {
			// Se carga el elemento texto con el nuevo valor
			livesText.text = lives.ToString ();
			// Si el número de vidas restantes es 1 se carga el texto correspondiente
			if (lives == 1) {
				infoText.text = "Queda " + lives.ToString () + " vida";
			} 
			// Si el número de vidas restantes es mayor que 1 se carga el texto correspondiente
			else {
				infoText.text = "Quedan " + lives.ToString () + " vidas";
			}
		}
	}

	// Método para reiniciar el nivel
	public void Restart(){
		SceneManager.LoadScene ("Breakout_Minijuego");
	}

	// Método para cargar escena a través del nombre introducido como parámetro
	public void ChangeScene(string scene){
		SceneManager.LoadScene (scene);
	}
}
