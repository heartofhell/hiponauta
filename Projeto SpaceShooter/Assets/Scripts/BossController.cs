using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {
	GameObject scoreUITextGO; //referencia ao score
	public GameObject ExplosionGO;// prefab da explosão
	public int BossHP; //HP do Boss
	public int BossPoint; //Valor dos pontos do Boss
	public int HPLv2; //HP para entrar no Level 2
	public int HPLv3; //HP para entrar no Level 3   
	[HideInInspector]
	public bool StartLv2 = false;
	bool block = false; //Var para parar a descida do Boss


	float speed; //velocidade do inimigo

	public enum BossState {
		Lv1,
		Lv2,
		Lv3,
	}

	BossState BState;

	// Use this for initialization
	void Start () {
		speed = 1f; //definir velocidade

		//define o texto do score
		scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");

	}

	// Update is called once per frame
	void Update () {
		//False = O Boss continua descendo
		if (block == false) {
			Vector2 position = transform.position;

			//calcular a nova posição do Boss
			position = new Vector2(position.x, position.y - speed * Time.deltaTime);

			//atualizar a posição do Boss
			transform.position = position;

			//Boss para na posição Y 2.5
			if (position.y <= 2.5F) {
				block = true;
			}
		}

		//Se o jogador morrer o boss some
		GameObject playerShip = GameObject.Find("PlayerGO");

		if (playerShip == null) {
			Destroy(gameObject);
		}
	}

	//função para alterar a dificuldade do boss
	void UpdateBossState () {
		switch (BState) {
			case BossState.Lv2:
				StartLv2 = true;
				break;
		}
	}

	//função para definir o GMState
	public void SetBossState (BossState state) {
		BState = state;
		UpdateBossState();
	}

	void OnTriggerEnter2D (Collider2D col) {
		//detecta a colisão com os tiros 
		if (col.tag == "PlayerBulletTag") {
			//HP do Boss -1
			BossHP -= 1;

			//Se HP for Zerado
			if (BossHP <= 0) {
				//add X pontos pontos para o score
				scoreUITextGO.GetComponent<GameScore>().Score += BossPoint;

				PlayExplosion();

				//destrói a nave inimiga
				Destroy(gameObject);
			}

			//Se HP For menor que a Variavel HPLv2
			if (BossHP <= HPLv2) {
				BState = BossState.Lv2;
			}
		}
	}

	//função para iniciar a explosão
	void PlayExplosion () {
		GameObject explosion = (GameObject)Instantiate(ExplosionGO);

		//definir a posição da explosão
		explosion.transform.position = transform.position;
	}
}
