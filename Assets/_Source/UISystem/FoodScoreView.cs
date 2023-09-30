using System;
using FoodSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UISystem
{
    public class FoodScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text foodScoreText;
        [SerializeField] private Slider foodScoreSlider;
        [Inject] private FoodScorer _foodScorer;

        private void OnEnable()
        {
            _foodScorer.OnFoodScoreChange += ScoreUpdate;
        }

        private void OnDisable()
        {
            _foodScorer.OnFoodScoreChange -= ScoreUpdate;
        }

        private void Init()
        {
            // foodScoreSlider.highValue = _foodScorer.MaxScore;
        }
        
        private void ScoreUpdate()
        {
            foodScoreText.text = $"{_foodScorer.Score}";
            foodScoreSlider.value = _foodScorer.Score;
        }
    }
}
