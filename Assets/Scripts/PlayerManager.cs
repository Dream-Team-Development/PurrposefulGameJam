using SushiGame;
using SushiGame.Controller;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private CatObject[] _cats;
    [SerializeField] private PlayerController _p1, _p2;

    private void Start()
    {
        _p1.CatType = _cats[PlayerPrefs.GetInt("P1Cat")];
        _p2.CatType = _cats[PlayerPrefs.GetInt("P2Cat")];
    }
}
