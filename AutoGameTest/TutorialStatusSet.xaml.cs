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
    /// TutorialStatusSet.xaml の相互作用ロジック
    /// </summary>
    public partial class TutorialStatusSet : Window {

        int TurnCount = 0;

        public TutorialStatusSet() {
            InitializeComponent();
            VisibleChanged();
            tbTutorial.Text = "これがスキル設定画面です。";
            EventList();
        }

        private void EventList() {
            if (TurnCount < 0) {
                TurnCount = 1;
            }
            switch (TurnCount) {
                case 0:
                    tbTutorial.Text = "これがスキル設定画面です。";
                    break;
                case 1:
                    VisibleChanged();
                    tbTutorial.Text = "あなたは同時にスキルを５つまで装備することができます。\r\n" +
                    "現在は「スキル１」に「回転斬り」が装備されています。";
                    break;
                case 2:
                    tbTutorial.Text = "スキル名の右側には「クールタイム」が表示されています。\r\n"+
                        "クールタイムとは、スキルを使用してから、\r\nもう一度使用するまでの時間を表しています。";
                    break;
                case 3:
                    tbTutorial.Text = "クールタイムの数字が大きいほど\r\n"+
                        "スキルを使用するまでの時間が長くなり、隙が大きくなってしまいます。";
                    break;
                case 4:
                    tbTutorial.Text = "逆にクールタイムの数字が小さければ\r\n" +
                        "短い間隔でスキルを使用することができます。";
                    break;
                case 5:
                    tbTutorial.Text = "クールタイムの右側にはスキルの効果が表示されています。";
                    break;
                case 6:
                    tbTutorial.Text = "逆に、あなたのHPが相手より先に0になった場合は、\r\n" +
                        "あなたの負けです。";
                    break;
                case 7:
                    tbTutorial.Text = "HPは名前の右側に「 現在値 / 最大値 」の状態で表示されるほか、" +
                        "\r\n名前の下に緑色のバーとしても表示されています。";
                    break;
                case 8:
                    tbTutorial.Text = "戦闘中、あなたは「スキル」を使用して敵を攻撃することができます。";
                    break;
                case 9:
                    tbTutorial.Text = "これらはあなたが装備しているスキルを表しています。\r\n" +
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
                    tbTutorial.Text = "スキルを装備するためには「スキル設定」ボタンを押しましょう。";
                    break;
            }
        }

        private void VisibleChanged() {
            btReturn.Visibility = Visibility.Hidden;
            ReturnButton.Visibility = Visibility.Hidden;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e) {

        }

        private void btReturn_Click(object sender, RoutedEventArgs e) {
            TurnCount--;
            EventList();
        }

        private void btGo_Click(object sender, RoutedEventArgs e) {
            TurnCount++;
            EventList();
        }

        private void SkillName1_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void SkillName2_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void SkillName3_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void SkillName4_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void SkillName5_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
