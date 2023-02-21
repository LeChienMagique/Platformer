using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser: MonoBehaviour {
	public string     filename;
	public GameObject rockPrefab;
	public GameObject brickPrefab;
	public GameObject questionBoxPrefab;
	public GameObject stonePrefab;
	public Transform  environmentRoot;

	// --------------------------------------------------------------------------
	void Start() {
		LoadLevel();
	}

	// --------------------------------------------------------------------------
	void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			ReloadLevel();
		}
	}

	private GameObject GetCorrespondingBlock(char letter) =>
		letter switch {
			'b' => brickPrefab,
			'?' => questionBoxPrefab,
			'x' => rockPrefab,
			's' => stonePrefab,
			_   => throw new ArgumentOutOfRangeException(nameof(letter), letter, null)
		};

	// --------------------------------------------------------------------------
	private void LoadLevel() {
		string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
		Debug.Log($"Loading level file: {fileToParse}");

		Stack<string> levelRows = new Stack<string>();

		// Get each line of text representing blocks in our level
		using (StreamReader sr = new StreamReader(fileToParse)) {
			string line;
			while ((line = sr.ReadLine()) != null) {
				levelRows.Push(line);
			}

			sr.Close();
		}

		int row = 0;
		// Go through the rows from bottom to top
		while (levelRows.Count > 0) {
			string currentLine = levelRows.Pop();

			char[] letters = currentLine.ToCharArray();
			for (int column = 0 ; column < letters.Length ; column++) {
				char letter = letters[column];
				if (letter == ' ') continue;
				// Todo - Instantiate a new GameObject that matches the type specified by letter
				GameObject block = Instantiate(GetCorrespondingBlock(letter));
				// Todo - Position the new GameObject at the appropriate location by using row and column
				block.transform.position = new Vector3(0.5f + column, 0.5f + row, 0);
				// Todo - Parent the new GameObject under levelRoot
				block.transform.SetParent(environmentRoot, worldPositionStays: true);
			}
			row++;
		}
	}

	// --------------------------------------------------------------------------
	private void ReloadLevel() {
		foreach (Transform child in environmentRoot) {
			Destroy(child.gameObject);
		}
		LoadLevel();
	}
}