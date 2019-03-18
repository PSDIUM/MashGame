using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

	[SerializeField]private float deathTime;
	private float bleedTime;

	void Start(){
		float modifier = GameManager.Instance.SoldierDeathModifier;
		bleedTime = deathTime - modifier;
		Debug.Log(bleedTime);
	}

	void Update(){
		Bleeding();
	}

	private void Bleeding(){
		bleedTime-= Time.deltaTime;
		if(bleedTime<=0){
			Die();
		}
	}

	private void TestMethod(){
		
	}

	private void Die(){
		GameManager.Instance.RemoveInjuredSoldier(1);
		GameManager.Instance.DecreaseMorale();
		Destroy(this.gameObject);
	}
}
