using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksController : MonoBehaviour {

	// Objetos bloque y bloque no destruible
	public GameObject brick;
	public GameObject brickNotBreakable;

	// Contador de bloques en pantalla
	public static int numeroBloques = 0;

	// Use this for initialization
	void Start () {
		// Inicialización de número de bloques
		numeroBloques = 0;
		// Creación de bloques
		CreateBricks ();
	}

	// Método para la creación aleatoria de bloques
	void CreateBricks(){
		// Valor aleatorio para la creación de bloques destruibles
		// Valores posibles y número de bloques creados:
		// 4 --> 9 bloques
		// 3 --> 7 bloques
		// 2 --> 5 bloques
		// 1 --> 3 bloques
		// 0 --> 1 bloque
		int randomBricks1 = Random.Range (0, 5);
		for (var i = -randomBricks1; i < (randomBricks1 + 1); i++) {	
			// Instancia del prefab en las posiciones dadas por el valor aleatorio
			Instantiate (brick, new Vector3 (i*1f, 2.5f, 0), Quaternion.identity);
			// Se incrementa el valor del número de bloques en pantalla
			numeroBloques++;
		}
		// Valor aleatorio para la creación de bloques destruibles
		int randomBricks2 = Random.Range (0, 4);
		// Creación de bloques en bucle desde el valor obtenido con signo negativo, hasta su valor + 1
		for (var i = -randomBricks2; i < (randomBricks2 + 1); i++) {	
			// Instancia del prefab en las posiciones dadas por el valor aleatorio
			Instantiate (brick, new Vector3(i*1f, 0.9f, 0), Quaternion.identity);
			// Se incrementa el valor del número de bloques en pantalla
			numeroBloques++;
		}
		// Valor aleatorio para la creación de bloques no destruibles
		int randomBlocks1 = Random.Range (0, 3);
		// Creación de bloques en bucle desde el valor obtenido con signo negativo, hasta su valor + 1
		for (var i = -randomBlocks1; i < (randomBlocks1 + 1); i++) {
			// Instancia del prefab en las posiciones dadas por el valor aleatorio
			Instantiate (brickNotBreakable, new Vector3 (i * 1.7f + 0.1f, 1.7f, 0), Quaternion.identity);
		}
		// Valor aleatorio para la creación de bloques no destruibles
		int randomBlocks2 = Random.Range (0, 2);
		// Creación de bloques en bucle desde el valor obtenido con signo negativo, hasta su valor + 1
		for (var i = -randomBlocks2; i < (randomBlocks2 + 1); i++) {
			// Instancia del prefab en las posiciones dadas por el valor aleatorio
			Instantiate (brickNotBreakable, new Vector3(i * 1.7f - 0.1f, 0.1f, 0), Quaternion.identity);
		}
	}
}
