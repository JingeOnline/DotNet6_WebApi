using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSpeech
{
    public interface IAzureTextToSpeechService
    {
        Task<string> TextToSpeechAsync(string text);
    }
}
