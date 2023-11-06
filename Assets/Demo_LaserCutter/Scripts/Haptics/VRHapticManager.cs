//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using SimpleJSON;
using System.IO;
using System.Text;
using XdeEngine.Core;
using VMachina;
using UnityEditor;

namespace VMachina
{
	public class VRHapticManager : Singleton<VRHapticManager>
	{
		public ConfigFileJSON configFile;

		public float publicFrequency;

		public SteamVR_Action_Vibration hapticAction;

		private string patternsFile = string.Empty;
		public List<PatternHaptic> patternsHaptic;

		//debug
		public SteamVR_Action_Boolean trackpadAction;
		private int idReading = 0;

		//Contact Monitoring
		private XdeCollisionMonitor collisionMonitor;


		protected override void Awake()
		{
			base.Awake();
		}

		// Start is called before the first frame update
		void Start()
		{
			XdeScene xdeScene = FindObjectOfType<XdeScene>();
			xdeScene.SimulationStateChangedEvent += StartConnection;
		
			patternsFile = Path.Combine( Application.dataPath,Path.Combine(Path.Combine("StreamingAssets","Haptics"),"patternsFile.json"));
			patternsHaptic = new List<PatternHaptic>();

			if (File.Exists(patternsFile))
			{
				ConfigFileJSON config = new ConfigFileJSON(patternsFile);
				ParseHapticFile(config);
			}
			else
			{
				Debug.LogError("[VRHapticManager] Start - File patternsFile.json is missing at path: "+patternsFile);
			}
		}

		//Subscribe to Collision Events
		public void StartConnection(SIMULATION_STATE simulationState)
		{
			if (simulationState == SIMULATION_STATE.ACTIVE)
			{
				collisionMonitor = FindObjectOfType<XdeCollisionMonitor>();
				if (collisionMonitor != null)
				{
					for (int i = 0; i < collisionMonitor.BodyColliders.Count; i++)
						collisionMonitor.BodyColliders[i].OnCollisionDetected += getCollisionData; 
				}
			}
		}

		// Update is called once per frame
		void Update()
		{
			//Debugging
			//if (trackpadAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
			//{
			//  Pulse(5, 150, 75, SteamVR_Input_Sources.LeftHand, 5);
			//}
			//if (trackpadAction.GetStateDown(SteamVR_Input_Sources.RightHand))
			//{
			//  Pulse(5, 150, 1, SteamVR_Input_Sources.RightHand);
			//  Pulse(5, 150, 0.2f, SteamVR_Input_Sources.RightHand, 5);
			//}
			//if (Input.GetKeyDown(KeyCode.Space))
			//{
			//  playPattern(idReading);
			//  idReading = (++idReading % patternsHaptic.Count);
			//}
		}

		private void getCollisionData(XdeBody touchedBody, XdeBody otherTouchedBody, float impactForce, Vector3 worldPosition)
		{
			///// Get contact info
			//bodyName = touchedBody.name;
			//otherBodyName = otherTouchedBody.name;
			//force = impactForce;
			//collisionPosition = worldPosition;

			if (otherTouchedBody.tag == "Cube" && touchedBody.tag == "Manikin")
			{
				playPattern(otherTouchedBody.GetComponent<TriggerVibration>().idPattern);
			}
		}


		/// <summary>
		/// Trigger the haptics at a certain time for a certain length
		/// </summary>
		/// <param name="duration">How long the haptic action should last (in seconds)</param>
		/// <param name="frequency">How often the haptic motor should bounce (0 - 320 in hz. The lower end being more useful)</param>
		/// <param name="amplitude">How intense the haptic action should be (0 - 1)</param>
		/// <param name="source">The device you would like to execute the haptic action. Any if the action is not device specific.</param>
		/// <param name="delay">How long from the current time to execute the action (in seconds - can be 0)</param>
		/// 
		public void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source, float delay = 0)
		{
			hapticAction.Execute(delay, duration, frequency, amplitude, source);
			Debug.Log("Vibration (" + source.ToString() + ")");
		}

		private IEnumerator lastroutine;
		public void playPattern(int id)
		{
			if (id < patternsHaptic.Count)
			{ 
				if (lastroutine != null)
					StopCoroutine(lastroutine);
				//StopAllCoroutines(); //stop previous launch
														 //patternsHaptic[id].playPattern(hapticAction);
				 var logBuilder = new StringBuilder();
				 logBuilder.Append("[VRHapticManager] playPattern - Play:" + patternsHaptic[id].Name+"\n");
				//Debug.Log("Play:" + patternsHaptic[id].Name);
				foreach (var pe in patternsHaptic[id].patternElements)
				{
					lastroutine = PlanHapticElement(hapticAction, pe.BeginTime, pe.Duration, pe.Frequency, pe.Amplitude,
						pe.Source);
					StartCoroutine(lastroutine);
					//Debug.Log("\t|->(" + pe.BeginTime + ", " + pe.Duration + ", " + pe.Frequency + ", " + pe.Amplitude + ", " + pe.Source.ToString() + ")");
					logBuilder.Append("\t|->(" + pe.BeginTime + ", " + pe.Duration + ", " + pe.Frequency + ", " + pe.Amplitude + ", " + pe.Source.ToString() + ")\n");
				}
				Debug.Log(logBuilder.ToString());
			}
		}

		private void ParseHapticFile(ConfigFileJSON config)
		{
			var logBuilder = new StringBuilder();
			
			if (config.Parameters["pattern_count"] != null)
			{
				logBuilder.Append("[VRHapticManager] ParseHapticFile - Added patterns : \n");
				
				int size = config.Parameters["pattern_count"].AsInt;
				PatternHaptic pat;
				for (int i = 1; i <= size; i++)
				{
					if (config.Parameters.HasKey("pattern_" + i))
					{
						SimpleJSON.JSONNode node = config.Parameters["pattern_" + i];
						string name = "defaultName";
						string desc = "";
						if (node["pattern_name"] != null)
						{
							name = node["pattern_name"];
						}
						if (node["pattern_desc"] != null)
						{
							desc = node["pattern_desc"];
						}
						pat = new PatternHaptic(name, desc);
						if (node["pattern"] != null)
						{
							float startTime = 0;
							JSONArray arr = node["pattern"].AsArray;
							JSONArray el;
							PatternElement pe;
							for (int p = 0; p < arr.Count; p++)
							{
								el = arr[p].AsArray;
								float dur = (el.Count >= 1) ? el[0].AsFloat : 0;
								float amp = (el.Count >= 2) ? el[1].AsFloat : 0;
								if (amp > 0) //add only the element with a amplitude not null
								{
									float freq = (el.Count >= 3) ? el[2].AsFloat : 0;
									string source = (el.Count >= 4) ? el[3].Value : "all";
									pe = new PatternElement(startTime, dur, amp, freq, source);
									pat.addPatternElement(pe);
								}
								startTime += dur;
							}
						}
						patternsHaptic.Add(pat);
						//Debug.Log("[VRHapticManager] ParseHapticFile - Pattern "+pat.Name+" add from file");
						logBuilder.Append("Pattern "+pat.Name+" add from file\n");
					}
				}
			}
		}
		public IEnumerator PlanHapticElement(SteamVR_Action_Vibration hapticAction, float beginTime, float duration, float amplitude, float frequency, SteamVR_Input_Sources source)
		{
			yield return new WaitForSeconds(beginTime);
			hapticAction.Execute(0, duration, amplitude, frequency, source);
		}
	}

	public class PatternHaptic
	{
		private string _name;
		private string _description;
		public List<PatternElement> patternElements;

		public string Name { get => _name; private set => _name = value; }
		public string Description { get => _description; private set => _description = value; }

		public PatternHaptic(string name, string description, List<PatternElement> patternElements)
		{
			_name = name;
			_description = description;
			this.patternElements = patternElements;
		}
		public PatternHaptic(string name, string description)
		{
			_name = name;
			_description = description;
			this.patternElements = new List<PatternElement>();
		}

		public void addPatternElement(PatternElement patternElement)
		{
			this.patternElements.Add(patternElement);
		}

		//not working because the SteamVR function dont use the first parameter
		public void playPattern(SteamVR_Action_Vibration hapticAction)
		{
			Debug.Log("[VRHapticManager] playPattern - Play:" + _name);
			foreach (var el in patternElements)
			{
				hapticAction.Execute(el.BeginTime, el.Duration, el.Frequency, el.Amplitude, el.Source);
				Debug.Log("\t|->(" + el.BeginTime + ", " + el.Duration + ", " + el.Frequency + ", " + el.Amplitude +
				          ", " + el.Source.ToString() + ")\n");
			}
		}
	}


	public class PatternElement
	{
		private float _beginTime;
		private float _duration;
		private float _amplitude;
		private float _frequency;
		private SteamVR_Input_Sources _source;

		public float BeginTime { get => _beginTime; private set => _beginTime = value; }
		public float Duration { get => _duration; private set => _duration = value; }
		public float Amplitude { get => _amplitude; private set => _amplitude = value; }
		public float Frequency { get => _frequency; private set => _frequency = value; }
		public SteamVR_Input_Sources Source { get => _source; private set => _source = value; }

		public PatternElement(float beginTime, float duration, float amplitude, float frequency, string source)
		{
			_beginTime = beginTime;
			_duration = duration;
			_amplitude = amplitude;
			_frequency = frequency;
			switch (source)
			{
				case "right":
					_source = SteamVR_Input_Sources.RightHand;
					break;
				case "left":
					_source = SteamVR_Input_Sources.LeftHand;
					break;
				case "default":
					_source = SteamVR_Input_Sources.Any;
					break;
			}
		}


	}
}
