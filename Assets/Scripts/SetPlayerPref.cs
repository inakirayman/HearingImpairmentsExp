using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetPlayerPrefValue(string value)
    {
        PlayerPrefs.SetString("HearingType", value);
    }

    




}
