using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameManager : MonoBehaviour
{
    public static NameManager nameManager;

    public static string playerName;

    private GameObject nameInputGO;
    private TMP_InputField nameInput;

    private bool generateRandomInt = true;

    void Start()
    {
        if (nameManager != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            nameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }

        nameInputGO = GameObject.Find("/Canvas/NameSelection/NameText/WhiteLayer/NameInputText");
	}

    // Update is called once per frame
    void Update()
    {
        if (nameInputGO != null)
        {
            nameInput = nameInputGO.GetComponent<TMP_InputField>();
            if (!string.IsNullOrWhiteSpace(nameInput.text))
            {
                playerName = nameInput.text;
                generateRandomInt = true;
            }
            if (string.IsNullOrWhiteSpace(nameInput.text) && generateRandomInt)
            {
                int randomInt = Random.Range(0, 1000);
                playerName = $"Player{randomInt}";
                Debug.Log(randomInt);
                generateRandomInt = false;
            }
        }
    }

}
