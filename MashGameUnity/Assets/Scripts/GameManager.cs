﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	[SerializeField] GameObject soldierPrefab;
	[SerializeField] Vector3[] soldierStartPos;
	[SerializeField] private int morale;
	[SerializeField] private HealthBar moraleBar;
	[SerializeField] private Text scoreText;
	[SerializeField] private GameObject player;
	private int maxMorale;
	private float score;
	private int currentInjuredSoldiers;
	private int totalInjuredSoldiers;
	private float timer;
	private float soldierDeathModifier = 0;
	private GameObject soldiers;

	public static GameManager Instance { get { return _instance; } }
	public float Score { get { return score; } set { score = value; } }
	public int TotalInjuredSoldiers {get {return totalInjuredSoldiers; } }
	public int CurrentInjuredSoldiers {get {return currentInjuredSoldiers; } }
	public float SoldierDeathModifier { get { return soldierDeathModifier; } }
	public int Morale { get { return morale; } }



	void Awake(){
		if(_instance !=null && _instance != this){
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	void Update(){
		GameTimer();
		ResetGame();
	}

	private void GameTimer(){
		timer += Time.deltaTime;
	}

	void Start(){
		soldiers = GameObject.Find("Soldiers");
		totalInjuredSoldiers =  soldiers.transform.childCount;
		currentInjuredSoldiers = totalInjuredSoldiers;	
		maxMorale = morale;

		soldierStartPos = new Vector3[soldiers.transform.childCount];
		for(int x=0; x<soldiers.transform.childCount; x++){
			soldierStartPos[x] = soldiers.transform.GetChild(x).transform.position;
		}
	}

	public void RemoveInjuredSoldier(int amount){
		currentInjuredSoldiers-= amount;
		if(currentInjuredSoldiers<=0){
			SpawnSoldiers();
		}
	}

	public void DecreaseMorale(){
		morale -= 10;
		DialogueManager.Instance.SetDialogue("Dammit, soldiers are dying out there what are you doing!?");
		float percentage = (float)morale/(float)maxMorale;
		moraleBar.SetSize(percentage);

		if(morale<=0){
			LoseGame();
		}
	}

	public void CalculateScore(int dropOff){
		int scoreGain = (int)(25*dropOff*timer);
		score+=scoreGain;
		scoreText.text = "Score: " + score;
		Debug.Log("Score: " + score);
	}

	private void SpawnSoldiers(){
		Debug.Log("Spawning Soldiers");

		if(soldierDeathModifier < 5){
			soldierDeathModifier += 0.1f;
		}
		for(int x=0; x<soldierStartPos.Length; x++){
			GameObject soldier = Instantiate(soldierPrefab, soldierStartPos[x], Quaternion.identity);
			soldier.transform.parent = soldiers.transform;
		}
		currentInjuredSoldiers = totalInjuredSoldiers;
	}

	public void WinGame(){
		Debug.Log("You collected all the soldiers!");
	}

	public void LoseGame(){
		DialogueManager.Instance.SetDialogue("Game Over! You got yourself killed soldier!");
	}

	public void ResetGame () {
		if (Input.GetKeyDown (KeyCode.R)) {
			score = 0;
			scoreText.text = "Score: " + score;
			morale = maxMorale;

			if (player != null) {
				Destroy (player);
			}
			GameObject newPlayer = Instantiate (Resources.Load ("Prefabs/Helicopter") as GameObject);
			player = newPlayer;

			foreach (Transform soldier in GameObject.Find("Soldiers").transform) {
				Destroy (soldier.gameObject);
			}
			for (int x = 0; x < soldierStartPos.Length; x++) {
				GameObject soldier = Instantiate (soldierPrefab, soldierStartPos [x], Quaternion.identity);
				soldier.transform.parent = soldiers.transform;
			}

			DialogueManager.Instance.DisableDialogue ();
		}
	}
}
