using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Star : MonoBehaviour {

	public float speed; //velocidade em que a estrela passa pela tela

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//pega a posição da estrela
		Vector2 position = transform.position;

		//calcula a nova posição da estrela
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		//atualiza a posição da estrela
		transform.position = position;

		//parte de baixo-esquerda da tela
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		//parde te cima-direita da tela
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//se a estrela sai da tela por baixo ela é jogada para a perte de cima, randomicamente no eixo x
		if (transform.position.y < min.y) {
			transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
		}
	}
}
