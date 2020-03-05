using Data.Core;
using System;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace SpeechApi.Models
{
    public class TextToSpeech
    {
        public Stream GetAudio(string text, LanguageEnum language)
        {
            var memoryStream = new MemoryStream();
            using (var synth = new SpeechSynthesizer())
            {
                SelectVoice(synth, language);
                synth.SetOutputToWaveStream(memoryStream);
                synth.Speak(text);
                synth.SetOutputToNull();
            }
            memoryStream.Position = 0;
            return memoryStream;
        }
        public void SaveAudio(string text, LanguageEnum language, string path)
        {
            using (var synth = new SpeechSynthesizer())
            {
                SelectVoice(synth, language);
                synth.SetOutputToWaveFile(path);
                synth.Speak(text);
                synth.SetOutputToNull();
            }
        }

        public async Task<Stream> GetAudioAsync(string text, LanguageEnum language)
        {
            return await Task.Run(() => GetAudio(text, language));
        }
        public async Task SaveAudioAsync(string text, LanguageEnum language, string path)
        {
            await Task.Run(() => SaveAudio(text, language, path));
        }

    

        private void SelectVoice(SpeechSynthesizer synth, LanguageEnum language)
        {
            synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.NotSet, 0, language.GetCulture());
        }

       

    }
}