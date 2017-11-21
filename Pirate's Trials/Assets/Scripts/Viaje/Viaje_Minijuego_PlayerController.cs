using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viaje_Minijuego_PlayerController : MonoBehaviour {

	// Variable para importar el GameController
	public GameObject gameController;
	// Variable para nuestro objeto generador de enemigos
	public GameObject enemyGenerator;
	// Variable de animación
	private Animator animator;
	// Variables para los sonidos del jugador
	private AudioSource playerSounds;
	public	AudioClip playerJump;
	public  AudioClip playerDie;
	public  AudioClip playerPoint;
	// Variable para posición inicial
	private float initialY;

	// Variable para el sistema de partículas
	public ParticleSystem waterParticle;

	// Use this for initialization
	void Start () {
		// Obtenemos el componente animador
		animator = GetComponent<Animator> ();
		// Recuperamos el componente para los sonidos
		playerSounds = GetComponent<AudioSource> ();
		// Posición inicial del jugador en el eje Y
		initialY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		// Mecánica de salto con verificación de estado válido
		// Debe encontrarse en estado jugando, pulsar la tecla, y que la posición corresponda a la de inicio
		if ((gameController.GetComponent<Viaje_Minijuego_GameController>().gameState ==
			Viaje_Minijuego_GameController.GameState.Playing) &&
			((Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0))) &&
			(transform.position.y == initialY)){
			// Actualizamos el estado a salto
			UpdateState ("Player_Jump");
			// Sonido de salto
			playerSounds.clip = playerJump;
			playerSounds.Play ();

		}

	}

	// Actulización de estados
	// Mediante un string se le indica el estado a modificar
	public void UpdateState(string state = null){
		if (state != null){
			// Actualización del estado introducido en la llamada
			animator.Play (state);
		}
	}

	// Controlamos la colisión con el Trigger Enemy
	// Sobreescribimos el método ya existente OnTriggerEnter2D
	//indicando con quién colisiona mediante el Collider2D
	void OnTriggerEnter2D(Collider2D col){
		// Verificamos si colisiona con el enemigo mediante el tag y el estado es jugando
		if ((col.gameObject.tag == "Enemy") && 
			(gameController.GetComponent<Viaje_Minijuego_GameController>().gameState ==
				Viaje_Minijuego_GameController.GameState.Playing)) {
			// Activamos el estado de muerte del jugador
			UpdateState ("Player_Die");
			// Modificamos el estado de juego a muerto
			gameController.GetComponent<Viaje_Minijuego_GameController> ().gameState =
				Viaje_Minijuego_GameController.GameState.Death;
			// Finalizamos la invocación de enemigos
			enemyGenerator.SendMessage ("FinishGenerate");
			// Finalizamos la música y reproducimos el sonido de muerte
			gameController.GetComponent<AudioSource> ().Stop ();
			playerSounds.clip = playerDie;
			playerSounds.Play ();
			// Detenemos el sistema de partículas
			WaterStop ();
			// Cancelación del incremento de velocidad
			gameController.SendMessage ("ResetDifficulty");
		}
		// Colisión con el marcador de puntos y estado es jugando
		else if ((col.gameObject.tag == "Points") && 
			(gameController.GetComponent<Viaje_Minijuego_GameController>().gameState ==
				Viaje_Minijuego_GameController.GameState.Playing)){
			// Incrementamos la puntuación
			gameController.SendMessage ("IncreasePoints");
			// Sonido de punto conseguido
			playerSounds.clip = playerPoint;
			playerSounds.Play ();
		}
	}

	// Actualización de estado a listo para reinicio 
	void ReadyRestart(){
		gameController.GetComponent<Viaje_Minijuego_GameController> ().gameState =
			Viaje_Minijuego_GameController.GameState.Ready;
	}

	// Método para arrancar el sistema de particulas
	void WaterPlay(){
		waterParticle.Play ();
	}

	// Método para detener el sistema de partículas
	void WaterStop(){
		waterParticle.Stop ();
	}
}
