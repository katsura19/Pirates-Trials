using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BreakoutUIController : MonoBehaviour {

	// Objetos de UI
	public GameObject uiIdle;
	public GameObject uiPlaying;
	// Objeto correspondiente a los controles de Android
	public GameObject androidControls;
	// Objetos controlador
	public BallController ballController;
	public BricksController bricksController;
	// Variable y elemento texto para puntos 
	private int points = 0;
	public Text pointsText;
	// Variable y elemento texto para vidas
	private int lives = 0;
	public Text livesText;
	// Elemento texto para información de vidas restantes
	public Text infoText;

	// Use this for initialization
	void Start () {
		// Se recupera el valor de puntos que se corresponde con el número de bloques destruibles
		points = BricksController.numeroBloques;
		// Se carga el objeto texto con el valor de puntos
		pointsText.text = points.ToString ();
		// Se recuperan las vidas restantes del contador global de juego
		lives = Variables_Globales.variables_Globales.playingLives;
		// Se carga el objeto texto con el valor de vidas restantes
		livesText.text = lives.ToString ();
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
			// Carga de la pantalla de Game Over
			SceneManager.LoadScene ("GameOver"); 
		}
	}

	// Método de decremento de puntos
	public void DecreasePoints(){
		// Restamos 1 a los puntos
		points = points - 1;
		// Añadimos 1 punto a nuestro marcador de puntos globales
		Variables_Globales.variables_Globales.pointsLevels++;
		// Añadimos los puntos a nuestro marcador de texto
		pointsText.text = points.ToString ();

		// Si los puntos, bloques restantes, es menor o igual a 0
		if (points <= 0){
			// Se actualiza el valor de vidas globales con las vidas restantes
			Variables_Globales.variables_Globales.playingLives = lives;
			// Se añade una vida
			Variables_Globales.variables_Globales.playingLives++;
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

	// Método de decremento de vidas
	public void DecreaseLives(){
		// Se reduce en 1 el número de vidas restantes
		lives = lives - 1;
		// Si el número de vidas es menor que 1
		if (lives < 1) {
			// Carga de la pantalla de Game Over
			SceneManager.LoadScene ("GameOver"); 
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
		// Cargamos la escena actual
		SceneManager.LoadScene ("Breakout");
	}

	// Método para cargar escena a través del nombre introducido como parámetro
	public void ChangeScene(string scene){
		SceneManager.LoadScene (scene);
	}
}
