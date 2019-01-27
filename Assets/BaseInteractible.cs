using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseInteractible : MonoBehaviour, IInteractible
{
	public static class States
	{
		public static ObjectState Broken = new ObjectState { Name = "Broken" };
		public static ObjectState Good = new ObjectState { Name = "Good" };
	}

	public string ObjectName;
	public bool Breakable = false;

	public Interaction CurrentInteraction;
	public PlayerStatus CurrentPlayer;
	public float interactionTimeout;

	public ObjectState State = States.Good;
	public Transform lockPosition;
	public AudioSource AudioBreak;
	public AudioSource AudioFixing;
	public AudioSource AudioPee;
	public AudioSource AudioShower;

	//private MeshRenderer[] myRenderers;
	private Interaction[] interactions;
	private Household household;
	private ParticleSystem ps;
	private WorkProgress ui;
	public bool alreadyBeingUsed(PlayerStatus status) => CurrentPlayer != status && interactionTimeout > 0;

	public string Name => ObjectName;

	void Start()
	{
		//myRenderers = GetComponentsInChildren<MeshRenderer>();
		ui = GetComponent<WorkProgress>();
		interactions = GetComponents<Interaction>();
		household = FindObjectOfType<Household>();
		ps = GetComponentInChildren<ParticleSystem>();
		ps?.gameObject.SetActive(false);
	}

	void Update()
	{
		if (!CurrentInteraction?.Continuous ?? true)
			interactionTimeout -= Time.deltaTime;

		if (interactionTimeout < 0)
		{
			CurrentPlayer = null;
		}

		//myRenderer.material.color = State == States.Broken ? Color.red : Color.blue;
		if (null != ui)
		{
			ui.FillAmount = (CurrentInteraction != null && !CurrentInteraction.Continuous)
				? Mathf.Clamp(CurrentInteraction.WorkDone / CurrentInteraction.WorkRequired, 0, 1)
				: 0;
		}
	}

	public Interaction[] GetInteractions()
	{
		return State == States.Broken
			? interactions.Where(x => x.Name == "Fix").ToArray()
			: interactions.Where(x => x.Name != "Fix").ToArray();
	}

	public bool Interact(PlayerStatus status, Interaction type, float workAmount)
	{
		if (alreadyBeingUsed(status))
		{
			return false;
		}

		if (type != CurrentInteraction)
		{
			CurrentInteraction?.ResetToDefaults();
			if (household.Money >= type.Cost && status.stressBalance > type.StressCost)
			{
				household.Money -= type.Cost;
				status.stress += type.StressCost;
				CurrentInteraction = type;
				CurrentPlayer = status;

				if (CurrentInteraction.Name == "Fix")
				{
					if (AudioFixing != null)
						AudioFixing.Play();
				} else if (CurrentInteraction.Name == "Pee")
				{
					if (AudioPee != null)
						AudioPee.Play();
				}else if (CurrentInteraction.Name == "Wash")
				{
					if(AudioShower != null)
						AudioShower.Play();
				}
			}
			else
			{
				return false;
			}
		}
		interactionTimeout = 1;

		CurrentInteraction?.DoWork(status, workAmount);

		if (CurrentInteraction?.Done ?? false)
		{
			EndCleanupInteraction(status);
			return true;
		}

		return false;
	}

	private void EndCleanupInteraction(PlayerStatus status)
	{
		if (Random.value < 0.2f) Break();

		CurrentInteraction.FinishWork(status);
		if (CurrentInteraction.Name == "Fix")
		{
			State = States.Good;
			ps?.gameObject.SetActive(false);

			if (AudioBreak != null)
				StartCoroutine(FadeOut(AudioBreak, 1f));

			if (AudioFixing != null)
				StartCoroutine(FadeOut(AudioFixing, 0.5f));
		}
		StopSFX(CurrentInteraction);
		CurrentInteraction.ResetToDefaults();
		CurrentInteraction = null;
		CurrentPlayer = null;
	}

	public virtual void Break()
	{
		if (Breakable)
		{
			State = States.Broken;
			ps?.gameObject.SetActive(true);
			if (AudioBreak != null)
				AudioBreak.Play();
		}
	}

	public void StopInteracting(PlayerStatus status)
	{
		if (status != CurrentPlayer) return;

		if (CurrentInteraction?.Continuous ?? false)
		{
			EndCleanupInteraction(status);
		}
		else if (CurrentInteraction.Name == "Fix")
		{
			if (AudioFixing != null)
				StartCoroutine(FadeOut(AudioFixing, 0.5f));
		}
		StopSFX(CurrentInteraction);
		CurrentInteraction = null;
		CurrentPlayer = null;
	}

	private IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			Debug.Log(audioSource.volume);
			yield return null;
		}
		audioSource.Stop();
		audioSource.volume = startVolume;
	}

	private void StopSFX(Interaction inter)
	{
		if (inter.Name == "Pee" && AudioPee != null)
			StartCoroutine(FadeOut(AudioPee, 0.5f));
		if (inter.Name == "Wash" && AudioShower != null)
			StartCoroutine(FadeOut(AudioShower, 0.5f));

	}
}
