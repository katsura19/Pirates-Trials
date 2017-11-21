using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// Variable de velocidad
	public float velocity = 3f;

	// Variable para el componente Rigidbody2D del enemigo
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		// Recuperación del componente Rigidbody2D
		rb2d = GetComponent<Rigidbody2D> ();
		// Indicamos la velocidad del Rigidbody mediante un vector dirigido a la izquierda
		rb2d.velocity = Vector2.left * velocity;
	}

	// Controlamos la colisión con el Trigger Destroyer
	// Sobreescribimos el método ya existente OnTriggerEnter2D
	//indicando con quién colisiona mediante el Collider2D
	void OnTriggerEnter2D(Collider2D objectCollider){
		// Verificamos si colisiona con el destructor mediante el tag
		if (objectCollider.gameObject.tag == "Destroyer") {
			// Destruimos el objeto enemigo
			Destroy (gameObject);
		}
	}
}
