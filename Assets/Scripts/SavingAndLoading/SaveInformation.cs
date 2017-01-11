using UnityEngine;
using System.Collections;

public class SaveInformation
{

    public static void saveAllInformation()
    {
        PlayerPrefs.SetInt("PLAYERLEVEL", GameInformation.PlayerLevel);
        PlayerPrefs.SetInt("PLAYERLEVEL", GameInformation.PlayerLevel);
    }


}
