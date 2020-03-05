using System;
using System.IO;
using System.Reflection;
using Data.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeechApi.Models;

namespace SpeechApi.UnitTests
{
    [TestClass]
    public class SpeachLogicTest
    {
        [TestMethod]
        public void GetAudioFromTextAndGetTextFromAudio_EnglishWord_GetSameText()
        {
            var text = "Dog";
            var language = LanguageEnum.English;

            var textToSpeech = new TextToSpeech();
            var stream = textToSpeech.GetAudio(text, language);


            var speechToText = new SpeechToText();
            var result = speechToText.GetText(stream);

            Assert.AreEqual(text, result);
        }


        [TestMethod]
        public void GetAudioFromTextAndGetTextFromAudio_EnglishSentence_GetSameText()
        {
            var text = "I can speak English fluently";
            var language = LanguageEnum.English;

            var textToSpeech = new TextToSpeech();
            var stream = textToSpeech.GetAudio(text, language);


            var speechToText = new SpeechToText();
            var result = speechToText.GetText(stream);

            Assert.AreEqual(text, result);
        }


        [TestMethod]
        public void GetTextFromAudioFile_RecordWithSpeechToText_ReturnRecordedWord()
        {
            var speechToText = new SpeechToText();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\resources\SpeechSynthesizerSoundTest.wav");

            string result;
            using (var stream = File.Open(path, FileMode.Open))
            {
                result = speechToText.GetText(stream);
            }

            string expectedResult = "Test";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        //форматы отличные от wav еще не поддерживаются
        public void GetTextFromAudioFile_RecordWithWindowsApplication_ReturnRecordedWord()
        {
            var speechToText = new SpeechToText();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\resources\WindowsRecordSoundCat.m4a");

            string result;
            using (var stream = File.Open(path, FileMode.Open))
            {
                result = speechToText.GetText(stream);
            }

            string expectedResult = "Cat";
            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public void GetTextFromAudioFile_RecordWithWindowsApplicationAndRecoded_ReturnRecordedWord()
        {
            var speechToText = new SpeechToText();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\resources\WindowsRecordSoundCat.wav");

            string result;
            using (var stream = File.Open(path, FileMode.Open))
            {
                result = speechToText.GetText(stream);
            }

            string expectedResult = "Cat";
            Assert.AreEqual(expectedResult, result);
        }

    }
}
