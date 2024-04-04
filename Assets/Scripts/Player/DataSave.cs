using UnityEngine;

public class DataSave : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void Save()
    {
        PlayerPrefs.SetInt("currentMoney", _player.Money);
    }
}