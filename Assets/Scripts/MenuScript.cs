using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button _start;

    [SerializeField] private Button _quit;


    private void Start()
    {
        _start.onClick.AddListener(OnStartClick);
        _quit.onClick.AddListener(OnQuitClick);
    }

    private void OnStartClick()
    {
        LevelController.Instance.ResetStats();
        LevelController.Instance.ChangeScene("Tutorial");
    }

    private void OnQuitClick()
    {
        Application.Quit();
    }
}
