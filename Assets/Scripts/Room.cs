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

	private float hpToRemove = 0;
	private float roomHP;

	public float HPRemovalInterval => hpRemovalInterval;

	private void Start()
	{
		roomHP = maxRoomHP;
	}

	public void SetRoomHP()
	{
		roomHP -= hpToRemove;
		float ratio = roomHP / maxRoomHP;
		Sequence setBars = DOTween.Sequence();
		setBars.Append(healthBar.transform.DOScaleY(ratio, .2f))
			.Append(healthRedBar.transform.DOScaleY(ratio, 1f));
	}

	public void AddHPToRemove() => hpToRemove += hpPerObject;

	public void RemoveHPToRemove() => hpToRemove -= hpPerObject;
}
