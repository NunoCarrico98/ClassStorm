using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Room : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] private Image healthBar;
	[SerializeField] private Image healthRedBar;
	[SerializeField] private float maxRoomHP;
	[SerializeField] private float hpPerObject;
	[SerializeField] private float hpRemovalInterval;

	[SerializeField] private GameObject deathPanel;

    private GameLoop gp;
	private float hpToRemove = 0;
	private float inithpPerObject = 0;
	private float roomHP;

	public float HPRemovalInterval => hpRemovalInterval;
	public bool Dead { get; private set; } = false;

	private void Start()
	{
        gp = FindObjectOfType<GameLoop>();
		roomHP = maxRoomHP;
        inithpPerObject = hpPerObject;
	}

	private void Update()
	{
		Die();
	}

	public void Die()
	{
		if (roomHP <= 0)
		{
			Dead = true;
			deathPanel.SetActive(true);
			StopAllCoroutines();
		}
	}

    //public void ManageHpPerObject()
    //{
    //    hpPerObject = inithpPerObject / ((gp.breaksCounter+1)*5);
    //    Debug.Log("Called and value is: " + hpPerObject);
    //    Debug.Log("breaksCounter is: " + gp.breaksCounter);
    //}


    public void SetRoomHP()
	{
		roomHP -= hpToRemove;
        Debug.Log(hpToRemove);
		if (roomHP < 0) roomHP = 0;
		float ratio = roomHP / maxRoomHP;
		Sequence setBars = DOTween.Sequence();
		setBars.Append(healthBar.transform.DOScaleY(ratio, .2f))
			.Append(healthRedBar.transform.DOScaleY(ratio, 4f));
	}

	public void AddHPToRemove() => hpToRemove += hpPerObject;

	public void RemoveHPToRemove() => hpToRemove -= hpPerObject;
}
