using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoGameTest {
    /// <summary>
    /// TutorialBattle.xaml の相互作用ロジック
    /// </summary>
    public partial class TutorialBattle : Window {

        int TurnCount = 0;
        public bool SystemEnd = false;
        public string PlayerName = "あああ";

        public TutorialBattle() {
            InitializeComponent();
            VisibleChanged();
            tbText.IsReadOnly = true;
            tbTutorial.IsReadOnly = true;
        }

        public void TutorialMessage() {
            StatusSetting();
            tbTutorial.Text = "卒業制作ゲームにようこそ。";
        }

        private void StatusSetting() {
            NameText.Text = PlayerName;
            Person player = new Person(PlayerName,100);
            Person enemy = new Person(PlayerName, 101);
            EnemyNameText.Text = enemy.Name;
            PlayerHPText.Text = "HP:100/100";
            EnemyHPText.Text = "HP:100/100";
            PlayerHPBar.Maximum = 100;
            PlayerHPBar.Value = PlayerHPBar.Maximum;
            EnemyHPBar.Maximum = 100;
            EnemyHPBar.Value = EnemyHPBar.Maximum;
            PlayerSkillName1.Text = player.EquipSkillList[0].Name;
            PlayerSkillName2.Text = player.EquipSkillList[1].Name;
            PlayerSkillName3.Text = player.EquipSkillList[2].Name;
            PlayerSkillName4.Text = player.EquipSkillList[3].Name;
            PlayerSkillName5.Text = player.EquipSkillList[4].Name;
        }

        private void btTutorial_Click(object sender, RoutedEventArgs e) {
            TurnCount++;
            EventList();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e) {
            TurnCount--;
            EventList();
        }

        private void EventList() {
            if (TurnCount <= 0) {
                TurnCount = 1;
            }
            switch (TurnCount) {
                case 1:
                    VisibleChanged();
                    tbTutorial.Text = "今からこのゲームの説明をします。";
                    break;
                case 2:
                    tbTutorial.Text = "このゲームのクリア目標は敵と１vs１で戦い、\r\n５人の相手に勝利することです。";
                    Event1();
                    break;
                case 3:
                    tbTutorial.Text = "上の四角には敵の情報、下の四角にはあなたの情報が、\r\nそれぞれ書かれています。";
                    break;
                case 4:
                    tbTutorial.Text = "あなたの名前の右側に表示されている数字はHPです。\r\n"+
                        "この数字はあなたの体力を表し、ダメージを受けると減少します。";
                    break;
                case 5:
                    Event2();
                    tbTutorial.Text = "あなたのHPが０になる前に相手のHPを０にすると、\r\n"+
                        "あなたの勝利です。";
                    break;
                case 6:
                    Event3();
                    tbTutorial.Text = "逆に、あなたのHPが相手より先に0になった場合は、\r\n"+
                        "あなたの負けです。";
                    break;
                case 7:
                    tbTutorial.Text = "HPは名前の右側に「 現在値 / 最大値 」の状態で表示されるほか、" +
                        "\r\n名前の下に緑色のバーとしても表示されています。";
                    break;
                case 8:
                    Event4();
                    tbTutorial.Text = "戦闘中、あなたは「スキル」を使用して敵を攻撃することができます。";
                    break;
                case 9:
                    Event5();
                    tbTutorial.Text = "これらはあなたが装備しているスキルを表しています。\r\n"+
                        "あなたは現在「回転斬り」という名前のスキルを装備しています。";
                    break;
                case 10:
                    tbTutorial.Text = "スキルには敵を攻撃するものや、\r\n" +
                        "自分のHPを回復するなど、さまざまな種類があります。";
                    break;
                case 11:
                    tbTutorial.Text = "敵を倒すことでそれらの新しいスキルを\r\n入手できる可能性があります。";
                    break;
                case 12:
                    tbTutorial.Text = "新しいスキルは所持するだけでなく、\r\n" +
                        "「 装備 」をする必要があります。";
                    break;
                case 13:
                    SkillSetButton.Visibility = Visibility.Visible;
                    btTutorial.Visibility = Visibility.Hidden;
                    tbTutorial.Text = "スキルを装備するためには「スキル設定」ボタンを押しましょう。";
                    break;
            }
        }

        private void Event1() {
            PlayerGB.Visibility = Visibility.Visible;
            EnemyGB.Visibility = Visibility.Visible;
            PlayerHPText.Visibility = Visibility.Visible;
        }

        private void Event2() {
            EnemyHPText.Visibility = Visibility.Visible;
            EnemyHPBar.Visibility = Visibility.Visible;
            PlayerHPText.Text = "67/100";
            PlayerHPBar.Value = 67;
            EnemyHPText.Text = "0/100";
            EnemyHPBar.Value = 0;
        }

        private void Event3() {
            EnemyHPText.Text = "48/100";
            EnemyHPBar.Value = 48;
            PlayerHPText.Text = "0/100";
            PlayerHPBar.Value = 0;
        }

        private void Event4() {
            EnemyHPText.Text = "100/100";
            EnemyHPBar.Value = 100;
            PlayerHPText.Text = "100/100";
            PlayerHPBar.Value = 100;
        }

        private void Event5() {
            PlayerSkillName1.Visibility = Visibility.Visible;
            PlayerSkillName2.Visibility = Visibility.Visible;
            PlayerSkillName3.Visibility = Visibility.Visible;
            PlayerSkillName4.Visibility = Visibility.Visible;
            PlayerSkillName5.Visibility = Visibility.Visible;
            PlayerSkill1.Visibility = Visibility.Visible;
            PlayerSkill2.Visibility = Visibility.Visible;
            PlayerSkill3.Visibility = Visibility.Visible;
            PlayerSkill4.Visibility = Visibility.Visible;
            PlayerSkill5.Visibility = Visibility.Visible;
        }

        private void SkillSetButton_Click(object sender, RoutedEventArgs e) {
            TutorialStatusSet tutorialStatusSet = new TutorialStatusSet();
            tutorialStatusSet.ShowDialog();
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e) {

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e) {

        }

        private void VisibleChanged() {
            PlayerGB.Visibility = Visibility.Hidden;
            EnemyGB.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Hidden;
            SkillSetButton.Visibility = Visibility.Hidden;
            tbText.Visibility = Visibility.Hidden;
            ShopButton.Visibility = Visibility.Hidden;
            EnemyHPText.Visibility = Visibility.Hidden;
            EnemyHPBar.Visibility = Visibility.Hidden;
            ResetButton.Visibility = Visibility.Hidden;
            PlayerSTRText.Visibility = Visibility.Hidden;
            PlayerDEXText.Visibility = Visibility.Hidden;
            PlayerINTText.Visibility = Visibility.Hidden;
            PlayerFAIText.Visibility = Visibility.Hidden;
            PlayerSkillName1.Visibility = Visibility.Hidden;
            PlayerSkillName2.Visibility = Visibility.Hidden;
            PlayerSkillName3.Visibility = Visibility.Hidden;
            PlayerSkillName4.Visibility = Visibility.Hidden;
            PlayerSkillName5.Visibility = Visibility.Hidden;
            PlayerSkill1.Visibility = Visibility.Hidden;
            PlayerSkill2.Visibility = Visibility.Hidden;
            PlayerSkill3.Visibility = Visibility.Hidden;
            PlayerSkill4.Visibility = Visibility.Hidden;
            PlayerSkill5.Visibility = Visibility.Hidden;
            tbEnemySkill1.Visibility = Visibility.Hidden;
            tbEnemySkill2.Visibility = Visibility.Hidden;
            tbEnemySkill3.Visibility = Visibility.Hidden;
            tbEnemySkill4.Visibility = Visibility.Hidden;
            tbEnemySkill5.Visibility = Visibility.Hidden;
            EnemySkill1.Visibility = Visibility.Hidden;
            EnemySkill2.Visibility = Visibility.Hidden;
            EnemySkill3.Visibility = Visibility.Hidden;
            EnemySkill4.Visibility = Visibility.Hidden;
            EnemySkill5.Visibility = Visibility.Hidden;

        }

        private void Window_Closed(object sender, EventArgs e) {
            SystemEnd = true;
            this.Close();
        }
    }
}
