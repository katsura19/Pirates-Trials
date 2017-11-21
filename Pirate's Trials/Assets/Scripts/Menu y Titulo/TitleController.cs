using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Si se pulsa espacio se pasa a la pantalla de menú
		if (Input.GetKeyDown ("space")){
			SceneManager.LoadScene ("Menu");
		}
	}
}
