using UnityEngine;

namespace RhythmGame
{
    public class FloatingScript : MonoBehaviour
    {
        [SerializeField] private float _destroyTime = 1f;
        [SerializeField] private Vector3 _randomisePos = new Vector3(1f, 1,0);

        private void Start()
        {
            Destroy(gameObject, _destroyTime);
        
            transform.localPosition += new Vector3(
                Random.Range(-_randomisePos.x, _randomisePos.x),
                Random.Range(-_randomisePos.y, _randomisePos.y),
                0);
        }
    }
}
