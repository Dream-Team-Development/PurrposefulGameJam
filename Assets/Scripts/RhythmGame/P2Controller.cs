using System;
using UnityEngine;

namespace RhythmGame
{
    public class P2Controller : PlayerController
    {
        private void Update()
        {
            if (!_noteToHit)
            {
                Debug.Log("No note found");
                return;
            }
            
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
        }


        private void CheckHit(KeyCode keyToHit)
        {
            Debug.Log("Checking hit");
            
            if (!Input.GetKeyDown(keyToHit)) return;

            if (_noteToHit.transform.position.y <= _noteToHit.HitPos.y + 1 && _noteToHit.transform.position.y >= _noteToHit.HitPos.y - 1)
            {
                Debug.Log("Hit");
                Hit(true);
            }
            else
            {
                Debug.Log("Miss");
                Hit(false);
            }
        }


        private void Hit(bool hit)
        {
            if (hit)
            {
                GuiManager.Instance.P2Weight += _noteToHit.Points;
                _noteToHit.TriggerFloatingText("Nice!");
            }
            else if (!hit)
            {
                GuiManager.Instance.P2Weight -= _noteToHit.Points / 2;
                _noteToHit.TriggerFloatingText("Missed!");
            }
            
            Destroy(_noteToHit.gameObject);
        }
    }
}
