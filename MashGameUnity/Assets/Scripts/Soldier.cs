using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

	[SerializeField]private float deathTime;
	private float bleedTime;

	void Start(){
		bleedTime = deathTime;
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

	private void Die(){
		Destroy(this.gameObject);
	}
}
