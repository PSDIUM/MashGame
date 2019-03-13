using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	[SerializeField] GameObject soldierPrefab;
	[SerializeField] Vector3[] soldierStartPos;
	[SerializeField] private int morale;
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
	}

	private void GameTimer(){
		timer += Time.deltaTime;
	}

	void Start(){
		soldiers = GameObject.Find("Soldiers");
		totalInjuredSoldiers =  soldiers.transform.childCount;
		currentInjuredSoldiers = totalInjuredSoldiers;	

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
		morale -= 25;
		Debug.Log("Dammit, soldiers are dying out there what are you doing!?");
		if(morale<=0){
			LoseGame();
		}
	}

	public void CalculateScore(int dropOff){
		int scoreGain = (int)(25*dropOff*timer);
		score+=scoreGain;
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
		Debug.Log("Game Over! You achieved a score of: " + score);
	}

	public void ResetGame(){
		score = 0;
		foreach(Transform soldier in GameObject.Find("Soldiers").transform){
			Destroy(soldier.gameObject);
		}
		for(int x=0; x<soldierStartPos.Length; x++){
			GameObject soldier = Instantiate(soldierPrefab, soldierStartPos[x], Quaternion.identity);
			soldier.transform.parent = soldiers.transform;
		}
	}
}
