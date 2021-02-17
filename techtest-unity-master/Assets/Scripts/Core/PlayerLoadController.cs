using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLoadController : MonoBehaviour
{
    public Transform ProfileList;
    public Button LoadButton;
    public Button StartButton;
    public InputField Name;

    public SelectableProfile profilePrefab;

    PlayerDataList _playerList;

    SelectableProfile activeProfile;

    public delegate void OnLoadedAction(PlayerData playerData);
    public event OnLoadedAction OnLoaded;

    // Start is called before the first frame update
    void OnEnable()
    {
        LoadButton.interactable = false;
        StartButton.interactable = false;
        if (PlayerSaveManager.HasData())
        {
            _playerList = PlayerSaveManager.GetData();
            PopulateProfiles();
        }
        else
        {
            _playerList = new PlayerDataList();
        }
    }

    public void OnFieldPopulated(string contents)
    {
        StartButton.interactable = contents.Length > 0;
    }

    public void CreateNewPlayer()
    {
        OnLoaded(new PlayerData(Name.text));
        Close();
    }

    void PopulateProfiles()
    {
        SelectableProfile entry;
        foreach (PlayerData player in _playerList.players)
        {
            entry = Instantiate(profilePrefab);
            entry.transform.SetParent(ProfileList);
            entry.transform.position = new Vector3(0, 0, 0);
            entry.transform.localScale = new Vector3(1, 1, 1);

            entry.Initialize(player, OnClickProfile);
        }
        ProfileList.GetComponent<RectTransform>().sizeDelta = new Vector2(ProfileList.GetComponent<RectTransform>().sizeDelta.x, profilePrefab.GetComponent<RectTransform>().sizeDelta.y * _playerList.players.Count); //This only runs once at the start, so no sweating the getComponents
    }

    void OnClickProfile(SelectableProfile profile)
    {
        if (activeProfile != null && activeProfile!=profile)
            activeProfile.Deselect();

        activeProfile = profile;
        LoadButton.interactable = true;
    }

    public void Load()
    {
        OnLoaded(activeProfile.GetPlayer());
        Close();
    }

    void Close()
    {
        CleanUpData();
        gameObject.SetActive(false);
    }

    void CleanUpData()
    {
        Name.SetTextWithoutNotify("");
        for(int i = ProfileList.childCount-1; i>=0; i--)
        {
            Destroy(ProfileList.GetChild(i).gameObject);
        }
        ProfileList.DetachChildren();
    }
}
