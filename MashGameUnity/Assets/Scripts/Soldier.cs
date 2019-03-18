using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

	[SerializeField]private float deathTime;
	[SerializeField]private HealthBar healthBar;
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
		UpdateBleedBar();
		if(bleedTime<=0){
			Die();
		}
	}

	private void UpdateBleedBar(){
		float percentage = bleedTime/deathTime;
		healthBar.SetSize(percentage);
	}

	private void Die(){
		GameManager.Instance.RemoveInjuredSoldier(1);
		GameManager.Instance.DecreaseMorale();
		Destroy(this.gameObject);
	}
}
