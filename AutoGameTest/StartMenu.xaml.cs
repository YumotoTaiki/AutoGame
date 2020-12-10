using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AutoGameTest {
    /// <summary>
    /// StartMenu.xaml の相互作用ロジック
    /// </summary>
    public partial class StartMenu : Window {

        public Random rand = new Random();
        public string PlayerName = "プレイヤー";
        public bool SystemEnd = false;
        TutorialBattle tutorialBattle = new TutorialBattle();

        public StartMenu() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (PlayerNameText.Text == "") {
                MessageBox.Show("スタートの前に名前を入力してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            PlayerName = PlayerNameText.Text;
            MessageBoxResult result = (MessageBoxResult)MessageBox.Show("チュートリアルを行いますか？",
            "ゲームの説明です。",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information);
            if (result == MessageBoxResult.Yes) {
                this.Hide();
                tutorialBattle.PlayerName = PlayerName;
                tutorialBattle.TutorialMessage();
                tutorialBattle.ShowDialog();
                if (tutorialBattle.SystemEnd == true) {
                    SystemEnd = true;
                    tutorialBattle.Close();
                    this.Close();
                }
            } else {
                this.Hide();
            }
        }

        private void BtEnd_Click(object sender, RoutedEventArgs e) {
            SystemEnd = true;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e) {
            SystemEnd = true;
            tutorialBattle.Close();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            int num = rand.Next(2);
            switch (num) {
                case 0:
                    PlayerNameText.Text = "やまちゃん";
                    break;
                case 1:
                    PlayerNameText.Text = "まやちゃん";
                    break;
            }
        }
    }
}
