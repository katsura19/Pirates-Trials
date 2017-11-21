using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordController : MonoBehaviour {

	// Objeto de tipo Text para mostrar en pantalla el valor del record
	public Text recordText;
	// Variable de almacenamiento del record
	private int points = 0;

	// Use this for initialization
	void Start () {
		// Obtención del record a través del método
		points = GetMaxPoints ();
		// Mostramos por pantalla el valor del record
		recordText.text = points.ToString ();
	}

	// Método para devolver la puntuación máxima
	public int GetMaxPoints(){
		// Devuelve el valor almacenado en History, y por defecto se muestra 0
		return PlayerPrefs.GetInt ("History", 0);
	}
}
