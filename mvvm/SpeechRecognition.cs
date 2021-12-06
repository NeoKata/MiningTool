using Mining_Tool_3.Model;
using System.ComponentModel;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Threading;

namespace Mining_Tool_3.mvvm
{
    public class SpeechRecognition
    {
        static ManualResetEvent _completed = null;
        private BackgroundWorker _worker = new BackgroundWorker();

        SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine
            (new System.Globalization.CultureInfo("de-DE"));

        public SpeechRecognition()
        {
            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.LoadGrammar(DefaultGrammar());
            recognitionEngine.SpeechRecognized += RecognitionEngine_SpeechRecognized;
            _completed = new ManualResetEvent(false);
            _worker.DoWork += _worker_DoWork;
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            _completed.WaitOne();
            recognitionEngine.RecognizeAsyncStop();
        }

        public void start()
        {
            _completed.Reset();
            _worker.RunWorkerAsync();
        }

        private void RecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var text = ReplacementText(e.Result.Text);

            Messenger.Instance.Send(text, "SpeechRecognition");
        }

        private string ReplacementText(string text)
        {         
            foreach (Element element in Element.ByValues)
            {
                if (text.Contains(element.GermanSpeech))
                {
                    return text.Replace(element.GermanSpeech, element.Name);
                }               
            }           
            return text;
        }

        private Grammar DefaultGrammar()
        {

            var choices = new Choices();
            choices.Add("Meining");
            choices.Add("Raffinerie");
            choices.Add("Neuer Stein");


            var numbers = new Choices();
            for (int i = 0; i <= 99; i++)
            {
                numbers.Add(i.ToString());
            }
            numbers.Add("Löschen");

            var power = new Choices(new string[] { "Tausend", "Hundert" });
            var power2 = new Choices(new string[] { "Hundert" });

            GrammarBuilder massPhrase = new GrammarBuilder("Masse");
            massPhrase.Append(numbers, 0, 1);
            massPhrase.Append(power, 0, 1);
            massPhrase.Append(numbers, 0, 1);
            massPhrase.Append(power2, 0, 1);
            massPhrase.Append(numbers, 0, 1);
            choices.Add(
              massPhrase
                );

            var komma = new Choices(new string[] { "Komma" });
            var prozent = new Choices(new string[] { "Prozent" });

            foreach (Element element in Element.ByValues)
            {
                GrammarBuilder elementPhrase = new GrammarBuilder(element.GermanSpeech);
                elementPhrase.Append(numbers, 0, 1);
                elementPhrase.Append(komma, 0, 1);
                elementPhrase.Append(numbers, 0, 2);
                elementPhrase.Append(prozent, 0, 1);
                choices.Add(elementPhrase);
            }

            return new Grammar(new GrammarBuilder(choices))
            { Name = "StoneGrammar" };
        }

        internal void stop()
        {
            _completed.Set();
        }
    }
}
