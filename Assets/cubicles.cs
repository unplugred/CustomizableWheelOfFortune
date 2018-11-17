using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class cubicles : MonoBehaviour
{
	public GameObject game;
	public Transform canvo;
	public GameObject textthing;
	public Object cube;
	public AnimationCurve cubcurv;
	public Transform thingy;
	public Material blendbg;
	public Transform screenshake;
	public Material chroma;
	public Material distort;
	public Font bold;
	public Font regular;
	string[] participants;
	float speed;
	float duration;
	bool onloop = true;
	Text[] txtish;
	float splice;
	int f;
	float flashening = 10;
	float chrom;
	float timeningdistorto;

	void Start()
	{
		thingy.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));

		if (File.Exists("participants.txt")) participants = File.ReadAllLines("participants.txt");
		txtish = new Text[participants.Length];
		splice = 360 / participants.Length;
		for (int i = 0; i < participants.Length; i++)
		{
			float a = ((float)i / participants.Length) * Mathf.PI * 2;
			(txtish[i] = (Object.Instantiate(textthing, new Vector3(
				Mathf.Cos(a) * textthing.transform.position.y,
				Mathf.Sin(a) * textthing.transform.position.y, -5), Quaternion.identity, canvo) as GameObject).GetComponent(typeof(Text)) as Text).text = participants[i];
		}
		fix();
		boostticker();
	}

	void Update()
	{
		screenshake.localPosition = Vector3.zero;
		distort.SetFloat("ratio", (float)Screen.width / Screen.height);

		if (Input.GetKeyUp(KeyCode.Space))
		{
			boostticker();
			if (onloop)
			{
				foreach (Transform lilboi in transform) Object.Destroy(lilboi.gameObject);
				fix();
				screenshake.localPosition += new Vector3(Random.Range(-2, 3), Random.Range(-2, 3));
				chrom += 4;
				timeningdistorto = -Time.deltaTime;
			}
			flashening = 10;
			txtish[f % participants.Length].enabled = true;
		}

		thingy.Rotate(0, 0, Mathf.Min(speed = Mathf.Lerp(speed, 0, Time.deltaTime * duration), 10000f) * Time.deltaTime, 0);

		if (speed <= 3)
		{
			if (!onloop)
			{
				onloop = true;
				screenshake.localPosition += new Vector3(Random.Range(-2, 3), Random.Range(-2, 3));
				chrom += 4;
				speed = 0;
				flashening = 0;
			}
		}
		else onloop = false;

		int e = f;
		if ((f = Mathf.FloorToInt((thingy.localEulerAngles.z + (splice * 0.5f) + 90) / splice)) != e && (e = Mathf.Abs(e - f)) != participants.Length)
		{
			screenshake.localPosition += new Vector3(Random.Range(-e, e + 1), Random.Range(-e, e + 1));
			chrom += e * 3;
			txtish[(f - 1) % participants.Length].color = Color.black;
			txtish[(f - 1) % participants.Length].font = regular;
			txtish[f % participants.Length].font = bold;
		}

		txtish[f % participants.Length].color = (flashening += Time.deltaTime) < 2 ? (flashening % 0.5f > 0.25f ? Color.red : Color.black) : Color.red;

		chroma.SetFloat("much", chrom + speed * 0.0001f);
		chrom = Mathf.Lerp(chrom, 0, Time.deltaTime * 10);

		distort.SetFloat("size", timeningdistorto += Time.deltaTime * 6);
	}

	void fix()
	{
		float a;
		Vector2 s;
		for (int i = 0; i < 50; i++) (Object.Instantiate(cube, new Vector3((cubcurv.Evaluate((s = Random.insideUnitCircle).x * 0.5f + 0.5f) - 0.5f) * 6, (cubcurv.Evaluate(s.y * 0.5f + 0.5f) - 0.5f) * 6, -4), Quaternion.Euler(0, 0, Random.Range(0f, 360f)), transform) as GameObject).transform.localScale = new Vector3(a = Random.Range(0.8f, 2.2f), a, a);

		float hue = Random.value;
		int flip = Random.Range(0, 2);
		blendbg.SetColor("bg", Color.HSVToRGB(Mathf.Repeat(hue + (~-(Random.Range(0, 2) << 1)) * 0.1f, 1f), 0.4f, 0.3f + flip * 0.3f));
		Color fg = Color.HSVToRGB(hue, 0.4f, 0.3f + (1 - flip) * 0.3f);
		blendbg.SetColor("fg", fg);
		distort.SetColor("fg", fg);
	}

	void boostticker()
	{
		screenshake.localPosition += new Vector3(Random.Range(-2, 3), Random.Range(-2, 3));
		speed += Random.Range(100, 1500);
		duration = Random.Range(0.1f, 2f);
	}

	void OnApplicationQuit()
	{
		chroma.SetFloat("much", 2);
		distort.SetFloat("size", 1000);
	}
}
