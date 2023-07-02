using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using System;
// using System.Text;
using UnityEngine.UI;

public class AppMaster : MonoBehaviour
{
    #region
    [System.Serializable]
    public class MessageModel
    {
        public string role;
        public string content;
    }
    [System.Serializable]
    public class CompletionRequestModel
    {
        public string model;
        public List<MessageModel> messages;
    }

    [System.Serializable]
    public class ChatGPTUdemyModel
    {
        public string id;
        public string @object;
        public int created;
        public Choice[] choices;
        public Usage usage;

        [System.Serializable]
        public class Choice
        {
            public int index;
            public MessageModel message;
            public string finish_reason;
        }

        [System.Serializable]
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }
    #endregion

    public Text GPTChat_Text;

    private MessageModel assistantModel = new(){
        role = "system",
        content = "you are professional engineer"
    };
    private readonly string apiKey = "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";
    private List<MessageModel> communicationHistory = new();

    void Start()
    {
        communicationHistory.Add(assistantModel);
        MessageSubmit("Hello, What your name??");
    }

    private void Message(string newMessage, Action<MessageModel> result)
    {
        Debug.Log(newMessage);
        communicationHistory.Add(new MessageModel()
        {
            role = "user",
            content = newMessage
        });

        var apiUrl = "https://api.openai.com/v1/chat/completions";
        var jsonOptions = JsonUtility.ToJson(
            new CompletionRequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = communicationHistory
            }, true);
        var headers = new Dictionary<string, string>(){
            {"Authorization", "Bearer " + apiKey},
            {"Content-type", "application/json"},
            {"X-Slack-No-Retry", "1"}
        };
        var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        var operation = request.SendWebRequest();

        operation.completed += _ =>
        {
            if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                       operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(operation.webRequest.error);
                throw new Exception();
            }
            else
            {
                var responseString = operation.webRequest.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTUdemyModel>(responseString);
                communicationHistory.Add(responseObject.choices[0].message);
                GPTChat_Text.text = responseObject.choices[0].message.content;
            }
            request.Dispose();

        };
    }

    public void MessageSubmit(string sendMessage)
    {
        Message(sendMessage, (result) =>
        {
            GPTChat_Text.text = result.content;
        });
    }
}