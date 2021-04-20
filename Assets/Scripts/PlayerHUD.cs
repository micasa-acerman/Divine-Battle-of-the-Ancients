using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image HealthFiller;
    public Text LevelText;
    public Gradient Gradient;
    private DamageController m_DamageController;
    // Start is called before the first frame update
    void Start()
    {
        m_DamageController = GetComponent<DamageController>();
        Debug.Log(m_DamageController);
        Debug.Log("Player HUD");
        float fillAmount = m_DamageController.Health / m_DamageController.MaxHealth;
        UpdateIndicator(fillAmount);
    }
    // Update is called once per frame
    void Update()
    {
        float fillAmount = m_DamageController.Health / m_DamageController.MaxHealth;
        if (fillAmount != HealthFiller.fillAmount)
        {
            UpdateIndicator(fillAmount);
        }
        string Level = m_DamageController.Level.ToString();
        if (LevelText.text != Level)
        {
            LevelText.text = Level;
        }
    }

    void UpdateIndicator(float fillAmount)
    {
        HealthFiller.fillAmount = fillAmount;
        HealthFiller.color = Gradient.Evaluate(fillAmount);
    }
}
