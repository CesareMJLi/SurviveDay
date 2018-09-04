using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;


public class ButtonState : MonoBehaviour {

	public GameObject sceneState;
	public GameObject currentCanvas;

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>() as Button;
		button.onClick.AddListener(btClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void btClick(){
		sceneState.SetActive(true);
		currentCanvas.SetActive(false);
		Time.timeScale=0;
	}
}
