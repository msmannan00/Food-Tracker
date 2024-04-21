using AwesomeCharts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashboardStatsController : MonoBehaviour
{
    [Header("Stats Data")]
    public TMP_Text aActive;
    public Image aActiveBadge;
    public Image aActiveBadgeBackground;
    public TMP_Text aTotalCal;
    public TMP_Text aDateRange;
    public TMP_Text aCarbs;
    public TMP_Text aFats;
    public TMP_Text aProteins;
    public GameObject aPieChart;

    private PieChart pieChart;

    void Start()
    {
        pieChart = aPieChart.GetComponent<PieChart>();

        DateTime currentDate = DateTime.Now;
        DateTime endDate = userSessionManager.Instance.mUserStatsModel.sEndingDate;
        if (!userSessionManager.Instance.mUserStatsModel.sContinueWeeklyPlan && currentDate > endDate)
        {
            aActive.SetText("Inactive");
            aActiveBadge.color = Color.red;
            aActive.color = Color.red;
            Color lightRed = new Color(1f, 0.9f, 0.9f);
            aActiveBadgeBackground.color = lightRed;
        }

        double kiloCalories = userSessionManager.Instance.mUserStatsModel.sKiloCalories;
        string formattedKiloCalories = kiloCalories < 10 ? kiloCalories.ToString("0.000") : kiloCalories.ToString("0.00");
        this.aTotalCal.SetText(formattedKiloCalories);

        DateTime startingDate = userSessionManager.Instance.mUserStatsModel.sStartingDate;
        DateTime endingDate = userSessionManager.Instance.mUserStatsModel.sEndingDate;

        string formattedStartingDate = startingDate.ToString("MMM dd");
        string formattedEndingDate = endingDate.ToString("MMM dd");

        this.aDateRange.SetText($"{formattedStartingDate} - {formattedEndingDate}");

        UpdatePieChartValues();
    }

    void UpdatePieChartValues()
    {
        double carbs = userSessionManager.Instance.mUserStatsModel.sCarbs;
        double fats = userSessionManager.Instance.mUserStatsModel.sFats;
        double proteins = userSessionManager.Instance.mUserStatsModel.sProteins;

        double total = carbs + fats + proteins;
        string carbsText, fatsText, proteinsText;

        if (total == 0)
        {
            carbsText = fatsText = proteinsText = "0g (0%)";
            pieChart.UpdateEntry("Other", 100);
        }
        else
        {
            double carbsPercentage = (carbs / total) * 100;
            double fatsPercentage = (fats / total) * 100;
            double proteinsPercentage = (proteins / total) * 100;

            carbsText = $"{carbs:N1}g ({carbsPercentage:N0}%)";
            fatsText = $"{fats:N1}g ({fatsPercentage:N0}%)";
            proteinsText = $"{proteins:N1}g ({proteinsPercentage:N0}%)";

            pieChart.UpdateEntry("Carbs", carbsPercentage);
            pieChart.UpdateEntry("Fats", fatsPercentage);
            pieChart.UpdateEntry("Proteins", proteinsPercentage);
            pieChart.UpdateEntry("Other", 100 - carbsPercentage - fatsPercentage - proteinsPercentage);
        }

        aCarbs.SetText(carbsText);
        aFats.SetText(fatsText);
        aProteins.SetText(proteinsText);
    }
}
