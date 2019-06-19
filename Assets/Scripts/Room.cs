using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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
    private GameManager gm;
    private float hpToRemove = 0;
    private float inithpPerObject = 0;
    private float roomHP;

    public float HPRemovalInterval => hpRemovalInterval;
    public bool Dead { get; private set; } = false;

    private void Start()
    {
        gp = FindObjectOfType<GameLoop>();
        gm = FindObjectOfType<GameManager>();
        roomHP = maxRoomHP;
        inithpPerObject = hpPerObject;
    }

    private void Update()
    {
        if (!Dead)
            Die();
    }

    public void Die()
    {
        if (roomHP <= 0)
        {
            Dead = true;
            string time = string.Format("{0}:{1:00}", (int)gm.Timer / 60, (int)gm.Timer % 60);
            TextMeshProUGUI deathPanelTxt = deathPanel.GetComponentInChildren<TextMeshProUGUI>();
            deathPanelTxt.text = $"Conseguiste aguentar a sala durante {time} minutos!\n\n" +
                $"Há crianças que lidam com isto todos os dias, mas infelizmente não têm ferramentas para reparar as suas escolas.";
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
        if (roomHP < 0) roomHP = 0;
        float ratio = roomHP / maxRoomHP;
        Sequence setBars = DOTween.Sequence();
        setBars.Append(healthBar.transform.DOScaleY(ratio, .2f))
            .Append(healthRedBar.transform.DOScaleY(ratio, 4f));
    }

    public void AddHPToRemove() => hpToRemove += hpPerObject;

    public void RemoveHPToRemove() => hpToRemove -= hpPerObject;
}
