using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaIslaController : MonoBehaviour {

	// Variable para carga de nivel aleatorio
	private int nivelAleatorio = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Si se pulsa espacio se carga el nivel aleatorio
		if (Input.GetKeyDown ("space")){
			NivelAleatorio ();
		}
	}

	// Metodo para carga de nivel aleatorio
	public void NivelAleatorio(){
		// Se obtiene valor aleatorio entre 1 y 5
		nivelAleatorio = Random.Range (1, 6);
		// Evaluación de valor aleatorio para carga de nivel
		switch (nivelAleatorio)
		{
		case 1:
			SceneManager.LoadScene ("Breakout"); 
			break;
		case 2:
			SceneManager.LoadScene ("Vida_Gratis"); 
			break;
		case 3:
			SceneManager.LoadScene ("Breakout"); 
			break;
		case 4:
			SceneManager.LoadScene ("Vida_Gratis"); 
			break;
		case 5:
			SceneManager.LoadScene ("Breakout"); 
			break;
		default:
			// Si no es ningún valor válido se cargará la pantalla de error
			SceneManager.LoadScene ("Pantalla_Error"); 
			break;
		}
	}
}
