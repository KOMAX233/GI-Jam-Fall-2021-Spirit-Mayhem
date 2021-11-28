using TMPro;
using UnityEngine;

public class SpellDescription : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Player.Instance.allSpirits.Count == 0)
        {
            text.color = Color.white;
            text.text = "None";
        }
        else
        {
            var projectileMove = Player.Instance.allSpirits[0].MyProjectileMove;
            text.color = Color.Lerp(projectileMove.spellParams.color, Color.white, .5f);
            text.text = projectileMove.spellEffect.description;
        }
    }
}