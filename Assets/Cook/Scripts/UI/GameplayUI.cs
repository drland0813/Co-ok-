using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private CountDownTimerUI _countDownTimerUI;
        [SerializeField] private ClockCountDownUI _clockCountDownUI;
        public void UpdateCountDownTimer(float timer)
        {
            _countDownTimerUI.UpdateTimer(timer);
        }
        
        public void UpdateClockCountDownTimer(float timer)
        {
            _clockCountDownUI.UpdateTimer(timer);
        }
    }
}
