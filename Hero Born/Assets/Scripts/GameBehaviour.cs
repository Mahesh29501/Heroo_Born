using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public Button LossButton;
    public Button WinButton;
    public int MaxItems = 4;
    public Text HealthText;
    public Text ItemText;
    public Text ProgressText;

    private void Start()
    {
        ItemText.text += _itemcollected;
        HealthText.text += _playerHP;
    }

    public void UpdatedScene(string UpdatedText)
    {
        ProgressText.text = UpdatedText;
        Time.timeScale = 0f;
    }

    private int _itemcollected = 0;

    public int Items
    {
        get 
        { return _itemcollected;}
        set 
        { _itemcollected = value;
            ItemText.text = "Item Collected:  " + Items;

            if(MaxItems<= _itemcollected)
            {
                WinButton.gameObject.SetActive(true);
                UpdatedScene("You've found all the items");

            }

            else
            {
                ProgressText.text = "Found item, only " + (MaxItems - _itemcollected) + " more to go!";
            }

        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private int _playerHP = 10;

    public int Health { 
        get 
        { 
            return _playerHP; 
        }
        set 
        { 
            _playerHP = value;

            HealthText.text = "Player Health: " + Health;
            if (_playerHP <= 0)
            {
                LossButton.gameObject.SetActive(true);
                UpdatedScene("You've Lost!");
            }
            else
            {
                ProgressText.text = "Oouch... that's gonna hurt";
            }
            Debug.Log($"Player HP {_playerHP}");
        }
    
    
    }
}

