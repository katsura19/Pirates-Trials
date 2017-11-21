using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour {

	// Método para cambio de escena
	// nombre --> escena a cargar
	public void CambiarEscena(string nombre){
		SceneManager.LoadScene (nombre);
	}

	// Método de sálida de la aplicación
	public void SalirJuego(){
		Application.Quit ();
	}
}
