using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    public GameObject HealthIndicatorPrefab;
    public float OffsetTop = 0;
    public Gradient Gradient;
    private DamageController m_EnemyController;
    private Collider m_Collider;
    private GameObject m_Indicator;
    private Image m_Filler;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_EnemyController = GetComponent<DamageController>();
        m_Indicator = Instantiate(HealthIndicatorPrefab, new Vector3(transform.position.x, m_Collider.bounds.max.y + OffsetTop, transform.position.z), Quaternion.identity, transform);
        m_Filler = m_Indicator.transform.Find("Canvas").Find("Bar").Find("Filler").GetComponent<Image>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float fillAmount = m_EnemyController.Health / m_EnemyController.MaxHealth;
        if (fillAmount != m_Filler.fillAmount){
            m_Filler.fillAmount = fillAmount;
            m_Filler.color = Gradient.Evaluate(fillAmount);
        }
    }
}
