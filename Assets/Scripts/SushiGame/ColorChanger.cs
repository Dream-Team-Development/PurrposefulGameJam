using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SushiGame
{
    public class ColorChanger : MonoBehaviour
    {
        private Color[] _colors = {Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow};

        private void Awake()
        {
            StartCoroutine(ColorChange());
        }

        public IEnumerator ColorChange()
        {
            var alwaysTrue = true;
            var render = GetComponent<SpriteRenderer>();
            var current = Random.Range(0, _colors.Length);
            var next = current + 1;
            var t = 0f;

            if (current == _colors.Length - 1) next = 0;
            while (alwaysTrue)
            {
                if (render.color == _colors[next])
                {
                    t = 0;
                    current++;
                    if (current == _colors.Length - 1) next = 0;
                    else if (current == _colors.Length) current = 0;
                    else next = current + 1;
                }

                t += Time.deltaTime;
                render.color = Color.Lerp(_colors[current], _colors[next], t);
                yield return null;
            }
        }
    }
}
