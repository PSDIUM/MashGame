using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

	[SerializeField] private float maxHealth;
	[SerializeField] private int maxPassengers;
	private float health;
	private int soldierCount;
	private Vector3 startPos;

	public float Health { get { return health; } set { health = value; } }
	public int MaxPassengers { get { return maxPassengers; } }
	public int SoldierCount { get { return soldierCount; } }

	void Start () {
		startPos = transform.position;
		health = maxHealth;
	}

	void Update () {
		Movement();
		Reset();
	}

	private void Movement(){
		float xInput = Input.GetAxis("Horizontal");
		float yInput = Input.GetAxis("Vertical");

		if(xInput!=0){
			transform.position +=  new Vector3(xInput * Time.deltaTime * 10, 0, 0);
		}
		if(yInput!=0){
			transform.position +=  new Vector3(0, yInput * Time.deltaTime * 10, 0);
		}
	}

	public void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag.Equals("Tree")){
			DamageHelicopter();
		}
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag.Equals("Soldier")){
			Debug.Log("Picking up solider");
			PickupSoldier(col.gameObject);
		} 	
		if(col.gameObject.tag.Equals("Hospital")){
			DropSoldiers();
		}
	}

	private void PickupSoldier(GameObject soldier){
		if(soldierCount<maxPassengers){
			soldierCount++;
			Destroy(soldier);
		} else {
			Debug.Log("Helicoper is full");
		}
	}

	private void DropSoldiers(){
		if(soldierCount>0){
			Debug.Log(soldierCount + " Soldiers have been dropped off");
			GameManager.Instance.CalculateScore(soldierCount);
			soldierCount = 0;
		}
	}

	private void DamageHelicopter(){
		health-=25;
		Debug.Log("Helicopter has taken damage!");
		if(health<=0){
			GameManager.Instance.LoseGame();
			Explode();
		}
	}

	private void Explode(){
		Destroy(this.gameObject);
	}

	private void Reset(){
		if(Input.GetKeyDown(KeyCode.R)){
			transform.position = startPos;
			health = maxHealth;
			soldierCount = 0;
			GameManager.Instance.ResetGame();
		}
	}
}
