using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveAI.Speech;

namespace Reactive
{
    public partial class Form1 : Form
    {
        SpeechRecognition speechRecognition = new SpeechRecognition();

        System.Timers.Timer updateTimer = new System.Timers.Timer(1000);

        public Form1()
        {
            InitializeComponent();
            updateTimer.Elapsed += Update;
            updateTimer.Start();
        }

        private void Update(object sender, System.Timers.ElapsedEventArgs e)
        {
            label1.Invoke((Action)delegate { label1.Text = SpeechRecognition.botReturnMessage; });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
