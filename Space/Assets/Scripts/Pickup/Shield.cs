using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Shield : MonoBehaviour
{
    private VisualEffect effect;
    private Coroutine desactive;

    [SerializeField] private float timeToActive;
    private void Awake()
    {
        effect = GetComponent<VisualEffect>();
    }

    //desactiva la corrutina 
    public void ActiveShield()
    {
        gameObject.SetActive(true);
        if (desactive != null) StopCoroutine(desactive);

        effect.SetFloat("lifeTime", timeToActive);
        effect.Play();
        StartCoroutine(Active());
    }
    /// <summary>
    /// vamos a esperar a que se desactive el escudo y cuando eso pase se va a desactivar el objeto y cambiar el tiempo
    /// </summary>
    /// <returns></returns>
    private IEnumerator Active()
    {
        yield return new WaitForSeconds(timeToActive);
        print("salio");
        effect.Stop();
        gameObject.SetActive(false);
        
    }
}
