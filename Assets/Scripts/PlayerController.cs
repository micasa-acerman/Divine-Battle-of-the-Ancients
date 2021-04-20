using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RotateSpeed = 130f;
    private bool m_InGround = true;

    public float JumpForce = 15f;
    public AudioSource AudioSourceJump;
    private Animator m_Animator;
    private Rigidbody m_RigidBody;
    private AudioSource m_AudioSource;
    private DamageController m_DamageController;
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        m_RigidBody = GetComponent<Rigidbody>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        m_DamageController = GetComponent<DamageController>();
    }
    void FixedUpdate()
    {
        var animatorInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        m_Animator.SetBool("IsWalking", SimpleInput.GetAxis("Vertical") > 0);
        m_Animator.SetBool("IsRunning", SimpleInput.GetAxis("Vertical") > 0.8);

        if (SimpleInput.GetButtonDown("Roll") && !animatorInfo.IsName("Male Sword Roll"))
        {
            m_Animator.SetTrigger("Roll");
        }
        if (SimpleInput.GetButtonDown("Jump") && m_InGround)
        {
            m_RigidBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            m_InGround = false;
            m_Animator.SetBool("InGround", false);
            AudioSourceJump.Play();
        }
        if (!m_InGround && SimpleInput.GetAxis("Vertical") != 0)
        {
            m_RigidBody.velocity += transform.forward * SimpleInput.GetAxis("Vertical") * 10f;
        }
        if (SimpleInput.GetButtonDown("Attack1"))
        {
            m_Animator.SetTrigger("Attack");
            m_Animator.SetInteger("AttackType", 1);
            m_DamageController.MakeDamage(10);
        }
        if (SimpleInput.GetButtonDown("Attack2"))
        {
            m_Animator.SetTrigger("Attack");
            m_Animator.SetInteger("AttackType", 2);
            m_DamageController.MakeDamage(25);
        }
        if (SimpleInput.GetButtonDown("Attack3"))
        {
            m_Animator.SetTrigger("Attack");
            m_Animator.SetInteger("AttackType", 3);
            m_DamageController.MakeDamage(30);
        }
        AudioController(animatorInfo);
        float angle = SimpleInput.GetAxis("Horizontal") * Time.deltaTime * RotateSpeed;
        var rotateValue = new Vector3(0, angle * -1, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        // TODO: Убрать
        if (collisionInfo.gameObject.tag == "Floor" || true)
        {
            m_InGround = true;
            m_Animator.SetBool("InGround", true);
        }
    }
    // void OnCollisionExit(Collision collisionInfo)
    // {
    //     m_InGround = false;
    //     m_Animator.SetBool("InGround", false);
    // }


    public AudioAction[] AudioClipActions;
    [System.Serializable]
    public class AudioAction
    {
        public string AnimationName;
        public AudioClip AudioClip;
        public bool IsLoop;
    }

    private string m_CurrentAnimationName;
    private void AudioController(AnimatorStateInfo animatorInfo)
    {
        bool find = false;
        foreach (var action in AudioClipActions)
        {
            if (animatorInfo.IsName(action.AnimationName))
            {
                if (m_CurrentAnimationName != action.AnimationName)
                {
                    m_AudioSource.clip = action.AudioClip;
                    m_AudioSource.loop = action.IsLoop;
                    m_AudioSource.Play();
                    m_CurrentAnimationName = action.AnimationName;
                }
                find = true;
                break;
            }
        }
        if (!find)
        {
            if (m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
            }
            if (m_CurrentAnimationName != "")
                m_CurrentAnimationName = "";
        }
    }
}
