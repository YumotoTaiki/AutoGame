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
        bool DistCheck = false;
        public Window ParentWindow;
        public Skill[]     SetSkillList  = new Skill[5];             //「スキル設定画面専用」の装備スキルリスト
        public List<Skill> HaveSkillList = new List<Skill> { };      //「スキル設定画面専用」の所持スキルリスト

        public StatusSetting(Person person) {
            SetCheck = false;
            InitializeComponent();
            player = person;

            SkillListUpdate();
            ContentSet();

            SetCheck = true;
        }

        public void Update(Person person) {
            player = person;
            SkillListUpdate();
            SkillNameUpdate();
            ContentSet();
        }

        //SetSkillListとHaveSkillListを最新の状態にする
        private void SkillListUpdate() {
            for (int i = 0; i < SetSkillList.Length; i++) {
                SetSkillList[i] = player.EquipSkillList[i];
            }
            HaveSkillList.Clear();
            for (int i = 0; i < player.GetSkillList.Count; i++) {
                HaveSkillList.Add(player.GetSkillList[i]);
            }
        }

        private void ContentSet() {

            SkillName1.Items.Clear();
            SkillName2.Items.Clear();
            SkillName3.Items.Clear();
            SkillName4.Items.Clear();
            SkillName5.Items.Clear();

            for (int i = 0; i < HaveSkillList.Count; i++) {
                SkillName1.Items.Add(HaveSkillList[i].Name);
            }
            for (int i = 0; i < HaveSkillList.Count; i++) {
                SkillName2.Items.Add(HaveSkillList[i].Name);
            }
            for (int i = 0; i < HaveSkillList.Count; i++) {
                SkillName3.Items.Add(HaveSkillList[i].Name);
            }
            for (int i = 0; i < HaveSkillList.Count; i++) {
                SkillName4.Items.Add(HaveSkillList[i].Name);
            }
            for (int i = 0; i < HaveSkillList.Count; i++) {
                SkillName5.Items.Add(HaveSkillList[i].Name);
            }

            //↑で足したやつと同じヤツが既にあるかどうか
            foreach (var item in SkillName1.Items) {
                if (item.ToString() == SetSkillList[0].Name) {
                    DistCheck = true;
                }
            }
            if (DistCheck == false) {
                SkillName1.Items.Add(SetSkillList[0].Name);
            }
            DistCheck = false;
            foreach (var item in SkillName2.Items) {
                if (item.ToString() == SetSkillList[1].Name) {
                    DistCheck = true;
                }
            }
            if (DistCheck == false) {
                SkillName2.Items.Add(SetSkillList[1].Name);
            }
            DistCheck = false;
            foreach (var item in SkillName3.Items) {
                if (item.ToString() == SetSkillList[2].Name) {
                    DistCheck = true;
                }
            }
            if (DistCheck == false) {
                SkillName3.Items.Add(SetSkillList[2].Name);
            }
            DistCheck = false;
            foreach (var item in SkillName4.Items) {
                if (item.ToString() == SetSkillList[3].Name) {
                    DistCheck = true;
                }
            }
            if (DistCheck == false) {
                SkillName4.Items.Add(SetSkillList[3].Name);
            }
            DistCheck = false;
            foreach (var item in SkillName5.Items) {
                if (item.ToString() == SetSkillList[4].Name) {
                    DistCheck = true;
                }
            }
            if (DistCheck == false) {
                SkillName5.Items.Add(SetSkillList[4].Name);
            }
            DistCheck = false;

            SetCheck = false;
            SkillName1.SelectedItem = SetSkillList[0].Name;
            SkillName2.SelectedItem = SetSkillList[1].Name;
            SkillName3.SelectedItem = SetSkillList[2].Name;
            SkillName4.SelectedItem = SetSkillList[3].Name;
            SkillName5.SelectedItem = SetSkillList[4].Name;
            SetCheck = true;

            SkillCD1.Text = SetSkillList[0].CD.ToString();
            SkillCD2.Text = SetSkillList[1].CD.ToString();
            SkillCD3.Text = SetSkillList[2].CD.ToString();
            SkillCD4.Text = SetSkillList[3].CD.ToString();
            SkillCD5.Text = SetSkillList[4].CD.ToString();

            SkillText1.Text = SetSkillList[0].Text.ToString();
            SkillText2.Text = SetSkillList[1].Text.ToString();
            SkillText3.Text = SetSkillList[2].Text.ToString();
            SkillText4.Text = SetSkillList[3].Text.ToString();
            SkillText5.Text = SetSkillList[4].Text.ToString();
        }

        private void SkillNameUpdate() {
            SetCheck = false;
            for (int i = 0; i < SkillName1.Items.Count; i++) {
                //名前欄１の項目全てと１つ目に装備中の元名が同じかつ普通の名が変わってたら
                if (SkillName1.Items[i].ToString() == SetSkillList[0].OriginalName && SkillName1.Items[i].ToString() != SetSkillList[0].Name) {
                    SkillName1.Items.Add(SetSkillList[0].Name);
                    if (SkillName1.SelectedItem.ToString() != SetSkillList[0].Name) {
                        SkillName1.SelectedItem = SetSkillList[0].Name;
                    }
                    SkillName1.Items.Remove(SkillName1.Items[i]);
                }
            }
            for (int i = 1; i < SkillName2.Items.Count; i++) {
                if (SkillName2.Items[i].ToString() == SetSkillList[1].OriginalName && SkillName2.Items[i].ToString() != SetSkillList[1].Name) {
                    SkillName2.Items.Add(SetSkillList[1].Name);
                    if (SkillName2.SelectedItem.ToString() != SetSkillList[1].Name) {
                        SkillName2.SelectedItem = SetSkillList[1].Name;
                    }
                    SkillName2.Items.Remove(SkillName2.Items[i]);
                }
            }
            for (int i = 0; i < SkillName3.Items.Count; i++) {
                if (SkillName3.Items[i].ToString() == SetSkillList[2].OriginalName && SkillName3.Items[i].ToString() != SetSkillList[2].Name) {
                    SkillName3.Items.Add(SetSkillList[2].Name);
                    if (SkillName3.SelectedItem.ToString() != SetSkillList[2].Name) {
                        SkillName3.SelectedItem = SetSkillList[2].Name;
                    }
                    SkillName3.Items.Remove(SkillName3.Items[i]);
                }
            }
            for (int i = 0; i < SkillName4.Items.Count; i++) {
                if (SkillName4.Items[i].ToString() == SetSkillList[3].OriginalName && SkillName4.Items[i].ToString() != SetSkillList[3].Name) {
                    SkillName4.Items.Add(SetSkillList[3].Name);
                    if (SkillName4.SelectedItem.ToString() != SetSkillList[3].Name) {
                        SkillName4.SelectedItem = SetSkillList[3].Name;
                    }
                    SkillName4.Items.Remove(SkillName4.Items[i]);
                }
            }
            for (int i = 0; i < SkillName5.Items.Count; i++) {
                if (SkillName5.Items[i].ToString() == SetSkillList[4].OriginalName && SkillName5.Items[i].ToString() != SetSkillList[4].Name) {
                    SkillName5.Items.Add(SetSkillList[4].Name);
                    if (SkillName5.SelectedItem.ToString() != SetSkillList[4].Name) {
                        SkillName5.SelectedItem = SetSkillList[4].Name;
                    }
                    SkillName5.Items.Remove(SkillName5.Items[i]);
                }
            }
            SetCheck = true;
        }

        private void SkillName1_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            var frefe = SkillName1.Text;
            var fad = SkillName1.SelectedItem;
            var feefr = player.EquipSkillList[0].Name;

            if (SkillName1.Items.Count == 0) {
                return;
            }
            if (SetCheck == true) {
                if (SkillName1.Text != "なし" && SkillName1.Text != "") {
                    //既に装備しているスキルがあれば「getSkillList」に移しておく
                    player.GetSkillList.Add(SetSkillList[0]);
                    SkillName2.Items.Add(SetSkillList[0].Name);
                    SkillName3.Items.Add(SetSkillList[0].Name);
                    SkillName4.Items.Add(SetSkillList[0].Name);
                    SkillName5.Items.Add(SetSkillList[0].Name);
                }

                //GetSkillListに同じ名前があるか
                if (SkillName1.SelectedItem != null) {
                    if (player.GetSkillList.Where(x => x.Name == SkillName1.SelectedItem.ToString()).First() == null) {
                        SetSkillList[0] = player.EquipSkillList[0];
                    } else {
                        SetSkillList[0] = player.GetSkillList.Where(x => x.Name == SkillName1.SelectedItem.ToString()).First();
                    }
                }
                //名前以外のデータをセットする
                SkillCD1.Text = SetSkillList[0].CD.ToString();
                SkillText1.Text = SetSkillList[0].Text.ToString();
                //装備したスキルが「なし」ではないなら「getList」から削除する。
                if (SkillName1.SelectedItem.ToString() != "なし") {
                    player.GetSkillList.Remove(player.GetSkillList.Where(x => x.Name == SetSkillList[0].Name).First());
                }
                player.EquipSkillList[0] = SetSkillList[0];
                if (SetSkillList[0].Name != "なし") {
                    SkillName2.Items.Remove(SkillName1.SelectedItem);
                    SkillName3.Items.Remove(SkillName1.SelectedItem);
                    SkillName4.Items.Remove(SkillName1.SelectedItem);
                    SkillName5.Items.Remove(SkillName1.SelectedItem);
                }
            }
        }

        private void SkillName2_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SkillName2.Items.Count == 0) {
                return;
            }
            if (SetCheck == true) {
                if (SkillName2.Text != "なし" && SkillName2.Text != "") {
                    player.GetSkillList.Add(SetSkillList[1]);
                    SkillName1.Items.Add(SetSkillList[1].Name);
                    SkillName3.Items.Add(SetSkillList[1].Name);
                    SkillName4.Items.Add(SetSkillList[1].Name);
                    SkillName5.Items.Add(SetSkillList[1].Name);
                }
                if (player.GetSkillList.Where(x => x.Name == SkillName2.SelectedItem.ToString()).First() == null) {
                    SetSkillList[1] = player.EquipSkillList[1];
                } else {
                    SetSkillList[1] = player.GetSkillList.Where(x => x.Name == SkillName2.SelectedItem.ToString()).First();
                }
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
            if (SkillName3.Items.Count == 0) {
                return;
            }
            if (SetCheck == true) {
                if (SkillName3.Text != "なし" && SkillName3.Text != "") {
                    player.GetSkillList.Add(SetSkillList[2]);
                    SkillName1.Items.Add(SetSkillList[2].Name);
                    SkillName2.Items.Add(SetSkillList[2].Name);
                    SkillName4.Items.Add(SetSkillList[2].Name);
                    SkillName5.Items.Add(SetSkillList[2].Name);
                }
                if (player.GetSkillList.Where(x => x.Name == SkillName3.SelectedItem.ToString()).First() == null) {
                    SetSkillList[2] = player.EquipSkillList[2];
                } else {
                    SetSkillList[2] = player.GetSkillList.Where(x => x.Name == SkillName3.SelectedItem.ToString()).First();
                }
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
            if (SkillName4.Items.Count == 0) {
                return;
            }
            if (SetCheck == true) {
                if (SkillName4.Text != "なし" && SkillName4.Text != "") {
                    player.GetSkillList.Add(SetSkillList[3]);
                    SkillName1.Items.Add(SetSkillList[3].Name);
                    SkillName2.Items.Add(SetSkillList[3].Name);
                    SkillName3.Items.Add(SetSkillList[3].Name);
                    SkillName5.Items.Add(SetSkillList[3].Name);
                }
                if (player.GetSkillList.Where(x => x.Name == SkillName4.SelectedItem.ToString()).First() == null) {
                    SetSkillList[3] = player.EquipSkillList[3];
                } else {
                    SetSkillList[3] = player.GetSkillList.Where(x => x.Name == SkillName4.SelectedItem.ToString()).First();
                }
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
            if (SkillName5.Items.Count == 0) {
                return;
            }
            if (SetCheck == true) {
                if (SkillName5.Text != "なし" && SkillName5.Text != "") {
                    player.GetSkillList.Add(SetSkillList[4]);
                    SkillName1.Items.Add(SetSkillList[4].Name);
                    SkillName2.Items.Add(SetSkillList[4].Name);
                    SkillName3.Items.Add(SetSkillList[4].Name);
                    SkillName4.Items.Add(SetSkillList[4].Name);
                }
                if (player.GetSkillList.Where(x => x.Name == SkillName5.SelectedItem.ToString()).First() == null) {
                    SetSkillList[4] = player.EquipSkillList[4];
                } else {
                    SetSkillList[4] = player.GetSkillList.Where(x => x.Name == SkillName5.SelectedItem.ToString()).First();
                }
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
