using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int Level = 1;
    public float MaxHealth = 100f;
    public float Health = 100f;
    public float MaxExperience = 100f;
    public float Experience = 0;
    private Collider m_Collider;
    private void Start() {
        m_Collider = GetComponent<Collider>();
    }

    private void Update() {
        
    }
    public void AddExperience(float experience){
        Experience+=experience;
        while(CheckExperience());
    }

    public bool CheckExperience(){
        if(Experience>=MaxExperience){
            Level+=1;
            Experience -= MaxExperience;
            return true;
        }
        return false;
    }
    public void MakeDamage(float damage,float staminaCast=0,float manaCast=0){
        RaycastHit hit;
        int layerMask = 0b100000000;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 1.1f;
        Vector3 center = m_Collider.bounds.center;
        Debug.DrawRay(center, forward, Color.green, 10f, false);
        if (Physics.Raycast(center, forward, out hit,Mathf.Infinity,layerMask)){
            var damageController = hit.transform.GetComponent<DamageController>();
            if(damageController){
                damageController.TakeDamage(damage,this);
            }
        }
    }
    public void TakeDamage(float damage,DamageController assaulter){
        Health -= damage;
        if(Health < 0){
            assaulter.AddExperience(100);
            Destroy(gameObject);
        }
    }
}