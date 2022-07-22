using Microsoft.CognitiveServices.Speech;
using System.Text;

namespace AzureSpeech
{
    public class AzureTextToSpeechService:IAzureTextToSpeechService
    {
        private static readonly string _YourSubscriptionKey = "";
        private static readonly string _YourServiceRegion = "australiaeast";
        private static readonly string _VoiceName = "zh-CN-XiaoqiuNeural";
        //private static SpeechSynthesizer speechSynthesizer;
        SpeechConfig speechConfig;

        public AzureTextToSpeechService()
        {
            speechConfig = SpeechConfig.FromSubscription(_YourSubscriptionKey, _YourServiceRegion);
            speechConfig.SpeechSynthesisVoiceName = _VoiceName;
            //speechSynthesizer = new SpeechSynthesizer(speechConfig);
        }

        /// <summary>
        /// Convert the text to speech, if success, output to speaker.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The result of Azure speech service (use for logging)</returns>
        public async Task<string> TextToSpeechAsync(string text)
        {
            using (SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                SpeechSynthesisResult speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                return OutputSpeechResult(speechSynthesisResult, text);
            }
        }

        /// <summary>
        /// Out put the result for logging
        /// </summary>
        /// <param name="speechSynthesisResult"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string OutputSpeechResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            string result=string.Empty;
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    result=$"Success speak text: [{text}]";
                    break;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"CANCELED: Reason={cancellation.Reason}");
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        stringBuilder.AppendLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        stringBuilder.AppendLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        stringBuilder.AppendLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    result = stringBuilder.ToString();
                    break;
                default:
                    break;
            }
            return result;
        }



    }
}