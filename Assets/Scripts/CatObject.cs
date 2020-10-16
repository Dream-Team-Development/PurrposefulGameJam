using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Cat", menuName = "Cat")]

[System.Serializable]
public class CatObject : ScriptableObject
{
    [Header("Information")]
    public string catName;
    public Sprite profileImage;
    public int idealWeight;
    public Sprite sexImage;

    [Header("Animations")]
    public AnimatorController sushiGameAnimator;
    public AnimatorController rhythmGameAnimator;
}
