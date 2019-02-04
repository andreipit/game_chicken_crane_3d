using UnityEngine;

public class UserModel : MonoBehaviour {

    public static UserModel self;
    const string recordField = "record";

    void Awake()
    {
        self = this;
    }

    public int GetRecord()
    {
        return PlayerPrefs.GetInt(recordField);
    }

    public void SetRecord(int value)
    {
        PlayerPrefs.SetInt(recordField, value);
    }
}
