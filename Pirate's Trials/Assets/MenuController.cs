using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	// Objeto que contiene al botón de salida de la aplicación
	public GameObject buttonExit;

	// Use this for initialization
	void Start () {
		// Si la plataforma de ejecución es Android se desactivará el botón de salida
		if (Variables_Globales.variables_Globales.currentPlatformAndroid == true){
			buttonExit.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
