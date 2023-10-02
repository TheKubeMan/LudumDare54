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
            _foodScorer.OnFoodMaxScoreChange += SliderMaxValueUpdate;
        }

        private void OnDisable()
        {
            _foodScorer.OnFoodScoreChange -= ScoreUpdate;
            _foodScorer.OnFoodMaxScoreChange += SliderMaxValueUpdate;
        }

        private void Start()
        {
            SliderMaxValueUpdate();
            ScoreUpdate();
        }

        private void SliderMaxValueUpdate()
        {
            foodScoreText.text = $"{_foodScorer.Score}/{_foodScorer.MaxScore}";
            foodScoreSlider.maxValue = _foodScorer.MaxScore;
        }
        
        private void ScoreUpdate()
        {
            foodScoreText.text = $"{_foodScorer.Score}/{_foodScorer.MaxScore}";
            foodScoreSlider.value = _foodScorer.Score;
        }
    }
}