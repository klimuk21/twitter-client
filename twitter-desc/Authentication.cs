using System;
using System.Windows.Forms;
using System.Diagnostics;
using Tweetinvi;
using Tweetinvi.Models;

namespace twitter_desc
{
    public partial class Authentication : Form
    {
        dynamic appCredentials, authenticationContext;
        public Authentication()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            appCredentials = new TwitterCredentials("LunW98nTNR2EnAaJLuVXvZZAY", "LQXZ4JgwJn07097PXv6MHEmreVXz943yZbBXYJTM0IVPz5ZsOA");

            // Инициализируем процесс аутентификации и сохраняем соответствующий «AuthenticationContext». 
            authenticationContext = AuthFlow.InitAuthentication(appCredentials);
            Process.Start(authenticationContext.AuthorizationURL);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var pinCode = textBox1.Text;

            // С помощью этого пин-кода теперь можно получить учетные данные из Twitter 
            var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(pinCode, authenticationContext);

            // Использовать учетные данные пользователя в приложении 
            Auth.SetCredentials(userCredentials);
            Form1 form = new Form1();
            form.Show();
            Hide();
        }
    }
}
