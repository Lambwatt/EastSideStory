using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
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
        gameObject.SetActive(false);
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
        gameObject.SetActive(false);
    }
}
