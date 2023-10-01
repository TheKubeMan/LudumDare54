using System;
using UnityEngine;

namespace FoodSystem
{
    public class FoodScorer
    {
        public event Action OnFoodScoreChange;
        public event Action OnFoodMaxScoreChange;
        public int Score { get; private set; }
        public int MaxScore { get; private set; }

        public void AddScore(int value)
        {
            Score += value;
            OnFoodScoreChange?.Invoke();
        }
        
        public void SetMaxScore(int value)
        {
            MaxScore = value;
            OnFoodMaxScoreChange?.Invoke();
        }
    }
}