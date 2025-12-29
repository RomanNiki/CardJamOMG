using App.Scripts.Infrastructure.BaseView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.Game.Views
{
    public class ViewCard : MonoView
    {
        public Image[] cardBackgrounds;
        public TMP_Text[] texts;

        public void SetNumber(int number)
        {
            foreach (TMP_Text tmpText in texts)
            {
                tmpText.SetText(number.ToString());
            }
        }

        public void SetColor(Color color)
        {
            foreach (var cardBackground in cardBackgrounds)
            {
                cardBackground.color = color;
            }
        }
    }
}