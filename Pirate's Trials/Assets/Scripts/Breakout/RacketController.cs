using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RacketController : MonoBehaviour {

	// Velocidad por defecto de la raqueta
	public float racketSpeed = 5f;
	// Variable para la dirección
	public float directionX;
	// Componente Rigidbody2D de la raqueta
	private Rigidbody2D rb2d;

	void Start(){
		// Recuperación del componente
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		// Dirección de la raqueta en el eje X
		directionX = CrossPlatformInputManager.GetAxis ("Horizontal");
		// Velocidad de la raqueta
		rb2d.velocity = new Vector2 (directionX * racketSpeed, 0);
	}
		
}
