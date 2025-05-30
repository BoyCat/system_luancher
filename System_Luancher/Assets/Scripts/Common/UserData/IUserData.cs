using UnityEngine;

public interface IUserData
{
    void SetDefaultData();

    bool SaveData();
    bool LoadData();    
}
