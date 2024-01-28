using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTextAssigner : MonoBehaviour
{
    public Text nameText;

    void Start()
    {
        nameText.text = NameManager.playerName;
    }

}
