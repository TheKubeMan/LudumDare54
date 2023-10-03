using System;
using TMPro;
using UnityEngine;

namespace UISystem
{
    public class MaxScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text maxScoreText;

        private void Start()
        {
            maxScoreText.text = $"Max score: {PlayerPrefs.GetInt("MaxScore")}";
        }
    }
}