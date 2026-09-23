using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ResultsManager : MonoBehaviour
{
    [SerializeField ] ScooringSystem score;
    [Header("UI")]
    public TMP_Text RIASECPattern;
    public TMP_Text OCEANPattern;
    public TMP_Text CareerPaths;

    public void DisplayResultsUI()
    {
        DisplayResults();
    }

    void DisplayResults()
    {
        if (score == null)
            return;

        RIASECPattern.text = GetRIASECPattern();

        OCEANPattern.text = GetOCEANDescription();

        CareerPaths.text = GetCareerRecommendations();
    }

    string GetRIASECPattern()
    {
        Dictionary<string, double> scores =
            new Dictionary<string, double>()
            {
                {"R", score.R},
                {"I", score.I},
                {"A", score.rA},
                {"S", score.S},
                {"E",score.rE},
                {"C", score.rC}
            };

        return string.Concat(
            scores.OrderByDescending(x => x.Value)
                  .Take(3)
                  .Select(x => x.Key)
        );
    }

    string GetOCEANDescription()
    {
        string result = "";

        result += GetLevel("Openness", (int)score.O);
        result += ",";

        result += GetLevel("Conscientiousness", (int)score.C);
        result += ",";

        result += GetLevel("Extraversion", (int)score.E);
        result += ",";

        result += GetLevel("Agreeableness", (int)score.A);
        result += ",";

        result += GetLevel("Neuroticism", (int)score.N);

        return result;
    }

    string GetLevel(string trait, int value)
    {
        string level;

        if (value >= 8)
            level = "High";
        else if (value >= 4)
            level = "Moderate";
        else
            level = "Low";

        return trait + ": " + level;
    }

    string GetCareerRecommendations()
    {
        string pattern = GetRIASECPattern();

        // Grab just their absolute highest trait (the first letter)
        char primaryTrait = pattern[0];

        switch (primaryTrait)
        {
            case 'R': // Realistic
                return "Engineering, Architecture, Agriculture, Industrial Design";
            case 'I': // Investigative
                return "Computer Science, Medicine, Data Science, Information Systems";
            case 'A': // Artistic
                return "Graphic Design, Fine Arts, Architecture, Public Relations";
            case 'S': // Social
                return "Psychology, Teaching, Nursing, Human Resources";
            case 'E': // Enterprising
                return "Business Administration, Law, Marketing, Management";
            case 'C': // Conventional
                return "Accounting, Finance, Data Entry, Actuarial Science";
            default:
                return "General Studies";
        }
    }
}