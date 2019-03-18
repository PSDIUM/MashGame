using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	[SerializeField] private GameObject textBox;
	private Text dialogue;

	private static DialogueManager _instance;
	public static DialogueManager Instance{ get { return _instance; } } 

	void Awake(){
		if(_instance!=null & _instance!=this){
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	void Start () {
		dialogue = textBox.transform.Find("Text").GetComponent<Text>();
		textBox.SetActive(false);
	}

	public void SetDialogue(string text){
		textBox.SetActive(true);
		dialogue.text = text;
	}
}
