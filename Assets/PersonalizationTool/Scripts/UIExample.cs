
using System.Text.Json;



using StackExchange.Redis;


using UnityEngine;

using TMPro;

public class UIExample : MonoBehaviour
{

    public TMP_Dropdown activityLevelDropdown;
    public TMP_Dropdown userLevelDropdown;
    public TMP_Dropdown suggestedLevelDropdown;
    public TMP_InputField redisConnectionInput;
    public TMP_Text connectionStatusText;
    public int activityLevel;
    public int userLevel;
    public int suggestedLevel = -1;

    public string redisHost = "localhost";
    public int redisPort = 6379;

    public string startActivityChannelName = "start_activity";
    public string endActivityChannelName = "end_activity";

    public string nextActivityChannelName = "next_activity_level";

    private ConnectionMultiplexer redis;
    private IDatabase db;
    private ISubscriber nextActivitySub;
    private RedisChannel startActivityChannel;
    private RedisChannel endActivityChannel;
    private RedisChannel nextActivityChannel;

    // Define a class to represent the structure of your JSON object
    private class NextActivityJsonObj
    {
        public int id { get; set; }
        public int next_activity_level { get; set; }
        // Add other fields as needed
    }

    // Start is called before the first frame update
    void Start()
    {
        connectionStatusText.SetText("Disconnected");

        // Explicitly specify PatternMode when creating RedisChannel
        startActivityChannel = new RedisChannel(startActivityChannelName, RedisChannel.PatternMode.Literal);
        endActivityChannel = new RedisChannel(endActivityChannelName, RedisChannel.PatternMode.Literal);
        nextActivityChannel = new RedisChannel(nextActivityChannelName, RedisChannel.PatternMode.Literal);

        redisConnectionInput.text = $"{redisHost}:{redisPort}";
        // redisConnectionInput.textComponent.text = $"{redisHost}:{redisPort}";
        Debug.Log("done start");
    }

    public void ConnectRedis(){
        redisHost = redisConnectionInput.text.ToString().Split(':')[0];
        redisPort = System.Convert.ToInt32(redisConnectionInput.text.ToString().Split(':')[1]);
        redis = ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}");
        db = redis.GetDatabase();
        nextActivitySub = redis.GetSubscriber();

        nextActivitySub.Subscribe(nextActivityChannel, (channel, message) => {
            ProcessMessageNextActivityLevel((string)message);
        });

        connectionStatusText.SetText("Connected: " + redis.GetStatus());
        Debug.Log("done connect Redis");
    }
    public void DisconnectRedis(){
        redis.Close();
        connectionStatusText.SetText("Disconnected: " + redis.GetStatus());
        Debug.Log("disconnect Redis");
    }

    public void ProcessMessageNextActivityLevel(string message){

        Debug.Log("ProcessMessageNextActivityLevel: " + message);

        try{
            // Deserialize the JSON string into an instance of YourJsonObject
            NextActivityJsonObj jsonObject = JsonSerializer.Deserialize<NextActivityJsonObj>(message);

            suggestedLevel = jsonObject.next_activity_level;
        }
        catch (JsonException ex)
        {
            // Handle JSON parsing errors
            Debug.Log($"Error parsing JSON: {ex.Message}");
        }

    }
    public void StartActivity(){
        activityLevel = activityLevelDropdown.value;
        userLevel = userLevelDropdown.value;


        var dataObject = new {
             id = 0,
             user_level = userLevel,
             activity_level = activityLevel,
        };
        string jsonMessage = JsonSerializer.Serialize(dataObject);

        // Publish the JSON message to the specified channel
        db.Publish(startActivityChannel, jsonMessage);

        connectionStatusText.SetText("Pub: Activity start");
        Debug.Log("StartActivity:  " + jsonMessage);
    }

    public void StopActivity(){
        var dataObject = new {
             id = 0,
             timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmssffff")
        };
        string jsonMessage = JsonSerializer.Serialize(dataObject);

        // Publish the JSON message to the specified channel
        db.Publish(endActivityChannel, jsonMessage);

        connectionStatusText.SetText("Pub: Activity Stop");
        Debug.Log("StopActivity  [" + endActivityChannelName + "]" + jsonMessage);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("new level:" + suggestedLevel as string);
        if (suggestedLevelDropdown != null && suggestedLevel > -1)
        {
            suggestedLevelDropdown.value = suggestedLevel;
        }
        else
        {
            // Debug.LogError("suggestedLevelDropdown is not assigned.");
        }
    }
}
