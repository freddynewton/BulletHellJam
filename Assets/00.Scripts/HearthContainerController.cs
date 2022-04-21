using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HearthContainerController : MonoBehaviour
{
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public GameObject HeartContainerPrefab;
    private PlayerManager playerManager;

    private List<Image> heartList = new List<Image>();

    private void Awake()
    {
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        for (int i = 0; i < playerManager.MaxHealth; i++)
        {
            var Image = Instantiate(HeartContainerPrefab, transform);
            heartList.Add(Image.GetComponent<Image>());

            heartList[heartList.Count - 1].sprite = FullHeart;
        }

        playerManager.OnPlayerHealthChange += UpdateHeartAmount;
        playerManager.OnPlayerDeath += UpdateHeartAmount;
    }

    private void UpdateHeartAmount()
    {
        for (int i = 0; i < playerManager.MaxHealth; i++)
        {
            heartList[i].sprite = i < playerManager.currentHealth ? FullHeart : EmptyHeart;
        }
    }
}
