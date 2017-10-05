using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public float speed;
	public bool isMoving;

	Vector2 min;
	Vector2 max;

	void Awake () {
		isMoving = false;

		//parte de baixo-esquerda da tela
		min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		//parde te cima-direita da tela
		max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//add a sprite do planeta pela metade no max de y
		max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

		//subtrai a sprite do planeta pela metade no min de y
		max.y = max.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving)
			return;

		//pega a posição do planeta
		Vector2 position = transform.position;

		//calcula a nova pos do planeta
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		//atualiza a posição do planeta
		transform.position = position;

		//se o planeta chegar a parte de baixo da tela ele para de mover
		if (transform.position.y < min.y) {
			isMoving = false;
		}
	}

	//função para resetar a posição do planeta
	public void ResetPosition () {
		//reseta a posição do planeta no topo, com x aleatório
		transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
	}
}
