using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Importamos los componentes de interfaz
using UnityEngine.UI;
// Importamos el gestor de escenas
using UnityEngine.SceneManagement;

public class Viaje_Minijuego_GameController : MonoBehaviour {

	// Variable para la velocidad del fondo
	public float parallaxSpeed = 0.02f;
	// Variable para importar los objetos RawImage
	public RawImage background;
	public RawImage platform;
	// Variable correspondiente a UI Idle
	public GameObject uiIdle; 
	// Variable correspondiente a UI Points
	public GameObject uiPoints; 

	// Estados del juego mediante enumerador, parado, jugando, muerto, listo para reiniciar
	public enum GameState {Idle, Playing, Death, Ready};
	// Seleccionamos la opción por defecto
	public GameState gameState = GameState.Idle;

	// Variable para nuestro objeto jugador
	public GameObject player;

	// Variable para nuestro objeto generador de enemigos
	public GameObject enemyGenerator;

	// Variable para la música de fondo
	private AudioSource musicBackground;

	// Variable para puntuación y acceso al elemento texto
	private int points = 0;
	public Text pointsText;
	public Text recordText;

	// Variables para dificultad incremental
	public float incDifficulty = 0.10f; 	// Porcentaje de incremento de velocidad
	public float timeDifficulty = 5f; 		// Segundos tras los que se incrementa
	public int 	 pointsDifficulty = 20; 	// Cada cuántos puntos se resetea la dificultad

	// Use this for initialization
	void Start () {
		// Recuperamos el componente para la música
		musicBackground = GetComponent<AudioSource> ();
		// Recuperamos el valor de record
		recordText.text = "Máximos barcos superados: " + GetMaxScore ().ToString ();
	}
	
	// Update is called once per frame
	void Update () {

		// Comenzar juego pulsando espacio en el teclado, el ratón, o pantalla,
		//pero verificando primero que se encuentre en estado parado
		if (gameState == GameState.Idle && (
		        Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0))) {
			// Activación de estado jugando
			gameState = GameState.Playing;
			// Desactivamos el UI Idle
			uiIdle.SetActive (false);
			// Activamos el UI Points
			uiPoints.SetActive (true);
			// Envíamos un mensaje a nuestro jugador, llamando al método y a la
			//animación que queremos que se ejecute al iniciar el juego
			player.SendMessage ("UpdateState", "Player_Movement");
			// Envíamos un mensaje a nuestro generador, llamando al método de 
			//inicio de generación de enemigos
			enemyGenerator.SendMessage ("StartGenerate");
			// Iniciamos música de fondo
			musicBackground.Play ();
			// Activamos el sistema de partículas
			player.SendMessage ("WaterPlay");
			// Incremento de dificultad invocando repetidamente, primero desde
			//nuestro timeDifficulty, y cada timeDifficulty nuevamente
			InvokeRepeating ("IncreaseDifficulty", timeDifficulty, timeDifficulty);
		} 
		// El juego se encuentra iniciado
		else if (gameState == GameState.Playing) {
			// Activamos el parallax
			Parallax ();
		}
		// Listo para reinicio
		else if (gameState == GameState.Ready) {
			// Si pulsamos sobre la pantalla se reiniciará el juego
			if (Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0)){
				Restart ();
			}
		}
	}

	// Efecto parallax para fondo y superficie
	void Parallax(){
		// En cada fotograma calcularemos la velocidad final utilizando deltaTime 
		//que es la velocidad de nuestro equipo
		float finalSpeed = parallaxSpeed * Time.deltaTime;
		// Modificamos la propiedad uvRect en el eje x añadiendo la velocidad 
		//que hemos calculado, y dejando el resto igual
		background.uvRect = new Rect (background.uvRect.x + finalSpeed, 0f, 1f, 1f);
		// A la plataforma le añadiremos un poco más de velocidad
		platform.uvRect = new Rect (platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
	}

	// Método para reiniciar el juego
	public void Restart(){
		// Cargamos la escena actual
		SceneManager.LoadScene ("Viaje_Minijuego");
	}

	// Método para el incremento de puntos
	public void IncreasePoints(){
		// Añadimos 1 a los puntos
		points++;
		// Añadimos los puntos a nuestro marcador de texto
		pointsText.text = points.ToString ();
		// Verificamos si la puntuación actual es mayor que el record
		if (points >= GetMaxScore ()){
			// Mostramos por pantalla el valor del record
			recordText.text = "Máximos barcos superados: " + points.ToString ();
			// Almacenamos el record
			SetMaxScore (points);
		}
		// Si se ha alcanzado un múltiplo de los puntos de dificultad se reseteará la dificultad
		if (points % pointsDifficulty == 0){
			// Reseteo de la dificultad
			ResetDifficulty ();
			// Se reinicia el incremento de dificultad
			InvokeRepeating ("IncreaseDifficulty", timeDifficulty, timeDifficulty);
		}
	}

	// Método para devolver la puntuación máxima
	public int GetMaxScore(){
		// Devuelve el valor almacenado en Record, y por defecto se muestra 0
		return PlayerPrefs.GetInt ("Record", 0);
	}

	// Método para almacenar la puntuación máxima
	public void SetMaxScore(int currentPoints){
		// Graba el valor almacenado en currentPoints en el registro Record
		PlayerPrefs.SetInt ("Record", currentPoints);
	}

	// Método para incrementar la dificultad
	void IncreaseDifficulty(){
		// Por defecto el timeScale del juego es 1, 
		//así que se le incrementa en nuestro valor
		Time.timeScale += incDifficulty;
	}

	// Cancelación del incremento de velocidad
	public void ResetDifficulty(){
		// Cancelamos la invocación del método
		CancelInvoke ("IncreaseDifficulty");
		// Devolvemos el timeScale a su valor normal
		Time.timeScale = 1f;
	}

	// Método para cargar escena a través del nombre introducido como parámetro
	public void ChangeScene(string scene){
		SceneManager.LoadScene (scene);
	}

}
