using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TMP_InputField moveUp;
    private PlayerEntity player;
    public TMP_InputField jump;
    void Start()
    {
        player = FindObjectOfType<PlayerEntity>();
        // jump.
    }
}
