using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _collectableText;
    void Start()
    {
        _collectableText.text = "Collectables: 0"; 
    }

    public void UpdateCollectables(int collectables)
    {
        _collectableText.text = "Collectables: " + collectables.ToString();
    }
}
