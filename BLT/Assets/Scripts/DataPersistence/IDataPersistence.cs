using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data); // When we load the data, the implementing script only wants to read that data
    void SaveData(ref GameData data); // Pass by ref because when we save data, we want to allow the implementing script to modify the data


}
