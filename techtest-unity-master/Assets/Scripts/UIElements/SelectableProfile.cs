using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectableProfile : MonoBehaviour
{
    public Text Name;
    public Text Money;

    public Image selectionBackground;

    public Color selected;
    public Color deselected;

    PlayerData _player;

    public delegate void OnSelectedAction(SelectableProfile playerData);
    public event OnSelectedAction OnSelected;

    public void Initialize(PlayerData player, OnSelectedAction onClick)
    {
        _player = player;
        Name.text = _player.name;
        Money.text = "$"+_player.coins;
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
