using AutoGameTest.Skills;
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
    /// StatusSetting.xaml の相互作用ロジック
    /// </summary>
    public partial class StatusSetting : Window {

        public Person player = new Person("あああ",1);
        bool SetCheck = false;
        public Window ParentWindow;
        public Skill[] SetSkillList = new Skill[5];

        public StatusSetting(Person person) {
            SetCheck = false;
            InitializeComponent();
            player = person;

            //スキル名のリストに追加
            //SkillName1.Items.Add(player.EquipSkillList[0].Name);
            //SkillName2.Items.Add(player.EquipSkillList[1].Name);
            //SkillName3.Items.Add(player.EquipSkillList[2].Name);
            //SkillName4.Items.Add(player.EquipSkillList[3].Name);
            //SkillName5.Items.Add(player.EquipSkillList[4].Name);

            SkillName1.SelectedItem = player.EquipSkillList[0].Name;
            SkillName2.SelectedItem = player.EquipSkillList[1].Name;
            SkillName3.SelectedItem = player.EquipSkillList[2].Name;
            SkillName4.SelectedItem = player.EquipSkillList[3].Name;
            SkillName5.SelectedItem = player.EquipSkillList[4].Name;

            SkillCD1.Text = player.EquipSkillList[0].CD.ToString();
            SkillCD2.Text = player.EquipSkillList[1].CD.ToString();
            SkillCD3.Text = player.EquipSkillList[2].CD.ToString();
            SkillCD4.Text = player.EquipSkillList[3].CD.ToString();
            SkillCD5.Text = player.EquipSkillList[4].CD.ToString();

            SkillText1.Text = player.EquipSkillList[0].Text.ToString();
            SkillText2.Text = player.EquipSkillList[1].Text.ToString();
            SkillText3.Text = player.EquipSkillList[2].Text.ToString();
            SkillText4.Text = player.EquipSkillList[3].Text.ToString();
            SkillText5.Text = player.EquipSkillList[4].Text.ToString();

            for (int i = 0; i < player.GetSkillList.Count; i++) {
                SkillName1.Items.Add(player.GetSkillList[i].Name);
            }
            for (int i = 0; i < player.GetSkillList.Count; i++) {
                SkillName2.Items.Add(player.GetSkillList[i].Name);
            }
            for (int i = 0; i < player.GetSkillList.Count; i++) {
                SkillName3.Items.Add(player.GetSkillList[i].Name);
            }
            for (int i = 0; i < player.GetSkillList.Count; i++) {
                SkillName4.Items.Add(player.GetSkillList[i].Name);
            }
            for (int i = 0; i < player.GetSkillList.Count; i++) {
                SkillName5.Items.Add(player.GetSkillList[i].Name);
            }
            SetCheck = true;
        }

        private void SkillName1_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SetCheck == true) {
                if (SkillName1.Text != "なし") {
                    //既に装備しているスキルがあれば「getSkillList」に移しておく
                    player.GetSkillList.Add(SetSkillList[0]);
                    SkillName2.Items.Add(SetSkillList[0].Name);
                    SkillName3.Items.Add(SetSkillList[0].Name);
                    SkillName4.Items.Add(SetSkillList[0].Name);
                    SkillName5.Items.Add(SetSkillList[0].Name);
                }
                SetSkillList[0] = player.GetSkillList.Where(x => x.Name == SkillName1.SelectedItem.ToString()).First();
                //名前以外のデータをセットする
                SkillCD1.Text = SetSkillList[0].CD.ToString();
                SkillText1.Text = SetSkillList[0].Text.ToString();
                //装備したスキルが「なし」ではないなら「getList」から削除する。
                if (SetSkillList[0].Name != "なし") {
                    player.GetSkillList.Remove(player.GetSkillList.Where(x => x.Name == SetSkillList[0].Name).First());
                }
                player.EquipSkillList[0] = SetSkillList[0];
                if (SkillName1.SelectedItem.ToString() != "なし") {
                    SkillName2.Items.Remove(SkillName1.SelectedItem);
                    SkillName3.Items.Remove(SkillName1.SelectedItem);
                    SkillName4.Items.Remove(SkillName1.SelectedItem);
                    SkillName5.Items.Remove(SkillName1.SelectedItem);
                }
            }
        }

        private void SkillName2_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SetCheck == true) {
                if (SkillName2.Text != "なし") {
                    player.GetSkillList.Add(SetSkillList[1]);
                    SkillName1.Items.Add(SetSkillList[1].Name);
                    SkillName3.Items.Add(SetSkillList[1].Name);
                    SkillName4.Items.Add(SetSkillList[1].Name);
                    SkillName5.Items.Add(SetSkillList[1].Name);
                }
                SetSkillList[1] = player.GetSkillList.Where(x => x.Name == SkillName2.SelectedItem.ToString()).First();
                SkillCD2.Text = SetSkillList[1].CD.ToString();
                SkillText2.Text = SetSkillList[1].Text.ToString();
                if (SetSkillList[1].Name != "なし") {
                    player.GetSkillList.Remove(player.GetSkillList.Where(x => x.Name == SetSkillList[1].Name).First());
                }
                player.EquipSkillList[1] = SetSkillList[1];
                if (SkillName2.SelectedItem.ToString() != "なし") {
                    SkillName1.Items.Remove(SkillName2.SelectedItem);
                    SkillName3.Items.Remove(SkillName2.SelectedItem);
                    SkillName4.Items.Remove(SkillName2.SelectedItem);
                    SkillName5.Items.Remove(SkillName2.SelectedItem);
                }
            }
        }

        private void SkillName3_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SetCheck == true) {
                if (SkillName3.Text != "なし") {
                    player.GetSkillList.Add(SetSkillList[2]);
                    SkillName1.Items.Add(SetSkillList[2].Name);
                    SkillName2.Items.Add(SetSkillList[2].Name);
                    SkillName4.Items.Add(SetSkillList[2].Name);
                    SkillName5.Items.Add(SetSkillList[2].Name);
                }
                SetSkillList[2] = player.GetSkillList.Where(x => x.Name == SkillName3.SelectedItem.ToString()).First();
                SkillCD3.Text = SetSkillList[2].CD.ToString();
                SkillText3.Text = SetSkillList[2].Text.ToString();
                if (SetSkillList[2].Name != "なし") {
                    player.GetSkillList.Remove(player.GetSkillList.Where(x => x.Name == SetSkillList[2].Name).First());
                }
                player.EquipSkillList[2] = SetSkillList[2];
                if (SkillName3.SelectedItem.ToString() != "なし") {
                    SkillName1.Items.Remove(SkillName3.SelectedItem);
                    SkillName2.Items.Remove(SkillName3.SelectedItem);
                    SkillName4.Items.Remove(SkillName3.SelectedItem);
                    SkillName5.Items.Remove(SkillName3.SelectedItem);
                }
            }
        }

        private void SkillName4_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SetCheck == true) {
                if (SkillName4.Text != "なし") {
                    player.GetSkillList.Add(SetSkillList[3]);
                    SkillName1.Items.Add(SetSkillList[3].Name);
                    SkillName2.Items.Add(SetSkillList[3].Name);
                    SkillName3.Items.Add(SetSkillList[3].Name);
                    SkillName5.Items.Add(SetSkillList[3].Name);
                }
                SetSkillList[3] = player.GetSkillList.Where(x => x.Name == SkillName4.SelectedItem.ToString()).First();
                SkillCD4.Text = SetSkillList[3].CD.ToString();
                SkillText4.Text = SetSkillList[3].Text.ToString();
                if (SetSkillList[3].Name != "なし") {
                    player.GetSkillList.Remove(player.GetSkillList.Where(x => x.Name == SetSkillList[3].Name).First());
                }
                player.EquipSkillList[3] = SetSkillList[3];
                if (SkillName4.SelectedItem.ToString() != "なし") {
                    SkillName1.Items.Remove(SkillName4.SelectedItem);
                    SkillName2.Items.Remove(SkillName4.SelectedItem);
                    SkillName3.Items.Remove(SkillName4.SelectedItem);
                    SkillName5.Items.Remove(SkillName4.SelectedItem);
                }
            }
        }

        private void SkillName5_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SetCheck == true) {
                if (SkillName5.Text != "なし") {
                    player.GetSkillList.Add(SetSkillList[4]);
                    SkillName1.Items.Add(SetSkillList[4].Name);
                    SkillName2.Items.Add(SetSkillList[4].Name);
                    SkillName3.Items.Add(SetSkillList[4].Name);
                    SkillName4.Items.Add(SetSkillList[4].Name);
                }
                SetSkillList[4] = player.GetSkillList.Where(x => x.Name == SkillName5.SelectedItem.ToString()).First();
                SkillCD5.Text = SetSkillList[4].CD.ToString();
                SkillText5.Text = SetSkillList[4].Text.ToString();
                if (SetSkillList[4].Name != "なし") {
                    player.GetSkillList.Remove(player.GetSkillList.Where(x => x.Name == SetSkillList[4].Name).First());
                }
                player.EquipSkillList[4] = SetSkillList[4];
                if (SkillName5.SelectedItem.ToString() != "なし") {
                    SkillName1.Items.Remove(SkillName5.SelectedItem);
                    SkillName2.Items.Remove(SkillName5.SelectedItem);
                    SkillName3.Items.Remove(SkillName5.SelectedItem);
                    SkillName4.Items.Remove(SkillName5.SelectedItem);
                }
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e) {
            if (player.EquipSkillList.All(x => x.Name == "なし")) {
                MessageBox.Show("最低１つ以上のスキルを設定してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            MainWindow.player = player;
            this.Hide();
        }
    }
}
