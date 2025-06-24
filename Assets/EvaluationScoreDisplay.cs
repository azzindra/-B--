using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvaluationScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moralLabelText;

    void Start()
    {
        int total = PointManager.Instance?.GetTotalPoints() ?? 0;
        scoreText.text = "Total Points: " + total;

        string moralLabel = GetMoralLabel(total);
        moralLabelText.text = "Moral Evaluation: " + moralLabel;
    }

    string GetMoralLabel(int score)
    {
        if (score == 0)
            return "Have perfect moral";
        else if (score >= 1 && score <= 7)
            return "Not That Bad";
        else if (score >= 8 && score <= 15)
            return "Concerning...";
        else
            return "IMMORAL!!!";
    }
}
