using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	public GameObject[] Planets;
	//queue pra "segurar" os planetas
	Queue<GameObject> availablePlanets = new Queue<GameObject>();
	
	// Use this for initialization
	void Start () {
		//add os planetas para o queue
		availablePlanets.Enqueue(Planets[0]);
		availablePlanets.Enqueue(Planets[1]);
		availablePlanets.Enqueue(Planets[2]);

		//chama a função para descer os planetas
		InvokeRepeating("MovePlanetDown", 0, 20f);
	}

	// Update is called once per frame
	void Update () {

	}

	//função para dequeue um planeta, e definir sua "isMoving" tag para true, assim ele começa a descer a tela
	void MovePlanetDown () {

		EnqueuePlanets();

		//se a queue estiver vazia, retorna
		if (availablePlanets.Count == 0)
			return;

		//pegar um planeta da queue
		GameObject aPlanet = availablePlanets.Dequeue();

		//define a tag para movendo
		aPlanet.GetComponent<Planet>().isMoving = true;
	}

	//função para enqueue os planetas que estão abaixo da tela e não estão se movendo
	void EnqueuePlanets () {
		foreach (GameObject aPlanet in Planets) {
			//se o planeta estiver abaixo da tela
			if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving)) {
				//reseta a posição do planeta
				aPlanet.GetComponent<Planet>().ResetPosition();

				//enqueue o planeta
				availablePlanets.Enqueue(aPlanet);
			}
		}
	}
}