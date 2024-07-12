
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour
{
    [SerializeField] private ShipInfo[] shipInfo;
    [SerializeField] private Image imageShip;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textWeapon;
    [SerializeField] private TextMeshProUGUI textSpeed;
    [SerializeField] private TextMeshProUGUI textLive;

    private int indexPlayer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("indexPlayer")) indexPlayer = PlayerPrefs.GetInt("indexPlayer");
        else indexPlayer = 0;
        indexPlayer = 0;
        updateInfo();
    }
    private void updateInfo()
    {
        textName.text = shipInfo[indexPlayer].Name;
        imageShip.sprite = shipInfo[indexPlayer].ImageRefence;
        textWeapon.text = shipInfo[indexPlayer].Weapon;
        textSpeed.text = shipInfo[indexPlayer].Speed.ToString();
        textLive.text = shipInfo[indexPlayer].Live.ToString();

    }
    public void NextShip()
    {
        if (indexPlayer < shipInfo.Length-1) indexPlayer++;
        else indexPlayer = 0;

        PlayerPrefs.SetInt("indexPlayer", indexPlayer);
        updateInfo();
    }
    public void beforeShip()
    {
        if (indexPlayer != 0) indexPlayer--;
        else indexPlayer = shipInfo.Length-1;
        print(indexPlayer);
        PlayerPrefs.SetInt("indexPlayer", indexPlayer);
        updateInfo();
    }

}
