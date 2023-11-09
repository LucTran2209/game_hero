using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Assets.Scripts.CharacterMain;
using Assets.Scripts;

public class PlayerHealth : MonoBehaviour
{
	// Khai báo máu 
	[SerializeField] private float current_health;
	private float max_health;


	// Khai báo biên UI
	[SerializeField] Slider playerHealthSlider;
	[SerializeField] TextMeshProUGUI txtBlood;
	[SerializeField] GameObject panelEndGame;

	// Adudio
	[SerializeField] AudioSource audioPlayerDead;
	[SerializeField] AudioSource audioPlayerHurt;

	private Animator m_animator;
	private Rigidbody2D m_rigidbody;
	private Collider2D m_collider;
	private float dmgTake = 0f;
	private float dmgScale = 1f;
	public float dmgMax = 1f;

	private bool isResistance; // Kháng gián đoạn
	private float resistanceDuration = 0f;



	private bool isHit;
	private float hitDuration = 0f;
	private int hitCount = 0;
	private int hitMax = 3;

	private bool death;

	// Start is called before the first frame update
	void Awake()
	{

        m_animator = GetComponent<Animator>();
		m_collider = GetComponent<Collider2D>();
		m_rigidbody = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
	{
        max_health = PlayerPrefs.GetFloat(Key.PlayerHealth); ;
        current_health = max_health;
        playerHealthSlider.maxValue = max_health;
        playerHealthSlider.value = max_health;
    }

	// Update is called once per frame
	void Update()
	{
		playerHealthSlider.value = current_health;
		txtBlood.text = $"{(int)current_health / max_health * 100}%";

		if (death)
		{
			return;
		}

		if (isHit)
		{
			hitDuration += Time.deltaTime;
		}

		if (hitDuration >= 0.75f && isHit)
		{
			hitDuration = 0f;
			isHit = false;
		}

		if (resistanceDuration >= 0 && isResistance)
		{
			ResistanceCooldown();
		}
	}

	public void TakeDmg(float dmg)
	{
		Debug.Log("Take dame: " + dmg);
		dmgTake = dmgScale * dmg * dmgMax;
		if (dmgTake > 0 && !death)
		{
			DecreaseHP(dmgTake);
		}
	}

	private void DecreaseHP(float dmg)
	{
		current_health = Mathf.Clamp(current_health - dmg, 0, max_health);
		Debug.Log("after decrease hp: " + current_health);

		PlayerPrefs.SetFloat(Key.PlayerCurrentHealth, current_health);
		if (current_health <= 0)
		{
			Deadth(2f);
			return;
		}

		if (current_health > 0
			&& !isResistance 
			&& !m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Block")
            && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("Block"))

		{
			if (isHit && hitDuration < 2f)
			{
				IncreaseHit();
			}

			// audio hurt
			audioPlayerHurt.Play();

			//Hit
			Debug.Log("Hit");
			m_animator.SetTrigger("Hit");

			//reset Hit duration
			Hit();
		}
	}

	private void Deadth(float timedelay)
	{
		Debug.Log("Dead");
		audioPlayerDead.Play();
		// set animation Deadth      
		// GetComponent<Collider2D>().isTrigger = true;
		// m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
		// StartCoroutine(DestroyAfterSeconds(2f));
		m_animator.SetTrigger("Dead");
		death = true;
		GetComponent<PlayerMovement>().StopMove();
		StartCoroutine(Endgame(timedelay));
	}

	private void IncreaseHit()
	{
		hitCount++;
		if (hitCount >= hitMax)
		{
			hitCount = 0;
			AcitveResistanceTime(2f);
		}
	}



	private void Hit()
	{
		isHit = true;
		hitDuration = 0f;
	}

	public void AcitveResistanceTime(float time)
	{
		isResistance = true;
		resistanceDuration = time;
	}

	private void ResistanceCooldown()
	{
		resistanceDuration -= Time.deltaTime;
		if (resistanceDuration <= 0)
		{
			isResistance = false;
			resistanceDuration = 0f;
		}
	}

	public void AcitveResistance()
	{
		isResistance = true;
	}

	public void ActitveDamageResistance(float dmgRes)
	{
		isResistance = true;
		dmgScale = dmgRes;
	}

	public void UnActiveResistance()
	{
		isResistance = false;
		dmgScale = 1f;
	}

	IEnumerator Endgame(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		panelEndGame.SetActive(true);
		Time.timeScale = 0f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Trap")
		{
			//PlayerPrefs.SetFloat(Key.PlayerCurrentHealth, 0f);
			this.DecreaseHP(max_health);
		}
	}


	public bool IsDead()
	{
		return death;
	}

	// Player dead when fall
	public void DeadWhenFall()
	{
		this.Deadth(0.5f);
	}
}
