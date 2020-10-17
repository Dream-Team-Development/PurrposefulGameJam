using RhythmGame;
using UnityEngine;

public class PlayerManagerRhythm : MonoBehaviour
{
    public static PlayerManagerRhythm Instance;
    
    [SerializeField] private CatObject[] _cats;
    [SerializeField] private PlayerController _p1, _p2;

    private void Start()
    {
        Instance = GetComponent<PlayerManagerRhythm>();
        
        _p1.CatType = _cats[PlayerPrefs.GetInt("P1Cat")];
        _p2.CatType = _cats[PlayerPrefs.GetInt("P2Cat")];

        _p1.Weight = PlayerPrefs.GetFloat("P1Weight");
        _p2.Weight = PlayerPrefs.GetFloat("P2Weight");

        _p1.Energy = PlayerPrefs.GetFloat("P1Energy");
        _p2.Energy = PlayerPrefs.GetFloat("P2Energy");
    }


    public void SaveStats()
    {
        PlayerPrefs.SetFloat("P1Weight", _p1.Weight);
        PlayerPrefs.SetFloat("P2Weight", _p2.Weight);
        
        PlayerPrefs.SetFloat("P1Energy", _p1.Energy);
        PlayerPrefs.SetFloat("P2Energy", _p2.Energy);
    }
}
