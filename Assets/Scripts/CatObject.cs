using UnityEngine;

[CreateAssetMenu(fileName = "Cat", menuName = "Cat")]

[System.Serializable]
public class CatObject : ScriptableObject
{
    public string catName;
    public Sprite profileImage;

    public int idealWeight;
}
