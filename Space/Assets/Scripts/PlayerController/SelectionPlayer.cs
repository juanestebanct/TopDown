using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SelectionPlayer : MonoBehaviour
{
    [Header("Spawn referen")]
    [SerializeField] private PlayerController[] allArrayPlayerPrefab;
    [SerializeField] private FloatingJoystick[] floatingJoysticks;
    [SerializeField] private GameObject deadUi;
    [SerializeField] private Image refImage;
    [SerializeField] private TextMeshProUGUI liveText;
    [SerializeField] private CinemachineVirtualCamera camera;

    private int indexPlayer;
    void Start()
    {
        if (PlayerPrefs.HasKey("indexPlayer")) indexPlayer = PlayerPrefs.GetInt("indexPlayer");
        else indexPlayer = 0;
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        PlayerController Newplayer = Instantiate(allArrayPlayerPrefab[indexPlayer],new Vector3(0,0,0), Quaternion.Euler(-90, 0, 0));
        Newplayer.GetUiRef(deadUi, floatingJoysticks);
        Newplayer.GetComponent<PlayerStats>().GetIuElement(refImage, liveText);
        camera.Follow = Newplayer.transform;
    }
}
