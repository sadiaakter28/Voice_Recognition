using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;      //will allow us to creat a topping voice(Text to Speech)
using System.Speech.Recognition;    //will allow the computer to recognize our voice
using System.Threading;             //take support of the recognization

namespace Speaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Form declaration....
        SpeechSynthesizer sSynth = new SpeechSynthesizer(); /*SpeechSynthesizer: provides access to the functionalities of an 
        installed speech synthesis engine*/
        
        PromptBuilder pBuilder = new PromptBuilder(); /*tells it what to speack, PromptBuilder: PromptBuilder is a class. 
        creats an empty prompt object & provides methods for adding content, selecting voices, controlling voice attributes & 
        also it is used for controlling the pronounciations of spoken words*/
        
        SpeechRecognitionEngine sRecognize = new SpeechRecognitionEngine(); /*SpeechRecognitionEngine: provides the means to access 
        & manage an In-Process Speech recognition engine. speech recognizer will recognize phrases*/

        private void button1_Click(object sender, EventArgs e)
        {
            pBuilder.ClearContent(); /*clear the content that is being spoken or has been into the variable*/
            pBuilder.AppendText(textBox1.Text); /**/
            sSynth.Speak(pBuilder);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;    //cannot click start again
            button3.Enabled = true;
            //set the grammar so to speak & this is for speech recognizer it will only recognize the words that you type in the code
            Choices sList = new Choices(); /*Choices: represents a set if alternatives in the constraints of a speech recognition grammar*/
            sList.Add(new string[] { "hello alex","what is your name","print my name","test","open chrome", "it works", "thank you",
                "what is the current time", "how are you", "today i am fine","exit", "close", "quit", "so",
                " hello "," alex "," what "," is "," your "," name ", " open "," mozilla ", " print ", " my ", " name "," test ",
                " open " ," chrome ", "it"," works", "thank"," you","what"," is"," the"," current"," time", "how"," are"," you",
                "today"," i"," am"," fine","exit", "quit", "so" });
            Grammar gr = new Grammar(new GrammarBuilder(sList));
            //for prevent error.....
            try
            {
                //get recognizing.....
                sRecognize.RequestRecognizerUpdate();       //updating the detailes 
                sRecognize.LoadGrammar(gr);     //Load grammar 
                sRecognize.SpeechRecognized += SRecognize_SpeechRecognized; /*if my system reognize this word it rises an event SpeechRecognized*/
                sRecognize.SetInputToDefaultAudioDevice();      //set the default audio device, which microphone to use 
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);      //recognize my word

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void SRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "hello alex":
                    sSynth.SpeakAsync("hello Sadia how are you");
                    break;
                case "what is your name":
                    sSynth.SpeakAsync("i am alex");
                    break; 
                case "how are you":
                    sSynth.SpeakAsync("i am doing great sadia how about you");
                    break;
                case "what is the current time":
                    sSynth.SpeakAsync("current is " + DateTime.Now.ToLongTimeString());
                    break;
                case "thank you":
                    sSynth.SpeakAsync("pleasure is mine sadia");
                    break;
                case "open chrome":
                    System.Diagnostics.Process.Start("chrome", "https://www.google.com/");
                    break;
                case "open mozilla":
                    System.Diagnostics.Process.Start("mozilla", "https://www.mozilla.com");
                    break;
                case "close":
                    Application.Exit();
                    break;
            }

            if (e.Result.Text == "exit")
            {
                Application.Exit();
            }
            else
            {
                //this will append the text or add it to what it is plus 
                textBox1.Text = textBox1.Text + "" + e.Result.Text.ToString() + Environment.NewLine;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sRecognize.RecognizeAsyncStop();        // stop recognition
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}