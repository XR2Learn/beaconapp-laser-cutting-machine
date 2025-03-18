//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================


using System;
using System.Collections.Generic;
using System.Text.Json;
using StackExchange.Redis;
using UnityEngine;

namespace PersonalizationTool.Redis
{
	public class RedisManager
	{
		public event Action<NextActivityData> NewNextActivityData;

		private string m_redisIp;
		private int m_redisPort;

		private readonly string m_startActivityChannelName = "start_activity";
		private readonly string m_endActivityChannelName = "end_activity";

		private readonly string m_nextActivityChannelName = "next_activity_level";

		private ConnectionMultiplexer m_redis;
		private IDatabase m_db;
		private ISubscriber m_nextActivitySub;
		private readonly RedisChannel m_startActivityChannel;
		private readonly RedisChannel m_endActivityChannel;
		private readonly RedisChannel m_nextActivityChannel;

		// Define a class to represent the structure of your JSON object
		public class NextActivityData
		{
			public int id { get; set; }

			public int next_activity_level { get; set; }
			// Add other fields as needed
			
			public Dictionary<string, string> emotion_information { get; set; }
		}


		public RedisManager()
		{
			SetConnectionData();
			// Explicitly specify PatternMode when creating RedisChannel
			m_startActivityChannel = new RedisChannel(m_startActivityChannelName, RedisChannel.PatternMode.Literal);
			m_endActivityChannel = new RedisChannel(m_endActivityChannelName, RedisChannel.PatternMode.Literal);
			m_nextActivityChannel = new RedisChannel(m_nextActivityChannelName, RedisChannel.PatternMode.Literal);
		}

		public void SetConnectionData(string p_redisIp = "localhost", string p_redisPort = "6379")
		{
			m_redisIp = p_redisIp;
			m_redisPort = System.Convert.ToInt32(p_redisPort);
		}
		
		public void ConnectRedis()
		{
			Debug.Log($"Connecting to redis: {m_redisIp}:{m_redisPort}");
			m_redis = ConnectionMultiplexer.Connect($"{m_redisIp}:{m_redisPort}");
			m_db = m_redis.GetDatabase();
			m_nextActivitySub = m_redis.GetSubscriber();

			m_nextActivitySub.Subscribe(m_nextActivityChannel,
									  (p_channel, p_message) => { ProcessMessageNextActivityLevel((string)p_message); });
		}

		public void DisconnectRedis()
		{
			Debug.Log($"Disconnecting from redis: {m_redisIp}:{m_redisPort}");
			m_redis.Close();
		}

		public string GetRedisStatus()
		{
			return m_redis.GetStatus();
		}

		private void ProcessMessageNextActivityLevel(string p_message)
		{

			Debug.Log("ProcessMessageNextActivityLevel: " + p_message);

			try
			{
				// Deserialize the JSON string into an instance of YourJsonObject
				NextActivityData l_jsonObject = JsonSerializer.Deserialize<NextActivityData>(p_message);

				NewNextActivityData?.Invoke(l_jsonObject);
			}
			catch (JsonException l_ex)
			{
				// Handle JSON parsing errors
				Debug.Log($"Error parsing JSON: {l_ex.Message}");
			}

		}

		public void StartActivity(int p_activityId, int p_activityLevel, int p_userLevel)
		{
			if(!m_redis.IsConnected) return;
			
			var l_dataObject = new
			{
				id = p_activityId,
				user_level = p_userLevel,
				activity_level = p_activityLevel,
			};
			string l_jsonMessage = JsonSerializer.Serialize(l_dataObject);

			// Publish the JSON message to the specified channel
			m_db.Publish(m_startActivityChannel, l_jsonMessage);

			Debug.Log("StartActivity:  " + l_jsonMessage);
		}

		public void StopActivity(int p_activityId)
		{
			if (!m_redis.IsConnected) return;
			
			var l_dataObject = new
			{
				id = p_activityId,
				timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmssffff")
			};
			string l_jsonMessage = JsonSerializer.Serialize(l_dataObject);

			// Publish the JSON message to the specified channel
			m_db.Publish(m_endActivityChannel, l_jsonMessage);

		}

	}
}