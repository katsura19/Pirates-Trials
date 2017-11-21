using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {
	// Método para detectar colisiones
	void OnCollisionEnter2D(Collision2D col){
		// Se destruye el objeto
		Destroy (gameObject);
	}
}
