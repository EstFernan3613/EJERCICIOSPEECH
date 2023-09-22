using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class STManager : MonoBehaviour
{

    [SerializeField] private string lenguaje = "es-ES";

    [SerializeField] private Text txtUI;

    [Serializable]

    public struct VoiceCommand
    {
        public string keyword;
        public UnityEvent response;
    }

    public VoiceCommand[] voiceCommand;

    private Dictionary<string, UnityEvent> commands = new Dictionary<string, UnityEvent>();

    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        foreach (var command in voiceCommand)
        {
            commands.Add(command.keyword.ToLower(), command.response);
        }
    }

    public void StartListening()
    {
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }

    public void OnFinalSpeechResult(string resultado)
    {
        txtUI.text = resultado;

        if(resultado != null)
        {
            var respuesta = commands[resultado.ToLower()];

            if(respuesta != null)
            {
                respuesta?.Invoke();
            }
        }
    }

    public void OnPartialSpeechResult(string resultado)
    {
        txtUI.text = resultado;
    }

    public void StartSpeaking(string mensaje)
    {
        TextToSpeech.Instance.StartSpeak(mensaje);
    }

    public void StopSpeaking(string mensaje)
    {
        TextToSpeech.Instance.StopSpeak();
    }

    public void OnSpeakStart()
    {
        Debug.Log("Escuchando...");
    }

    public void OnSpeakStop()
    {
        Debug.Log("Cerrando Escucha...");
    }


    // Start is called before the first frame update
    void Start()
    {
        TextToSpeech.Instance.Setting(lenguaje, 0.5f, 0.8f);
        SpeechToText.Instance.Setting(lenguaje);

        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;

        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
    }

}
