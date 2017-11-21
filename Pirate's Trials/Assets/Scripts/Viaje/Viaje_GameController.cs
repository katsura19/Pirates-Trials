using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Importamos los componentes de interfaz
using UnityEngine.UI;
// Importamos el gestor de escenas
using UnityEngine.SceneManagement;

public class Viaje_GameController : MonoBehaviour {

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
	public Text livesText;
	public Text goalsText;

	// Variables para dificultad incremental
	public float incDifficulty = 0.20f; // Porcentaje de incremento de velocidad
	public float timeDifficulty = 8f; // Segundos tras los que se incrementa

	private int nivelHistoria = 0; // Nivel de historia aleatorio
	private int pointsInitial = 0; // Puntos almacenados al iniciar el nivel

	// Use this for initialization
	void Start () {
		// Recuperamos el componente para la música
		musicBackground = GetComponent<AudioSource> ();
		// Verificación vidas restantes
		if (Variables_Globales.variables_Globales.playingLives <= 0) {
			// Si las vidas son iguales o menores a cero se carga la pantalla de Game Over
			SceneManager.LoadScene ("GameOver");
		} else {
			// Si las vidas no han llegado a cero
			// Carga de las vidas restantes en el campo texto
			livesText.text = Variables_Globales.variables_Globales.playingLives.ToString ();
			// Carga del objetivo a superar en este nivel
			goalsText.text = "Objetivo: Superar " + 
				Variables_Globales.variables_Globales.shipGoals.ToString () + " barcos";
			// Recuperación de puntos globales al cargar el nivel
			pointsInitial = Variables_Globales.variables_Globales.pointsLevels;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Comenzar juego pulsando espacio en el teclado,el ratón, o pantalla,
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
				// Reiniciamos los puntos del nivel con los puntos globales almacenados
				Variables_Globales.variables_Globales.pointsLevels = pointsInitial;
				// Reiniciamos el nivel
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
		// A la superficie le añadiremos un poco más de velocidad
		platform.uvRect = new Rect (platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
	}

	// Método para reiniciar el nivel
	public void Restart(){
		// Cargamos la escena actual
		SceneManager.LoadScene ("Viaje");
	}

	// Método para el incremento de puntos
	public void IncreasePoints(){
		// Añadimos 1 a los puntos
		points++;
		// Añadimos 1 a los puntos globales almacenados
		Variables_Globales.variables_Globales.pointsLevels++;
		// Añadimos los puntos a nuestro marcador de texto
		pointsText.text = points.ToString ();
		// Verificamos si la puntuación actual es el objetivo a cumplir
		if (points >= Variables_Globales.variables_Globales.shipGoals) {
			// Devolvemos el timeScale a su valor normal
			Time.timeScale = 1f;
			// Añadimos una vida
			Variables_Globales.variables_Globales.playingLives++;
			// Se obtiene un valor aleatorio para el siguiente nivel de historia
			nivelHistoria = Random.Range (1, 4);
			// Cargamos el siguiente nivel de historia según el valor aleatorio
			switch (nivelHistoria)
			{
			case 1:
				SceneManager.LoadScene ("Historia_2"); 
				break;
			case 2:
				SceneManager.LoadScene ("Historia_3"); 
				break;
			case 3:
				SceneManager.LoadScene ("Historia_4"); 
				break;
			default:
				// Si no es ningún valor válido se cargará la pantalla de error
				SceneManager.LoadScene ("Pantalla_Error"); 
				break;
			}
		}
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
