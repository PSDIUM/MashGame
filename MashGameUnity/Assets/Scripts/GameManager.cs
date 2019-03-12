using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	private float score;

	public static GameManager Instance { get { return _instance; } }
	public float Score { get { return score; } set { score = value; } }

	void Awake(){
		if(_instance !=null && _instance != this){
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	public void CalculateScore(int dropOff){
		int scoreGain = 25*dropOff;
		score+=scoreGain;
		Debug.Log("Score: " + score);
	}

	private void WinGame(){

	}

	private void LoseGame(){

	}

	private void ResetGame(){

	}
}
