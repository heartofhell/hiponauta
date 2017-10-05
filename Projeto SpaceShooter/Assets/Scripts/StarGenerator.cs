using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour {

	public GameObject StarGO; //stars prefab
	public int maxStars; // the max of stars

	//array of colors
	Color[] starColors = {
		new Color(0.5f, 0.5f, 1f), //blue
		new Color(0, 1f, 1f), //green
		new Color(1f, 1f, 0), //yellow
		new Color(1f, 0, 0), //red
	};

	// Use this for initialization
	void Start () {
		//parte de baixo-esquerda da tela
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		//parde te cima-direita da tela
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//loop para criar as estrelas
		for (int i = 0; i < maxStars; ++i) {
			GameObject star = (GameObject)Instantiate(StarGO);

			//define a cor da estrela
			star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

			//define a posição da estrela
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

			//define a velocidade da estrela
			star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

			//faz das estrelas "child" de StarGeneratorGO
			star.transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
