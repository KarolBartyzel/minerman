using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Image Bar;
	
	public float HealthLevel;
	
    // Start is called before the first frame update
    void Start()
    {
        HealthLevel = 1f;
    }

    // Update is called once per frame
    void Update()
    {
		
		HealthLevel -= Time.deltaTime * 0.1f;
		if (Bar.fillAmount >= 0.3f)
		{
        Bar.fillAmount = HealthLevel;
		}
    }
}
