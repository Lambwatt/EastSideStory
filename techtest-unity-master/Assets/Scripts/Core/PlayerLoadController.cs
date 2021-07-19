using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLoadController : MonoBehaviour
{
    [SerializeField] Transform _profileList;
    [SerializeField] Button _loadButton;
    [SerializeField] Button _startButton;
    [SerializeField] InputField _name;

    [SerializeField] SelectableProfile _profilePrefab;

    public delegate void OnLoadedAction(PlayerData playerData);
    public event OnLoadedAction OnLoaded;

    private SelectableProfile _activeProfile;
    private PlayerDataList _playerList;

    // Start is called before the first frame update
    void OnEnable()
    {
        _loadButton.interactable = false;
        _startButton.interactable = false;
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
        _startButton.interactable = contents.Length > 0;
    }

    public void CreateNewPlayer()
    {
        OnLoaded(new PlayerData(_name.text));
        Close();
    }

    void PopulateProfiles()
    {
        SelectableProfile entry;
        foreach (PlayerData player in _playerList.players)
        {
            entry = Instantiate(_profilePrefab);
            entry.transform.SetParent(_profileList);
            entry.transform.position = new Vector3(0, 0, 0);
            entry.transform.localScale = new Vector3(1, 1, 1);

            entry.Initialize(player, OnClickProfile);
        }
        _profileList.GetComponent<RectTransform>().sizeDelta = new Vector2(_profileList.GetComponent<RectTransform>().sizeDelta.x, _profilePrefab.GetComponent<RectTransform>().sizeDelta.y * _playerList.players.Count); //This only runs once at the start, so no sweating the getComponents
    }

    void OnClickProfile(SelectableProfile profile)
    {
        if (_activeProfile != null && _activeProfile!=profile)
            _activeProfile.Deselect();

        _activeProfile = profile;
        _loadButton.interactable = true;
    }

    public void Load()
    {
        OnLoaded(_activeProfile.GetPlayer());
        Close();
    }

    void Close()
    {
        CleanUpData();
        gameObject.SetActive(false);
    }

    void CleanUpData()
    {
        _name.SetTextWithoutNotify("");
        for(int i = _profileList.childCount-1; i>=0; i--)
        {
            Destroy(_profileList.GetChild(i).gameObject);
        }
        _profileList.DetachChildren();
    }
}
