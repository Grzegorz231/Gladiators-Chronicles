using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MatchLogic : MonoBehaviour
    {
        static MatchLogic Instance;

        public int maxPoints = 3;
        public Text pointsText;
        public GameObject levelCompleteUI;
        private int points = 0;

        private void Start()
        {
            Instance = this;
        }

        void UpdatePotinsText()
        {
            pointsText.text = points + "/" + maxPoints;
            if (points == maxPoints)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        public static void AddPoint()
        {
            AddPoints(1);
        }

        public static void AddPoints(int points)
        {
            Instance.points += points;
            Instance.UpdatePotinsText();
        }
    }
}
