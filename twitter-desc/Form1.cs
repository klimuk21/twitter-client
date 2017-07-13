using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Parameters;
using Tweetinvi.Models;

namespace twitter_desc
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            var authenticatedUser = User.GetAuthenticatedUser(); 
            label6.Text = "@" + authenticatedUser.UserIdentifier.ToString(); //id
            label7.Text = authenticatedUser.Name.ToString(); //Name
            label8.Text = authenticatedUser.FollowersCount.ToString();
            label9.Text = authenticatedUser.FriendsCount.ToString();
            label10.Text = authenticatedUser.StatusesCount.ToString();
            pictureBox1.ImageLocation = authenticatedUser.ProfileImageUrlFullSize; //Profile image user
            richTextBox1.Text = "";
            var tweets = Timeline.GetHomeTimeline();  //tweets user
            foreach (var timelinetweet in tweets)
            {
                richTextBox1.Text += "==================================================================================================" + Environment.NewLine + Environment.NewLine;
                richTextBox1.Text += "●" + timelinetweet.ToString() + Environment.NewLine;
                richTextBox1.Text += Environment.NewLine;

            }

        }

        string AddMediaPath = "";

       
        private void button1_Click(object sender, EventArgs e) //Publish Tweets
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле не должно быть пустым");
            }
            else
            {
                if (label14.Text == "")
                {
                    var firstTweet = Tweet.PublishTweet(textBox1.Text);
                    
                }
                else
                {
                    byte[] file1 = File.ReadAllBytes(AddMediaPath);
                    var media = Upload.UploadImage(file1);

                    var tweet = Tweet.PublishTweet(textBox1.Text, new PublishTweetOptionalParameters
                    {
                        Medias = new List<IMedia> { media }
                    });
                   
                }
            }

        }

        private void button2_Click(object sender, EventArgs e) //add image
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label14.Text = openFileDialog1.SafeFileName;
                button2.Text = "Добавлено " + openFileDialog1.SafeFileName;
                AddMediaPath = openFileDialog1.FileName;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (textBox2.Text == "")
                {

                    richTextBox1.Text = "";
                    var tweets = Timeline.GetHomeTimeline();
                    foreach (var timelinetweet in tweets)
                    {
                        richTextBox1.Text += "==================================================================================================" + Environment.NewLine + Environment.NewLine;
                        richTextBox1.Text += "●" + timelinetweet.ToString() + Environment.NewLine;
                        richTextBox1.Text += Environment.NewLine;
                    }

                }
                else
                {
                    richTextBox1.Text = "";
                    var tweets = Timeline.GetUserTimeline(textBox2.Text);
                    foreach (var timelinetweet in tweets)
                    {
                        richTextBox1.Text += "==================================================================================================" + Environment.NewLine + Environment.NewLine;
                        richTextBox1.Text += "●" + timelinetweet.ToString() + Environment.NewLine;
                        richTextBox1.Text += Environment.NewLine;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пользователя с таким ID не существует, либо доступ к его твитам ограничен!");
            }
        }   

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("Заполните поле адресата и введите текст сообщения");
            }
            else
            {
                var message = Tweetinvi.Message.PublishMessage(textBox3.Text, textBox4.Text);
                textBox3.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Поле не должно быть пустым");
                }
                else
                {
                    richTextBox1.Text = "";
                    var matchingTweets = Search.SearchTweets(textBox2.Text);
                    foreach (var machingTweet in matchingTweets)
                    {
                        richTextBox1.Text += "==================================================================================================" + Environment.NewLine + Environment.NewLine;
                        richTextBox1.Text += "●" + machingTweet.ToString() + Environment.NewLine;
                        richTextBox1.Text += Environment.NewLine;

                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
