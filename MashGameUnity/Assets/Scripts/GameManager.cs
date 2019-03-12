using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	[SerializeField] GameObject soldierPrefab;
	[SerializeField] Vector3[] soldierStartPos;
	private float score;
	private int currentInjuredSoldiers;
	private int totalInjuredSoldiers;

	public static GameManager Instance { get { return _instance; } }
	public float Score { get { return score; } set { score = value; } }
	public int InjuredSoldiers {get {return totalInjuredSoldiers; } }
	public int CurrentInjuredSoldiers {get {return currentInjuredSoldiers; } }

	void Awake(){
		if(_instance !=null && _instance != this){
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	void Start(){
		GameObject soldiers = GameObject.Find("Soldiers");
		totalInjuredSoldiers =  soldiers.transform.childCount;
		currentInjuredSoldiers = totalInjuredSoldiers;	

		soldierStartPos = new Vector3[soldiers.transform.childCount];
		for(int x=0; x<soldiers.transform.childCount; x++){
			soldierStartPos[x] = soldiers.transform.GetChild(x).transform.position;
		}
	}

	public void CalculateScore(int dropOff){
		int scoreGain = 25*dropOff;
		score+=scoreGain;
		Debug.Log("Score: " + score);

		currentInjuredSoldiers-=dropOff;
		if(currentInjuredSoldiers<=0){
			WinGame();
		}
	}

	public void WinGame(){
		Debug.Log("You collected all the soldiers!");
	}

	public void LoseGame(){
		Debug.Log("Game Over!");
	}

	public void ResetGame(){
		score = 0;
		foreach(Transform soldier in GameObject.Find("Soldiers").transform){
			Destroy(soldier.gameObject);
		}
		for(int x=0; x<soldierStartPos.Length; x++){
			Instantiate(soldierPrefab, soldierStartPos[x], Quaternion.identity);
		}
	}
}
