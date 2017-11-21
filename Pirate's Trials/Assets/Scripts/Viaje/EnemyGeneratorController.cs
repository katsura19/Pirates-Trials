using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour {

	// Variable para recuperar los prefab de los enemigos
	public GameObject enemyPrefab;

	// Variable para indicar el tiempo entre generación de enemigos
	public float generatorTime = 2f;

	// Método para la generación de enemigos
	void GenerateEnemy(){
		// Instanciamos el prefab, con la posición actual del generador,
		//y mantenemos su propiedad Quaternion, referente a rotación 
		Instantiate (enemyPrefab, transform.position, Quaternion.identity);
	}

	// Iniciamos la generación de enemigos
	public void StartGenerate(){
		// Invocamos el método GenerateEnemy, con retardo de inicio 0,
		// y repitiendo cada generatorTime
		InvokeRepeating ("GenerateEnemy", 0f, generatorTime);
	}

	// Finalizamos la generación de enemigos
	public void FinishGenerate(){
		// Cancelamos la invocación del método GenerateEnemy
		CancelInvoke ("GenerateEnemy");
	}
}
