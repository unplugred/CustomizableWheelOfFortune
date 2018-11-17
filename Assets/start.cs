using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class start : MonoBehaviour
{
	public GameObject strat;
	public GameObject game;
	public Text text;
	public AnimationCurve cruve;
	public Material blendbg;
	float hue;
	int bgn;

	void Start()
	{
		Cursor.visible = false;
		bgn = ~-(Random.Range(0, 2) << 1);
		hue = Random.value;
		blendbg.SetColor("bg", Color.HSVToRGB((hue + bgn * 0.1f) % 1, 0.4f, 0.6f));
		blendbg.SetColor("fg", Color.HSVToRGB(hue, 0.4f, 0.3f));
	}

	void Update()
	{
		hue += Time.deltaTime * 0.02f;
		blendbg.SetColor("bg", Color.HSVToRGB((hue + bgn * 0.1f) % 1, 0.4f, 0.6f));
		blendbg.SetColor("fg", Color.HSVToRGB(hue % 1, 0.4f, 0.3f));

		if (Input.GetKeyDown(KeyCode.Space))
		{
			text.color = Color.blue;
		}
		else
		{
			text.transform.localPosition = new Vector3(0, (int)((cruve.Evaluate(Time.time * 0.5f) - 0.5f) * 21));
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			strat.SetActive(false);
			game.SetActive(true);
		}
	}
}
