using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Instancia del controlador de juego
	public BreakoutUIController breakoutUIController;
	// Variable para nuestro objeto bola
	public GameObject ball;
	// Velocidad por defecto de la bola
	public float ballSpeed = 4f;
	// Variables para sonidos 
	private AudioSource ballSounds;
	public  AudioClip ballImpact;
	public  AudioClip brickCrash;
	public  AudioClip ballDeath;
	// Indicador de estado jugando, por defecto desactivado
	public bool isPlaying = false;

	// Use this for initialization
	void Start () {
		// Recuperamos el componente para los sonidos
		ballSounds = GetComponent<AudioSource> ();
	}

	void Update () {
		// Si el estado de juego no es jugando y se pulsa espacio, ratón, o pantalla
		if ((isPlaying == false) && (Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0))){
			// Pasamos el estado de juego a jugando
			isPlaying = true;
			// Se actualiza el componente Rigidbody2D para que no se cinemático y la bola se desplace
			GetComponent<Rigidbody2D> ().isKinematic = false;
			// Se actualiza el componente Rigidbody2D haciendo que tenga una velocidad indicada
			// por el factor ballSpeed dirigida hacia arriba
			GetComponent<Rigidbody2D> ().velocity = 
				Vector2.up * ballSpeed;
		}
	}

	//  Función para obtener el factor de colisión entre bola y raqueta
	// Como parámetro se introducen la posición de la bola, la de la raqueta, y el ancho de ésta
	float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth){
		return (ballPos.x - racketPos.x) / racketWidth;
	}

	// Colisión entre bola y objetos
	void OnCollisionEnter2D(Collision2D col){
		// Se verifica el impacto contra la raqueta
		if (col.gameObject.name == "Racket") {
			// Calculamos el factor de impacto para la desviación
			float x = hitFactor (transform.position,
				          col.transform.position,
				          col.collider.bounds.size.x);
			// Calculamos la dirección marcando la longitud a 1
			Vector2 dir = new Vector2 (x, 1).normalized;
			// Se establece la velocidad como dirección * speedBall
			GetComponent<Rigidbody2D> ().velocity = 
				dir * (ballSpeed * 1.2f);
			// Sonido de impacto
			ballSounds.clip = ballImpact;
			ballSounds.Play ();
		}
		// Impacto contra bloque destruible
		else if (col.gameObject.tag == "Brick") {
			// Reducimos los puntos
			breakoutUIController.SendMessage ("DecreasePoints");
			// Sonido de impacto contra bloque que se destruye
			ballSounds.clip = brickCrash;
			ballSounds.Play ();
		}
		// Impacto contra márgenes de juego
		else if (col.gameObject.tag == "Breakout_Border") {
			// Sonido de impacto
			ballSounds.clip = ballImpact;
			ballSounds.Play ();
		}
		// Impacto contra bloque no destruible
		else if (col.gameObject.tag == "Brick_not_breakable") {
			// Sonido de impacto
			ballSounds.clip = ballImpact;
			ballSounds.Play ();
		} 
	}
}
