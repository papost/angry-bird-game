using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;


namespace atomikhergasia
{
    public partial class Form1 : Form
    {
        int max_easy = 0;
        int max_hard = 0;
        int score = 0;
        bool easy = true;//metavliti type bool gia ton kathorismo tou epipedou easy
        bool difficult = false;//metavliti type bool gia ton kathorismo tou epipedou difficult
        int counter = 60;//xronos paixnidiou se seconds
        bool pause = false;//metavliti type bool gia to koumpi to ean o xronos einai stamathmenos
        bool play = false;//prokeitai gia mia bool metavlhth h opoia de epitrepei ston xrhsth na allaxei epipedo diskolias
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        WindowsMediaPlayer player1 = new WindowsMediaPlayer();
        WindowsMediaPlayer player2 = new WindowsMediaPlayer();



        public Form1()
        {
            InitializeComponent();
            player.URL = "playtrack.mp3";
        }

      
        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MADE BY APOSTOLOPOULOS PANAGIOTIS=>P17007", "INFO");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileInfo f1 = new FileInfo("easyscore.txt");
            if (f1.Length != 0)//ta parakatw isxuon mono an to arxeio den einai keno
            {
                richTextBox2.LoadFile("easyscore.txt", RichTextBoxStreamType.PlainText);
                List<string> onomata = new List<string>();//dhmiourgeitai lista type string gia ta onomata
                List<int> scores = new List<int>();//dhmiourgeitai lista type int gia ta score
                foreach (string n in richTextBox2.Lines)
                {
                    string[] tmp = n.Split(new string[] { " SCORE : " }, StringSplitOptions.None);
                    onomata.Add(tmp[0]);
                    int numb = Convert.ToInt32(tmp[1]);
                    scores.Add(numb);
                }
                for (int i = 0; i < scores.Count; i++)//loop gia ton arithmo twn score pou exoun katagrafei
                {
                    if (i == 0)
                    {
                        max_easy = scores[i];
                    }
                    if (scores[i] > max_easy)
                    {
                        max_easy = scores[i];
                    }
                    int pos = scores.FindIndex(x => x == max_easy);
                    label6.Text = onomata[pos];
                    label7.Text = "SCORE:" + max_easy.ToString();
                }
            }
            FileInfo f2 = new FileInfo("difficultscore.txt");
            if (f2.Length != 0)
            {
                richTextBox2.LoadFile("difficultscore.txt", RichTextBoxStreamType.PlainText);
                List<string> onomata = new List<string>();
                List<int> scores = new List<int>();
                foreach (string n in richTextBox2.Lines)
                {
                    string[] tmp = n.Split(new string[] { " SCORE : " }, StringSplitOptions.None);
                    onomata.Add(tmp[0]);
                    int numb = Convert.ToInt32(tmp[1]);
                    scores.Add(numb);
                }
                for (int i = 0; i < scores.Count; i++)
                {
                    if (i == 0)
                    {
                        max_hard = scores[i];
                    }
                    if (scores[i] > max_hard)
                    {
                        max_hard = scores[i];
                    }
                    int pos = scores.FindIndex(x => x == max_hard); //vriskei se poia thesi vrisketai to highscore 
                    label9.Text = onomata[pos];//grafei sto label to onoma me vash thn apo panw "ereuna"

                    label10.Text = "SCORE:" + max_hard.ToString();
                }
                pictureBox1.ImageLocation = "angry.gif";
                player.controls.play();
                
            }
        }

        private void pLAYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            play = true;
            player.controls.stop();
            richTextBox2.Visible = false;
            if (richTextBox1.Text == String.Empty)//check gia keno richtextbox,vasikh proupothesi gia na xekinhsei to paixnidi
            {
                MessageBox.Show("Please enter a name", "ERROR");
            }
            else
            {
               
                button1.Visible = true;
                if (easy)
                {
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    timer2.Interval = 1000;
                    label2.Text = "TIME :" + counter.ToString() + "sec";
                    pause = true;
                    pictureBox1.Visible = true;
                    //pictureBox2.Visible = false;


                }
                else if (difficult)
                {
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    timer2.Interval = 1000;
                    label2.Text = "TIME :" + counter.ToString() + "sec";
                    pause = true;
                    pictureBox1.Visible = true;
                    

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pause && easy)
            {
                score += 20;
                label3.Text = " SCORE:" + score.ToString();
                player1.URL = "ouch2.mp3";
                player1.controls.play();
            }
            else if (pause && difficult)
            {
                score += 50;
                label3.Text = " SCORE: " + score.ToString();
                player2.URL = "ouch4.mp3";
                player2.controls.play();
            }
           
        }

        private void eASYToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (play && difficult)//check gia to an paizei to paixnidi opote na mhn mporei na allaxei level
            {
                easy = false;
                difficult = true;

            }
            else
            {
                easy = true;
                difficult = false;
                MessageBox.Show("YOUR LEVEL EASY");
            }
            }

        private void dIFFICULTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (play && easy)//check gia to an paizei to paixnidi opote na mhn mporei na allaxei level
            {
                easy = true;
                difficult = false;
            }
            else
            {
                easy = false;
                difficult = true;

                MessageBox.Show("YOUR LEVEL IS DIFFICULT");
            }
         }

        private void timer2_Tick(object sender, EventArgs e)// ypeythinos gia ton xrono tou paixnidiou
        {
            counter--;
            label2.Text = "TIME: " + counter.ToString() + "sec";//grafei sto label ton xrono pou apomenei ston xrhsth
            if (counter == 0)
            {
                play = false;
                timer2.Stop();
                timer1.Stop();
                if (easy)//otan teleiwsei o xronos gia to eukolo level
                {
                    FileInfo f1 = new FileInfo("easyscore.txt");//dhmiourgeitai ena object ths fileinfo(f1) gia to easyscore.txt
                    StreamWriter sw = new StreamWriter("easyscore.txt", true);
                    if (f1.Length == 0)
                    {
                        sw.Write("NAME:" + richTextBox1.Text + " SCORE : " + score);//grafei sto arxeio to onoma kai dipla to score an einai keno to arxeio
                    }
                    else if (f1.Length != 0)
                    {
                        sw.Write(Environment.NewLine + "NAME:" + richTextBox1.Text + " SCORE : " + score);//alliws prvta nea grammh kai meta onoma kai score
                    }
                    sw.Close();//kleinei to arxeio
                    if (score > max_easy)//check gia neo highscore
                    {
                        label6.Text = "NAME:" + richTextBox1.Text;
                        label7.Text = "SCORE:" + score;
                        MessageBox.Show("CONGRATULATIONS  NEW HIGHSCORE: " + score);
                    }

                }
                else if (difficult)//otan teleiwsei o xronos gia to eukolo level
                {
                    FileInfo f2 = new FileInfo("difficultscore.txt");//dhmiourgeitai ena object ths fileinfo(f2) gia to difficultscore.txt
                    StreamWriter sw = new StreamWriter("difficultscore.txt", true);
                    if (f2.Length == 0)
                    {
                        sw.Write("NAME:" + richTextBox1.Text + " SCORE : " + score);// grafei sto arxeio to onoma kai dipla to score an einai keno to arxeio
                    }
                    else if (f2.Length != 0)
                    {
                        sw.Write(Environment.NewLine + "NAME:" + richTextBox1.Text + " SCORE : " + score);//alliws prwta nea grammh kai meta onoma kai score
                    }
                    sw.Close();//kleinei to arxeio
                    if (score > max_hard)//check gia neo highscore
                    {
                        label9.Text = "NAME:" + richTextBox1.Text;
                        label10.Text = "SCORE:" + score;
                        MessageBox.Show("CONGRATULATIONS  NEW HIGHSCORE: " + score);
                    }

                }
                DialogResult p = MessageBox.Show("DO YOU WANT TO TRY AGAIN?", "GAME OVER", MessageBoxButtons.YesNo);//dialog gia to an thelei na paixei pali o xrhsths
                if (p == DialogResult.Yes)//periptwsh yes
                {
                    player.controls.play();
                    counter = 60;//counter sta 60
                    score = 0;//score 0
                    label2.Text = "TIME: " + counter.ToString()+"sec";
                    label3.Text = "SCORE:" + score.ToString();
                    pictureBox1.Visible = false;
                    richTextBox2.Visible = false;
                    button1.Visible = false;

                }
                else if (p == DialogResult.No)//periptwsh oxi
                {
                    player.controls.play();
                    richTextBox1.Clear();//katharizei to richtextbox gia to name
                    counter = 60;
                    score = 0;
                    label2.Text = "TIME: " + counter.ToString()+ "sec";
                    label3.Text = "SCORE:" + score.ToString();
                    pictureBox1.Visible = false;
                    richTextBox2.Visible = false;
                    button1.Visible = false;
                }
            }
        }

        private void eASYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Visible = true;//emfanizetai to richtextbox2 pou emfanizei ta score
            pictureBox1.Visible = false;
            richTextBox2.LoadFile("easyscore.txt", RichTextBoxStreamType.PlainText);//fortonei to arxeio me ta score tou  easylevel me thn katallhlh morfh
        }

        private void dIFFICULTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Visible = true;
            pictureBox1.Visible = false;
            richTextBox2.LoadFile("difficultscore.txt", RichTextBoxStreamType.PlainText);//fortonei to arxeio me ta score tou  difficultlevel e thn katallhlh morfh
        }

        private void timer1_Tick(object sender, EventArgs e)//o timer einai ypeuthinos gia thn kinisi tou picturebox
        {
            Random r = new Random();//'gennitria' random numbers
            int i = this.Width;
            int z = this.Height;
            int x = r.Next(8, i - 96);
            int y = r.Next(128, z - 165);//128 to y toy panel - to megethos toy picturebox -10
            pictureBox1.Location = new Point(x, y);// coordnations tou picturebox1
            if (easy)
            {
                timer1.Interval = 1000;
            }
            else if (difficult)
            {
                timer1.Interval = 600;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)//kpumpi gia pause/resume
        {
            if (pause)
            {
                timer1.Stop();
                timer2.Stop();
                pause = false;
                button1.Text="RESUME";

            }
            else if (!pause)
            {
                pause = true;
                button1.Text = "PAUSE";
                timer1.Start();
                timer2.Start();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)//button gia exodo
        {
           
            Application.Exit();
        }

        
    }
}

