﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	public Transform buttonPrefab;
	public Transform buttonPanel;
	public Transform[] robotPrefabs;
	public Sprite[] buttonSprites;
	public Vector3Int spawnLoc;
	public FollowCamera charCam;

	private Transform currChar;
	private bool charIsScientist;
	private LevelManager levelManager;
	private RectTransform panelPos;
	

	void Awake() {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		Debug.Assert(levelManager != null, "Warning: Level Manager script not found in scene!");
		Debug.Assert(robotPrefabs.Length == buttonSprites.Length, "Warning: Spawner prefabs and sprites lists don't match!");
		panelPos = buttonPanel.GetComponent<RectTransform>();
		currChar = null;
		charIsScientist = false;
	}

	void Start() {
		// create a UI spawn button for each robot prefab
		for (int i = 0; i < robotPrefabs.Length; i++) {
			Transform buttonTransform = Instantiate(buttonPrefab, buttonPanel);
			Transform robotToSpawn = robotPrefabs[i];
			buttonTransform.GetComponent<Button>().onClick.AddListener(delegate{spawn(robotToSpawn);});
			buttonTransform.GetComponent<Image>().sprite = buttonSprites[i];
		}
	}

	public void spawn(Transform robotPrefab) {
		if (currChar == null) { // make sure you can't press the button multiple times
			panelPos.anchoredPosition += Vector2.down * 180f;
			currChar = Instantiate(robotPrefab, spawnLoc, Quaternion.identity, levelManager.transform);
			CubeObject charCubeObj = currChar.GetComponent<CubeObject>();
			// we'll need this bool to check later because the actual object will have been destroyed
			charIsScientist = charCubeObj is Scientist;
			levelManager.addBlock(charCubeObj);
			charCam.follow(currChar);
		}
	}

	public void onCharacterDeath() {
		if (charIsScientist) {
			//display game over + restart
			Debug.Log("Game Over");
		} else {
			// reset spawn button panel into view
			panelPos.anchoredPosition = Vector2.zero;
		}
	}

}
