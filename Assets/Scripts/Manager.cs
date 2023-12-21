using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public enum HearingType
    {
        Normal,
        HearingLoss,
        Deaf
    }


    public static Manager Instance;
    [SerializeField] private int _randomSeed = 42; // Seed for randomization, can be changed in the inspector
    public int RandomSeed => _randomSeed;

    private HearingType _currentType = HearingType.Normal;
    public HearingType CurrentType => _currentType;

    [SerializeField] private PlayerCam _playerCam;
    [SerializeField] private GameObject _endWindow;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _currentType = GetPlayerPrefs();

        Debug.Log(_currentType);
        Random.InitState(_randomSeed);
    }
    public HearingType GetPlayerPrefs()
    {
        string type = PlayerPrefs.GetString("HearingType");

        switch (type)
        {
            case "Normal":
                return HearingType.Normal;
            case "HearingLoss":
                return HearingType.HearingLoss;
            case "Deaf":
                return HearingType.Deaf;
            default:
                // Handle unexpected cases
                return HearingType.Normal; // or throw an exception
        }
    }
    public string GetTypeOfHearing()
    {
        switch (_currentType)
        {
            case HearingType.Normal:
                return "Normal";
            case HearingType.HearingLoss:
                return "HearingLoss";
            case HearingType.Deaf:
                return "Deaf";
            default:
                return null;
        }
    }

    public void EnableEndWindow()
    {
        _playerCam.LockCam();
        _endWindow.SetActive(true);
    }


}
