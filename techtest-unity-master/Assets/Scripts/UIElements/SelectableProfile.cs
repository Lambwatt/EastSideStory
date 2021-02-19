using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectableProfile : MonoBehaviour
{
    public Text playerName;
    public Text money;

    public Image selectionBackground;

    public Color selected;
    public Color deselected;

    PlayerData _player;

    public delegate void OnSelectedAction(SelectableProfile playerData);
    public event OnSelectedAction OnSelected;

    public void Initialize(PlayerData player, OnSelectedAction onClick)
    {
        _player = player;
        playerName.text = _player.name;
        money.text = "$"+_player.coins;
        OnSelected = onClick;
    }

    public void Select()
    {
        selectionBackground.color = selected;
        OnSelected(this);
    }

    public void Deselect()
    {
        selectionBackground.color = deselected;
    }

    public PlayerData GetPlayer()
    {
        return _player;
    }
}
