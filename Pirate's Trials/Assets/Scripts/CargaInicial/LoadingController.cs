using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour {

	// Objeto slider que simula ser una barra de carga
	Slider loadingBar;
	// Contador para el relleno de la barra de slider
	int cont = 0;

	void Awake(){
		// Obtenemos el componente correspondiente al slider mostrado en la escena
		loadingBar = GetComponent<Slider> ();
	}
		
	void Start(){
		// Activamos la barra de carga
		LoadingBar ();
	}

	// Actualización del estado de la barra de carga mediante el slider
	// maxVal -> máximo valor que podrá tener
	// actualVal -> valor actual 
	void UpdateLoadingBar(float maxVal, float actualVal){
		// Porcentaje de la barra que se encuentra llena
		float percentage;
		// Obtención del porcentaje
		percentage = actualVal / maxVal;
		// Carga del valor de porcentaje en el slider
		loadingBar.value = percentage;
	}

	// Carga de la barra de slider
	void LoadingBar(){
		// Si el contador de la barra de slider no ha llegado al 100%
		if (cont <= 100) {
			//Actualizamos el valor del porcentaje de carga y lo mostramos
			UpdateLoadingBar (100, cont);
			//Aumentamos en 1 el contador de carga
			cont++;
		}
		// Si el contador ha llegado al 100%
		else {
			// Se carga la escena correspondiente al Título
			SceneManager.LoadScene ("Titulo");
		}
		// Autoinvocamos el método cada 0.05 segundos
		Invoke ("LoadingBar", 0.05f);
	}
}
