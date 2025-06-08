using System;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerPref : MonoBehaviour
{
    [SerializeField] private string name;

    private void Update()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt(name) + "";
    }
}
