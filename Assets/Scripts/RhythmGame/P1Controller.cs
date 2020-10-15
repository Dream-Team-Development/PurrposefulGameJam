using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RhythmGame
{
    public class P1Controller : PlayerController
    {
        private void Update()
        {
            if (!_noteToHit) return;
            
            switch (_noteToHit.ThisDirection)
            {
                case MusicNote.Direction.Down:
                    CheckHit(KeyCode.S);
                    break;
                
                case MusicNote.Direction.Left:
                    CheckHit(KeyCode.A);
                    break;
                
                case MusicNote.Direction.Right:
                    CheckHit(KeyCode.D);
                    break;
                
                case MusicNote.Direction.Up:
                    CheckHit(KeyCode.W);
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Missed completely
            if (_noteToHit.transform.position.y >= _noteToHit.DespawnPos.y && _noteToHit.transform.position.y <= _noteToHit.HitPos.y - 2)
            {
                GuiManager.Instance.P1Weight -= _noteToHit.Points;
                _noteToHit.TriggerFloatingText("Missed!");
                
                Destroy(_noteToHit.gameObject);
            }
        }


        private void CheckHit(KeyCode keyToHit)
        {
            if (!Input.GetKeyDown(keyToHit)) return;

            // On time
            if (_noteToHit.transform.position.y <= _noteToHit.HitPos.y + 1 && _noteToHit.transform.position.y >= _noteToHit.HitPos.y - 1)
                Hit(true, false);
            
            // Early
            else if (_noteToHit.transform.position.y > _noteToHit.HitPos.y + 1)
                Hit(false, true);

            // Late/Missed
            else
                Hit(false, false);
        }


        private void Hit(bool hit, bool early)
        {
            if (hit)
            {
                UpdateStreak(true);
                GuiManager.Instance.P1Weight += _noteToHit.Points;
                _noteToHit.TriggerFloatingText(_hitFeedback[Random.Range(0, _hitFeedback.Length)]);
            }
            else if (early)
            {
                UpdateStreak(false);
                GuiManager.Instance.P1Weight -= _noteToHit.Points / 2;
                _noteToHit.TriggerFloatingText("Too early!");
            }
            else
            {
                UpdateStreak(false);
                GuiManager.Instance.P1Weight -= _noteToHit.Points / 2;
                _noteToHit.TriggerFloatingText("Missed!");
            }
            
            Destroy(_noteToHit.gameObject);
        }
    }
}
