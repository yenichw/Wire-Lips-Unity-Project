using UnityEngine;

public class Audio_SmartSound : MonoBehaviour
{
	private Transform PositionListen;

	private float maxDistanceSource;

	private float minDist;

	private float ctf;

	private int pointNear;

	private int r;

	private AudioLowPassFilter sdk;

	private bool OffAllRay;

	private bool gameGiz;

	private Vector3 ptSd;

	private Vector3 posAudio;

	private AudioSource au;

	private RaycastHit hit;

	public GameObject audioSource;

	public float DistanceDrob = 2f;

	public bool local = true;

	public Vector3[] PointsAngleSound;

	public Collider objcol;

	private void Start()
	{
		if (audioSource == null)
		{
			audioSource = base.gameObject;
		}
		PositionListen = GameObject.FindWithTag("MainCamera").gameObject.transform;
		ptSd = base.transform.position;
		au = audioSource.GetComponent<AudioSource>();
		maxDistanceSource = au.maxDistance;
		posAudio = audioSource.transform.position;
		if (audioSource.GetComponent<AudioLowPassFilter>() == null)
		{
			sdk = audioSource.AddComponent(typeof(AudioLowPassFilter)) as AudioLowPassFilter;
		}
		else
		{
			sdk = audioSource.GetComponent<AudioLowPassFilter>();
		}
		if (local)
		{
			for (int i = 0; i < PointsAngleSound.Length; i++)
			{
				PointsAngleSound[i] += base.transform.position;
			}
			local = false;
		}
		gameGiz = true;
	}

	private void FixedUpdate()
	{
		if (!(audioSource != null))
		{
			return;
		}
		OffAllRay = false;
		minDist = 100f;
		if (Physics.Linecast(posAudio, PositionListen.transform.position, out hit))
		{
			objcol = hit.collider;
			if (objcol.gameObject.GetComponent<Collider>().isTrigger)
			{
				objcol = null;
			}
		}
		if (objcol != null)
		{
			for (r = 0; r < PointsAngleSound.Length; r++)
			{
				if (!Physics.Linecast(PointsAngleSound[r], PositionListen.transform.position, out hit))
				{
					OffAllRay = true;
					if (Vector3.Distance(PointsAngleSound[r], PositionListen.position) < minDist)
					{
						minDist = Vector3.Distance(PointsAngleSound[r], PositionListen.position);
						pointNear = r;
					}
				}
			}
			ptSd = PointsAngleSound[pointNear];
			if (!OffAllRay)
			{
				minDist = 100f;
				for (r = 0; r < PointsAngleSound.Length; r++)
				{
					if (Vector3.Distance(PointsAngleSound[r], PositionListen.position) < minDist)
					{
						minDist = Vector3.Distance(PointsAngleSound[r], PositionListen.position);
					}
					if (Physics.Linecast(PointsAngleSound[r], PositionListen.transform.position, out hit) && Vector3.Distance(hit.point, PositionListen.position) < minDist)
					{
						minDist = Vector3.Distance(hit.point, PositionListen.position);
						ptSd = hit.point;
						pointNear = r;
						ptSd = Vector3.MoveTowards(ptSd, PointsAngleSound[r], Vector3.Distance(ptSd, PointsAngleSound[r]) / 2f);
					}
				}
			}
			ptSd = Vector3.MoveTowards(ptSd, posAudio, (Vector3.Distance(PointsAngleSound[pointNear], posAudio) / 2f - Vector3.Distance(PointsAngleSound[pointNear], PositionListen.position)) / (Vector3.Distance(PointsAngleSound[pointNear], PositionListen.position) / 1.5f));
			au.maxDistance = Mathf.Lerp(au.maxDistance, maxDistanceSource - Vector3.Distance(posAudio, ptSd) * DistanceDrob, 0.1f);
			sdk.cutoffFrequency = Mathf.Lerp(sdk.cutoffFrequency, 5000f - Vector3.Distance(PositionListen.position, ptSd) * 700f, 0.05f);
			audioSource.transform.position = Vector3.Lerp(audioSource.transform.position, ptSd, 0.05f);
		}
		else
		{
			au.maxDistance = Mathf.Lerp(au.maxDistance, maxDistanceSource, 0.1f);
			audioSource.transform.position = Vector3.Lerp(audioSource.transform.position, posAudio, 0.1f);
			sdk.cutoffFrequency = Mathf.Lerp(sdk.cutoffFrequency, 5000f, 0.1f);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (PointsAngleSound.Length != 0)
		{
			for (int i = 0; i < PointsAngleSound.Length; i++)
			{
				if (local)
				{
					if ((bool)audioSource)
					{
						Gizmos.color = new Vector4(0f, 1f, 0f, 0.8f);
						Gizmos.DrawLine(audioSource.transform.position, audioSource.transform.position + PointsAngleSound[i]);
						Gizmos.color = new Vector4(1f, 1f, 1f, 1f);
						Gizmos.DrawSphere(audioSource.transform.position + PointsAngleSound[i], 0.025f);
					}
					else
					{
						Gizmos.color = new Vector4(0f, 1f, 0f, 0.8f);
						Gizmos.DrawLine(base.transform.position, base.transform.position + PointsAngleSound[i]);
						Gizmos.color = new Vector4(1f, 1f, 1f, 1f);
						Gizmos.DrawSphere(base.transform.position + PointsAngleSound[i], 0.025f);
					}
					continue;
				}
				Gizmos.color = new Vector4(0f, 1f, 0f, 0.8f);
				if (gameGiz)
				{
					Gizmos.DrawLine(posAudio, PointsAngleSound[i]);
					if (audioSource == base.gameObject)
					{
						Gizmos.color = new Vector4(1f, 1f, 1f, 0.4f);
						Gizmos.DrawLine(base.transform.position, PointsAngleSound[i]);
						Gizmos.DrawSphere(posAudio, 0.025f);
					}
				}
				else if ((bool)audioSource)
				{
					Gizmos.DrawLine(audioSource.transform.position, PointsAngleSound[i]);
				}
				else
				{
					Gizmos.DrawLine(base.transform.position, PointsAngleSound[i]);
				}
				Gizmos.color = new Vector4(1f, 1f, 1f, 1f);
				Gizmos.DrawSphere(PointsAngleSound[i], 0.025f);
			}
		}
		Gizmos.color = new Vector4(1f, 1f, 0.2f, 1f);
		if (audioSource != null)
		{
			Gizmos.DrawSphere(audioSource.transform.position, 0.05f);
		}
		else
		{
			Gizmos.DrawSphere(base.transform.position, 0.05f);
		}
		if (PositionListen != null)
		{
			if (objcol != null)
			{
				Gizmos.color = new Vector4(1f, 0f, 0.2f, 1f);
			}
			else
			{
				Gizmos.color = new Vector4(0f, 0.8f, 1f, 1f);
			}
			Gizmos.DrawSphere(PositionListen.position, 0.05f);
			Gizmos.DrawLine(base.transform.position, PositionListen.position);
		}
		if (Physics.Linecast(posAudio, PositionListen.transform.position, out hit))
		{
			Gizmos.color = new Vector4(1f, 0f, 0.2f, 1f);
			Gizmos.DrawLine(base.transform.position + new Vector3(0f, 0.05f, 0f), hit.point);
		}
	}
}
