using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour {
	Text scoreTextUI;

	int score;

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			UpdateScoreTextUI();
		}
	}

	// Use this for initialization
	void Start () {
		//pega o texto UI deste GO
		scoreTextUI = GetComponent<Text>();
	}

	//função para atualizar o score
	void UpdateScoreTextUI () {
		string scoreStr = string.Format("{0:00000}", score);
		scoreTextUI.text = scoreStr;
	}
}
