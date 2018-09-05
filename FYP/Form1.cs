using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace FYP
{
    public partial class Form1 : Form
    {
        //
        // Golbal variable since no well organize database
        string Gfolder;
        string Gsong;
        bool play =false;
        WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
            //getSongName("Hits");
            //getDuration("Sia - Chandelier (Karaoke Version).mp4");
            //fillSong("Hits");
            fillCategories();
            dataGridView1_CellContentClick(dataGridView2, new DataGridViewCellEventArgs(0, 0));
            axWindowsMediaPlayer1.uiMode = "none";
        }


        // when click the catagery , work with fillSong function
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows.Clear();
            int index = e.RowIndex;
            try
            {
                string s = dataGridView1.Rows[index].Cells[0].Value.ToString();
                if (!fillSong(s))
                    MessageBox.Show("No Song Found");
                Gfolder = s;
            }catch(Exception ex)
            {

            }
            
        }

        // click song, work with playVocal function
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            try
            {
                string song = dataGridView2.Rows[index].Cells[1].Value.ToString();
                Gsong = song;
                string c = Directory.GetCurrentDirectory().ToString() + @"\Music\" + Gfolder+@"\"+@song;
                axWindowsMediaPlayer1.URL = @c;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                playVocal(song.Replace(".mp4",""));
                radioButton1.Checked = true;
                button1.Text = "Pause";
            }
            catch(Exception ex)
            {

            }
        }

        // all song name in catagery
        private List<string> getSongName(string folder)
        {
            List<string> song;
            try {
                song = new List<string>();

                //Get curent Directory
                string c = Directory.GetCurrentDirectory().ToString()+@"\Music\"+@folder;

                // Go to Folder
                DirectoryInfo d = new DirectoryInfo(c);

                // Get Extension "mp4, mp3 ....
                FileInfo[] files = d.GetFiles("*.mp4");


                // store to array
                foreach (FileInfo f in files)
                    song.Add(f.Name);
                return song;

            }catch (Exception ex)
            {
                song = new List<string>();
                string fail = "Fail to get song name.";
                MessageBox.Show(fail);
                return song;
            }
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void playVideo()
        {

        }

        private string getDuration(string folder,string song)
        {
            try
            {
                var player = new WindowsMediaPlayer();

                string c = Directory.GetCurrentDirectory().ToString() +@"\Music\"+ @folder+@"\"+@song;
                IWMPMedia a = player.newMedia(@c);
                return TimeSpan.FromSeconds(a.duration).ToString();
            }
            catch(Exception ex)
            {
                string ms = "Fail to get duration for song: " + song;
                MessageBox.Show(ms);
                return "";
            }
        }
        
        // fill the datagridview with all song name
        private bool fillSong(string cat)
        {
            try{
                List<string> song = new List<string>();
                song = getSongName(cat);
                if (song.Count == 0)
                    return false;
                
                for (int i = 0; i < song.Count ; i++)
                {
                    dataGridView2.Rows.Add(1);
                    dataGridView2.Rows[i].Cells[1].Value = song[i];
                    dataGridView2.Rows[i].Cells[2].Value = getDuration(cat,song[i]);
                }
                return true;
            }catch(Exception ex){
                return false;
            }
        }


        // Get all category in List
        private List<string> getFolderName()
        {
            List<string> folderName=new List<string>();
            try
            {
                string d = Directory.GetCurrentDirectory().ToString() + @"\Music\";
                string[] folders = Directory.GetDirectories(@d, "*", System.IO.SearchOption.AllDirectories);

                DirectoryInfo d1 = new DirectoryInfo(d);
                FileInfo[] files = d1.GetFiles("*");
                foreach (string name in folders)
                {
                    folderName.Add(name.Replace(@d,""));
                }
                return folderName;
            }
            catch(Exception ex)
            {
                return folderName;
            }
        }

        // Fill the Categories with List
        private bool fillCategories()
        {
            List<string> folderName = new List<string>();
            try
            {
                folderName = getFolderName();

                for(int i = 0; i < folderName.Count; i++)
                {
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[i].Cells[0].Value = folderName[i];
                }

                return true;
            }
            catch
            {

                return false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.mute = true;
            Player.settings.mute = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.mute = false;
            Player.settings.mute = true;
        }

        //play vocal
        private void playVocal(string song)
        {
            
            song += ".mp3";
            string c = Directory.GetCurrentDirectory().ToString() + @"\Music\" + @Gfolder+@"\"+@song;

            Player.URL = @c;

            Player.controls.play();
            Console.Write(c);

        }

        private void songDatabase()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            playSong();
        }

        private bool playSong()
        {
            try
            {
                if (play)
                {
                    Player.controls.play();
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    button1.Text = "Pause";
                    play = false;
                }
                else
                {
                    Player.controls.pause();
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                    button1.Text = "Play";
                    play = true;
                }

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

    }
}
