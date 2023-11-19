using System.IO;
using System.Text;
using System.Timers;

namespace DPSMonitor
{

    public partial class Form1 : Form
    {
        Dictionary<string, Player> players = new Dictionary<string, Player>();
        public Form1()
        {
            InitializeComponent();
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(button1_Click);

            // Set the Interval to 5 seconds (2000 milliseconds).
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.CheckKeyword("DAMAGE", Color.OrangeRed, 0);
            this.CheckKeyword("Physical", Color.Orange, 0);
            this.CheckKeyword("Divine", Color.Gold, 0);
            this.CheckKeyword("Magical", Color.Purple, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            readFile();
        }
        void readFile()
        {
            // A dictionary to store the players and their dps


            String playerName = "";
            int damageWord = 0;
            int physicalWord = 0;
            int divineWord = 0;
            int magicalWord = 0;

            int separator = 0;
            int separatorStart = 0;

            float damage = 0;
            float physical = 0;
            float divine = 0;
            float magical = 0;



            // The file path of the data file
            string filePath = @"" + textBox1.Text + "\\nwclientLog1.txt";

            // Try to open the file with read access and read/write share
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // Read each line of the file
                        string line;
                        //while ((line = sr.ReadLine()) != null)
                        while (true)
                        {

                            line = sr.ReadLine();
                            //if (line != null)
                            //{

                            damageWord = line.IndexOf("damages");
                            physicalWord = line.IndexOf("Physical");
                            divineWord = line.IndexOf("Divine");
                            magicalWord = line.IndexOf("Magical");


                            separator = line.IndexOf(": ");
                            separatorStart = line.IndexOf("] ");
                            // Split the line by colon
                            // Assume the line has the format: "PlayerName:Damage"
                            //string[] parts = line.Split(':');
                            if (damageWord > 0)
                            {
                                playerName = line.Substring(1, damageWord - 2);
                                playerName = playerName.Substring(40);
                                damage = float.Parse(line.Substring(separator, 5).Trim().Trim('(').Trim(':'));

                                if (physicalWord > 0)
                                {
                                    physical = float.Parse(line.Substring(physicalWord - 4, 5).Trim().Trim('(').Trim(':').Trim('P').Trim());
                                }
                                else
                                {
                                    physical = 0;
                                }
                                if (divineWord > 0)
                                {
                                    divine = float.Parse(line.Substring(divineWord - 3, 4).Trim().Trim('(').Trim(':').Trim('D').Trim());
                                }
                                else
                                {
                                    divine = 0;
                                }
                                if (magicalWord > 0)
                                {
                                    magical = float.Parse(line.Substring(magicalWord - 3, 4).Trim().Trim('(').Trim(':').Trim('M').Trim());
                                }
                                else
                                {
                                    magical = 0;
                                }

                                // Check if the player is already in the dictionary
                                if (players.ContainsKey(playerName))
                                {
                                    // Add the damage to the existing dps
                                    players[playerName].Damage += damage;
                                    players[playerName].Physical += physical;
                                    players[playerName].Divine += divine;
                                    players[playerName].Magical += magical;
                                }
                                else
                                {
                                    // Add the player and the damage to the dictionary
                                    Player player = new Player();
                                    player.Name = playerName;
                                    player.Damage = damage;
                                    player.Physical = physical;
                                    player.Divine = divine;
                                    player.Magical = magical;
                                    players.Add(playerName, player);
                                }
                            }
                            //}

                            if (line == null) Thread.Sleep(500);
                        }
                    }
                }
            }
            // Catch any exception and print the error message to the console
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            players = players.OrderByDescending(x => x.Value.Damage).ToDictionary(x => x.Key, x => x.Value);
            // Print the players and their dps to the console
            foreach (var player in players)
            {
                //Console.WriteLine("Player name: {0}", player.Key + " - > DPS: " + player.Value);
                //Console.WriteLine("DPS: {0}", player.Value);
                richTextBox1.AppendText("" + player.Key + " - > DAMAGE: " + player.Value.Damage + " Physical: " + player.Value.Physical + " Magical: "+ player.Value.Magical+
                    " " + " Divine: " + player.Value.Divine + "\n");
            }
            players.Clear();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        public void ChooseFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            ChooseFolder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = @"" + textBox1.Text + "\\nwclientLog1.txt";
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fs.SetLength(0);
                fs.Flush();
            }

            //File.WriteAllText(filePath, string.Empty);

        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }
        }
    }



}


