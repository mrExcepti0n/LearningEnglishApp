using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Threading.Tasks;
using System.Web;

namespace SpeechApi.Models
{
    public class SpeechToText
    {

        private Grammar _grammar;
         
        public SpeechToText()
        {
            _grammar = new DictationGrammar();
            _grammar.Name = "Dictation Grammar";
        }

        public string GetText(Stream stream)
        {
            return Recognize(stream);
        }

        public async Task<string> GetTextAsync(Stream stream)
        {
            return await  Task.Run(() => GetText(stream));
        }

        private string Recognize(Stream stream)
        {
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(GetRecognizerInfo()))
            {
                recognizer.LoadGrammar(_grammar);
                SetRegcognizerInput(recognizer, stream);
              
                var result = recognizer.Recognize();

                //async version
                //recognizer.SpeechRecognized +=  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                //recognizer.RecognizeCompleted +=  new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);

                return result?.Text;
            }
        }

        protected void SetRegcognizerInput(SpeechRecognitionEngine recognizer, Stream stream)
        {
            //var fmt = new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Sixteen, AudioChannel.Mono);
            recognizer.SetInputToWaveStream(stream);
        }


        private RecognizerInfo GetRecognizerInfo()
        {
            var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            return recognizers.First();
        }
    }
}