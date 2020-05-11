using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
	public string resultText = "a";

	// Updates the child text object with the contents of "resultText".
    /*public void UpdateText()
	{
		gameObject.GetComponentInChildren<Text>().text = resultText;
	}*/
	
	void Update()
	{
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = resultText;
	}
}
