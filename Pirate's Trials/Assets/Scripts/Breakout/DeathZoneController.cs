using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneController : MonoBehaviour {

	// Instancia de controladores
	public BallController ballController;
	public BreakoutUIController breakoutUIController;
	// Objetos 
	public GameObject ball;
	public GameObject racket;
	// Componente Rigidbody2D correspondiente a la bola
	private Rigidbody2D rbBall;
	// Variables para sonidos 
	private AudioSource ballSounds;
	public  AudioClip ballDeath;

	void Start(){
		// Recuperación de componentes
		rbBall = ball.GetComponent<Rigidbody2D> ();
		ballSounds = GetComponent<AudioSource> ();
	}

	// Método para verificación de colisión
	void OnTriggerEnter2D(Collider2D col){
		// Colisión con elemento bola
		if (col.tag == "Ball"){
			// Se posiciona sobre la raqueta
			ball.transform.position = new Vector3(racket.transform.position.x,
				racket.transform.position.y+0.35f,racket.transform.position.z);
			// Se inicializa la velocidad de la bola
			ball.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			// Se activa el componente cinemático para que se mantenga pegada a la raqueta
			rbBall.isKinematic = true;
			// Estado de juego no jugando
			ballController.isPlaying = false;
			// Se reduce 1 vida
			breakoutUIController.SendMessage ("DecreaseLives");
			// Activación de sonido de muerte
			ballSounds.clip = ballDeath;
			ballSounds.Play ();
		}
	}
}
