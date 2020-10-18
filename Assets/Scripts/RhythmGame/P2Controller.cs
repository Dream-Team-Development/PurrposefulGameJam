using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RhythmGame
{
    public class P2Controller : PlayerController
    {
        private void Update()
        {
            if (!RhythmGameManager.Instance.Playing) return;
            if (!_noteToHit) return;
            
            //if (_weight < 0) _weight = 0;
            //if (_weight > 0) _weight -= _weightLossRate * Time.deltaTime;
            
            switch (_noteToHit.ThisDirection)
            {
                case MusicNote.Direction.Down:
                    CheckHit(KeyCode.DownArrow);
                    break;
                
                case MusicNote.Direction.Left:
                    CheckHit(KeyCode.LeftArrow);
                    break;
                
                case MusicNote.Direction.Right:
                    CheckHit(KeyCode.RightArrow);
                    break;
                
                case MusicNote.Direction.Up:
                    CheckHit(KeyCode.UpArrow);
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_noteToHit.transform.position.y >= _noteToHit.DespawnPos.y && _noteToHit.transform.position.y <= _noteToHit.HitPos.y - 2)
            {
                Hit(false, false);
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
                Energy = _energyChange;
                //Weight = _weightChange;
                UpdateStreak(true);
                _noteToHit.TriggerFloatingText(_hitFeedback[Random.Range(0, _hitFeedback.Length)]);
            }
            else if (early)
            {
                Energy = _energyChange * 2;
                Weight -= _weightChange;
                UpdateStreak(false);
                _noteToHit.TriggerFloatingText("Too early!");
            }
            else
            {
                Energy = _energyChange / 2;
                Weight -= _weightChange;
                UpdateStreak(false);
                _noteToHit.TriggerFloatingText("Missed!");
            }
            
            Destroy(_noteToHit.gameObject);
        }
    }
}
