using Assets.Scripts.CharacterMain.PowerBall;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
    #region Status
    public float Attack = 100f;
    public float Ammor = 0f;
    public float Health = 500f;
	#endregion

	#region UI
	[SerializeField] Slider bloodSlider;
    #endregion

	#region Private Variables
    private float maxHealth;
    private float damageRessitance = 0f;
    private Animator animator;
	private Collider2D colider2d;
	private Rigidbody2D rigidbody2d;

	private bool isResistance; // Kháng gián đoạn
	private float resistanceDuration = 0f;

	private bool isHit;
	private float hitDuration = 0f;
	private int hitCount = 0;
	private int hitMax = 3;
	private bool death;
    #endregion

    #region
    private Vector3 currentPos;
    public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	public GameObject item4;
	public GameObject item5;
	public GameObject item6;
	public GameObject item7;
	public GameObject item8;
	private List<GameObject> listItems;
	public GameObject effect;
	#endregion

	

    public float GetMaxHealth()
	{
		return maxHealth;
	}
	// Start is called before the first frame update
	private void Awake()
	{
		bloodSlider.maxValue = Health;
		bloodSlider.value = Health;
		maxHealth = Health;
		animator = GetComponent<Animator>();
		colider2d = GetComponent<Collider2D>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

    // Start
    private void Start()
    {
        listItems = new List<GameObject>
        {
            item1,
            item2,
            item3,
            item4,
            item5,
            item6,
            item7,
            item8
        };
    }

    // Update is called once per frame
    void Update()
    {
		currentPos = transform.position;

        bloodSlider.value = Health;
        if (isHit)
		{
			hitDuration += Time.deltaTime;
		}

		if (hitDuration >= 0.75f && isHit)
		{
			hitDuration = 0f;
			isHit = false;
		}

		if (resistanceDuration > 0 && isResistance)
		{
			ResistanceCooldown();
		}
	}

	public void TakeDmg(float damage)
	{
		Health = Mathf.Clamp(Health - (damage - Ammor / 100 * damage) * (100 - damageRessitance) / 100, 0, maxHealth);
		if (Health <= 0)
		{
			//Death
			animator.SetTrigger("Death");
			rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            colider2d.isTrigger = true;
            rigidbody2d.gravityScale = 1;
            StartCoroutine(SpawnItemWithDelay(1.9f));
            death = true;
			//gameObject.SetActive(false);
			Destroy(gameObject, 2f);

		}

		if (Health > 0 && !isResistance)
		{
			if (isHit && hitDuration < 2f)
			{
				IncreaseHit();
			}
			//Hit
			animator.SetTrigger("Hit");

			//reset Hit duration
			Hit();
		}
	}

    IEnumerator SpawnItemWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        int randomIndex = Random.Range(0, listItems.Count); // Chọn một chỉ mục ngẫu nhiên
		Instantiate(listItems[randomIndex], currentPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D trig)
	{
		if (trig.tag != "Ground" && death) 
		{
			Physics2D.IgnoreCollision(trig.GetComponent<Collider2D>(), colider2d);
		}
		if (trig.tag == "PowerBall")
		{
			TakeDmg(500);
			//trig.GetComponent<AutoDestroy>().AppearEffect();
			trig.gameObject.SetActive(false);
			Instantiate(effect, currentPos, Quaternion.identity);
		}
	}

	private void Hit()
	{
		isHit = true;
		hitDuration = 0f;
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

	private void ResistanceCooldown()
	{
		resistanceDuration -= Time.deltaTime;
		if (resistanceDuration <= 0)
		{
			isResistance = false;
			resistanceDuration = 0f;
		}
	}

	public void AcitveResistanceTime(float time)
	{
		isResistance = true;
		resistanceDuration = time;
	}

	public void ActitveDamageResistance(float dmgRes)
	{
		isResistance = true;
		damageRessitance = dmgRes;
	}

	public void AcitveResistance()
	{
		isResistance = true;
	}

	public void UnActiveResistance()
	{
		isResistance = false;
		damageRessitance = 0f;
	}

	public bool IsDeadth()
	{
		return death;
	}
}
