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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AutoGameTest {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        bool EndCheck = false;
        bool EnemySkillONOFF = true;

        public int Time = 60;

        public Random rand = new Random();
        public static Person player = new Person("テストプレイヤー",0);
        Person enemy = new Person("悪いミウラ",1);
        StatusSetting statusSetting = new StatusSetting(player);
        Shop shop = new Shop(player);
        StartMenu startMenu = new StartMenu();
        Timer timer = new Timer();

        public MainWindow() {

            startMenu.ShowDialog();
            if (startMenu.SystemEnd == true) {
                this.Close();
                statusSetting.Close();
                shop.Close();
                startMenu.Close();

            }
            player.Name = startMenu.PlayerName;

            timer.Interval = 1000;

            InitializeComponent();
            NameText.Text = player.Name;
            PlayerHPBar.Maximum = player.HP;
            PlayerHPBar.Value = PlayerHPBar.Maximum;
            PlayerHPText.Text = PlayerHPBar.Value.ToString() + "/" + PlayerHPBar.Maximum.ToString();

            enemy.HP = 100;
            EnemyNameText.Text = enemy.Name;
            EnemyHPBar.Maximum = enemy.HP;
            EnemyHPBar.Value = EnemyHPBar.Maximum;
            EnemyHPText.Text = EnemyHPBar.Value.ToString() + "/" + EnemyHPBar.Maximum.ToString();
            tbText.IsReadOnly = true;
            ResetButton.IsEnabled = false;

            PlayerSTRText.Text = PlayerSTRText.Text + " : " + player.STR;
            PlayerDEXText.Text = PlayerDEXText.Text + " : " + player.DEX;
            PlayerINTText.Text = PlayerINTText.Text + " : " + player.INT;
            PlayerFAIText.Text = PlayerFAIText.Text + " : " + player.FAI;

            PlayerSkillSet();
        }

        private void PlayerSkillSet() {
            PlayerSkillName1.Text = player.EquipSkillList[0].Name;
            PlayerSkillName2.Text = player.EquipSkillList[1].Name;
            PlayerSkillName3.Text = player.EquipSkillList[2].Name;
            PlayerSkillName4.Text = player.EquipSkillList[3].Name;
            PlayerSkillName5.Text = player.EquipSkillList[4].Name;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) {
            EndCheck = false;
            if (player.EquipSkillList.All(x => x.Name == "なし")) {
                MessageBox.Show("最低１つ以上のスキルを設定してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            Time = 60;
            timer.Start();
            timer.Tick += Timer_Tick;
            if (player.EquipSkillList[0].Name != "なし") {
                PlayerAction1();
            }
            if (player.EquipSkillList[1].Name != "なし") {
                PlayerAction2();
            }
            if (player.EquipSkillList[2].Name != "なし") {
                PlayerAction3();
            }
            if (player.EquipSkillList[3].Name != "なし") {
                PlayerAction4();
            }
            if (player.EquipSkillList[4].Name != "なし") {
                PlayerAction5();
            }

            if (enemy.EquipSkillList[0].Name != "なし") {
                EnemyAction1();
            }
            if (enemy.EquipSkillList[1].Name != "なし") {
                EnemyAction2();
            }
            if (enemy.EquipSkillList[2].Name != "なし") {
                EnemyAction3();
            }
            if (enemy.EquipSkillList[3].Name != "なし") {
                EnemyAction4();
            }
            if (enemy.EquipSkillList[4].Name != "なし") {
                EnemyAction5();
            }
            StartButton.IsEnabled = false;
            SkillSetButton.IsEnabled = false;
            ShopButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
        }

        private void Timer_Tick(object sender, EventArgs e) {
            Time--;
            if (Time <= 0) {
                PlayerDeadCheck();
            } else {
                TimeText.Text = Time.ToString();
            }
        }

        //残骸
        private object SetSkillBar(int num) {
            switch (num) {
                case 1:
                    return PlayerSkill1;
                case 2:
                    return PlayerSkill2;
                case 3:
                    return PlayerSkill3;
                case 4:
                    return PlayerSkill4;
                case 5:
                    return PlayerSkill5;
            }
            return null;
        }

        async void PlayerAction1() {
            var skill = player.EquipSkillList[0];
            var converter = new System.Windows.Media.BrushConverter();
            int TurnCount = 0;

            //プログレスバーインスタンスできた記念
            //System.Windows.Forms.ProgressBar bar = (System.Windows.Forms.ProgressBar)(SetSkillBar(num));

            int damage;
            do {
                PlayerSkill1.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                //バーが最大になるまで繰り返す
                for (int i = 0; i < 101; i++) {
                    if (EndCheck == false) {
                        PlayerSkill1.Value = i;
                        await Task.Delay(skill.CD);
                    } else {
                        break;
                    }
                }
                if (EndCheck == true) {
                    continue;
                }
                switch (skill.Category) {
                    case "即時":
                        PlayerSkill1.Value = 0;
                        damage = player.PowerSet(skill);
                        tbText.Text = player.Name + "の" + skill.Name + "!　" + enemy.Name + "に" + damage + "のダメージ! ("+ EnemyHPBar.Value + "→"+ (EnemyHPBar.Value - damage) + ")\r\n" + tbText.Text;
                        EnemyHPBar.Value -= damage;
                        EnemyHPText.Text = EnemyHPBar.Value.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                        PlayerDeadCheck();
                        break;
                    case "持続":
                        tbText.Text = player.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                        int sec = 100 / (skill.SustainTime / 10);
                        damage = player.PowerSet(skill);
                        PlayerSkill1.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                        for (int i = skill.SustainTime / 10; i > 0; i--) {
                            if (EndCheck == false) {
                                switch (skill.Type) {
                                    case "攻撃":
                                        enemy.HP = (int)(EnemyHPBar.Value);
                                        enemy.HP -= damage;
                                        EnemyHPBar.Value -= damage;
                                        EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                        PlayerSkill1.Value = sec * i;
                                        PlayerDeadCheck();
                                        break;
                                    case "回復":
                                        player.HP = (int)(PlayerHPBar.Value);
                                        player.HP += damage;
                                        if (player.HP > PlayerHPBar.Maximum) {
                                            player.HP = (int)(PlayerHPBar.Maximum);
                                        }
                                        PlayerHPBar.Value += damage;
                                        PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                        PlayerSkill1.Value = sec * i;
                                        break;
                                    case "防御":
                                        if (TurnCount == 0) {
                                            player.DamageRelieve = player.DamageRelieve * 1 - (skill.Power / 100);
                                        }
                                        PlayerSkill1.Value = sec * i;
                                        TurnCount++;
                                        break;
                                }
                                await Task.Delay(1000);
                            } else {
                                break;
                            }
                        }
                        if (EndCheck == false) {
                            TurnCount = 0;
                            player.DamageRelieve += skill.Power / 100;
                            PlayerSkill1.Value = 0;
                        }
                        break;
                }
            } while (EndCheck == false);
        }

        async void PlayerAction2() {
            var skill = player.EquipSkillList[1];
            var converter = new System.Windows.Media.BrushConverter();
            int TurnCount = 0;
            int damage;
            do {
                PlayerSkill2.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                //バーが最大になるまで繰り返す
                for (int i = 0; i < 101; i++) {
                    if (EndCheck == false) {
                        PlayerSkill2.Value = i;
                        await Task.Delay(skill.CD);
                    } else {
                        break;
                    }
                }
                if (EndCheck == true) {
                    continue;
                }
                switch (skill.Category) {
                    case "即時":
                        PlayerSkill2.Value = 0;
                        damage = player.PowerSet(skill);
                        tbText.Text = player.Name + "の" + skill.Name + "!　" + enemy.Name + "に" + damage + "のダメージ! (" + EnemyHPBar.Value + "→" + (EnemyHPBar.Value - damage) + ")\r\n" + tbText.Text;
                        EnemyHPBar.Value -= damage;
                        EnemyHPText.Text = EnemyHPBar.Value.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                        PlayerDeadCheck();
                        break;
                    case "持続":
                        tbText.Text = player.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                        int sec = 100 / (skill.SustainTime / 10);
                        damage = player.PowerSet(skill);
                        PlayerSkill2.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                        for (int i = skill.SustainTime / 10; i > 0; i--) {
                            if (EndCheck == false) {
                                switch (skill.Type) {
                                    case "攻撃":
                                        enemy.HP = (int)(EnemyHPBar.Value);
                                        enemy.HP -= damage;
                                        EnemyHPBar.Value -= damage;
                                        EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                        PlayerSkill2.Value = sec * i;
                                        PlayerDeadCheck();
                                        break;
                                    case "回復":
                                        player.HP = (int)(PlayerHPBar.Value);
                                        player.HP += damage;
                                        PlayerHPBar.Value += damage;
                                        if (player.HP > PlayerHPBar.Maximum) {
                                            player.HP = (int)(PlayerHPBar.Maximum);
                                        }
                                        PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                        PlayerSkill2.Value = sec * i;
                                        break;
                                    case "防御":
                                        if (TurnCount == 0) {
                                            player.DamageRelieve = player.DamageRelieve * 1 - (skill.Power / 100);
                                        }
                                        PlayerSkill2.Value = sec * i;
                                        TurnCount++;
                                        break;
                                }
                                await Task.Delay(1000);
                            } else {
                                break;
                            }
                        }
                        if (EndCheck == false) {
                            TurnCount = 0;
                            player.DamageRelieve += skill.Power / 100;
                            PlayerSkill2.Value = 0;
                        }
                        break;
                }
            } while (EndCheck == false);
        }

        async void PlayerAction3() {
            var skill = player.EquipSkillList[2];
            var converter = new System.Windows.Media.BrushConverter();
            int TurnCount = 0;
            int damage;
            do {
                PlayerSkill3.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                //バーが最大になるまで繰り返す
                for (int i = 0; i < 101; i++) {
                    if (EndCheck == false) {
                        PlayerSkill3.Value = i;
                        await Task.Delay(skill.CD);
                    } else {
                        break;
                    }
                }
                if (EndCheck == true) {
                    continue;
                }
                switch (skill.Category) {
                    case "即時":
                        PlayerSkill3.Value = 0;
                        damage = player.PowerSet(skill);
                        tbText.Text = player.Name + "の" + skill.Name + "!　" + enemy.Name + "に" + damage + "のダメージ! (" + EnemyHPBar.Value + "→" + (EnemyHPBar.Value - damage) + ")\r\n" + tbText.Text;
                        EnemyHPBar.Value -= damage;
                        EnemyHPText.Text = EnemyHPBar.Value.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                        PlayerDeadCheck();
                        break;
                    case "持続":
                        tbText.Text = player.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                        int sec = 100 / (skill.SustainTime / 10);
                        damage = player.PowerSet(skill);
                        PlayerSkill3.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                        for (int i = skill.SustainTime / 10; i > 0; i--) {
                            if (EndCheck == false) {
                                switch (skill.Type) {
                                    case "攻撃":
                                        enemy.HP = (int)(EnemyHPBar.Value);
                                        enemy.HP -= damage;
                                        EnemyHPBar.Value -= damage;
                                        EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                        PlayerSkill3.Value = sec * i;
                                        PlayerDeadCheck();
                                        break;
                                    case "回復":
                                        player.HP = (int)(PlayerHPBar.Value);
                                        player.HP += damage;
                                        PlayerHPBar.Value += damage;
                                        if (player.HP > PlayerHPBar.Maximum) {
                                            player.HP = (int)(PlayerHPBar.Maximum);
                                        }
                                        PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                        PlayerSkill3.Value = sec * i;
                                        break;
                                    case "防御":
                                        if (TurnCount == 0) {
                                            player.DamageRelieve = player.DamageRelieve * 1 - (skill.Power / 100);
                                        }
                                        PlayerSkill3.Value = sec * i;
                                        TurnCount++;
                                        break;
                                }
                                await Task.Delay(1000);
                            } else {
                                break;
                            }
                        }
                        if (EndCheck == false) {
                            TurnCount = 0;
                            player.DamageRelieve += skill.Power / 100;
                            PlayerSkill3.Value = 0;
                        }
                        break;
                }
            } while (EndCheck == false);
        }

        async void PlayerAction4() {
            var skill = player.EquipSkillList[3];
            var converter = new System.Windows.Media.BrushConverter();
            int TurnCount = 0;
            int damage;
            do {
                PlayerSkill4.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                //バーが最大になるまで繰り返す
                for (int i = 0; i < 101; i++) {
                    if (EndCheck == false) {
                        PlayerSkill4.Value = i;
                        await Task.Delay(skill.CD);
                    } else {
                        break;
                    }
                }
                if (EndCheck == true) {
                    continue;
                }
                switch (skill.Category) {
                    case "即時":
                        PlayerSkill4.Value = 0;
                        damage = player.PowerSet(skill);
                        tbText.Text = player.Name + "の" + skill.Name + "!　" + enemy.Name + "に" + damage + "のダメージ! (" + EnemyHPBar.Value + "→" + (EnemyHPBar.Value - damage) + ")\r\n" + tbText.Text;
                        EnemyHPBar.Value -= damage;
                        EnemyHPText.Text = EnemyHPBar.Value.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                        PlayerDeadCheck();
                        break;
                    case "持続":
                        tbText.Text = player.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                        int sec = 100 / (skill.SustainTime / 10);
                        damage = player.PowerSet(skill);
                        PlayerSkill4.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                        for (int i = skill.SustainTime / 10; i > 0; i--) {
                            if (EndCheck == false) {
                                switch (skill.Type) {
                                    case "攻撃":
                                        enemy.HP = (int)(EnemyHPBar.Value);
                                        enemy.HP -= damage;
                                        EnemyHPBar.Value -= damage;
                                        EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                        PlayerSkill4.Value = sec * i;
                                        PlayerDeadCheck();
                                        break;
                                    case "回復":
                                        player.HP = (int)(PlayerHPBar.Value);
                                        player.HP += damage;
                                        PlayerHPBar.Value += damage;
                                        if (player.HP > PlayerHPBar.Maximum) {
                                            player.HP = (int)(PlayerHPBar.Maximum);
                                        }
                                        PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                        PlayerSkill4.Value = sec * i;
                                        break;
                                    case "防御":
                                        if (TurnCount == 0) {
                                            player.DamageRelieve = player.DamageRelieve * 1 - (skill.Power / 100);
                                        }
                                        PlayerSkill4.Value = sec * i;
                                        TurnCount++;
                                        break;
                                }
                                await Task.Delay(1000);
                            } else {
                                break;
                            }
                        }
                        if (EndCheck == false) {
                            TurnCount = 0;
                            player.DamageRelieve += skill.Power / 100;
                            PlayerSkill4.Value = 0;
                        }
                        break;
                }
            } while (EndCheck == false);
        }
        async void PlayerAction5() {
            var skill = player.EquipSkillList[4];
            var converter = new System.Windows.Media.BrushConverter();
            int TurnCount = 0;
            int damage;
            do {
                PlayerSkill5.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                //バーが最大になるまで繰り返す
                for (int i = 0; i < 101; i++) {
                    if (EndCheck == false) {
                        PlayerSkill5.Value = i;
                        await Task.Delay(skill.CD);
                    } else {
                        break;
                    }
                }
                if (EndCheck == true) {
                    continue;
                }
                switch (skill.Category) {
                    case "即時":
                        PlayerSkill5.Value = 0;
                        damage = player.PowerSet(skill);
                        tbText.Text = player.Name + "の" + skill.Name + "!　" + enemy.Name + "に" + damage + "のダメージ! (" + EnemyHPBar.Value + "→" + (EnemyHPBar.Value - damage) + ")\r\n" + tbText.Text;
                        EnemyHPBar.Value -= damage;
                        EnemyHPText.Text = EnemyHPBar.Value.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                        PlayerDeadCheck();
                        break;
                    case "持続":
                        tbText.Text = player.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                        int sec = 100 / (skill.SustainTime / 10);
                        damage = player.PowerSet(skill);
                        PlayerSkill5.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                        for (int i = skill.SustainTime / 10; i > 0; i--) {
                            if (EndCheck == false) {
                                switch (skill.Type) {
                                    case "攻撃":
                                        enemy.HP = (int)(EnemyHPBar.Value);
                                        enemy.HP -= damage;
                                        EnemyHPBar.Value -= damage;
                                        EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                        PlayerSkill5.Value = sec * i;
                                        PlayerDeadCheck();
                                        break;
                                    case "回復":
                                        player.HP = (int)(PlayerHPBar.Value);
                                        player.HP += damage;
                                        PlayerHPBar.Value += damage;
                                        if (player.HP > PlayerHPBar.Maximum) {
                                            player.HP = (int)(PlayerHPBar.Maximum);
                                        }
                                        PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                        PlayerSkill5.Value = sec * i;
                                        break;
                                    case "防御":
                                        if (TurnCount == 0) {
                                            player.DamageRelieve = player.DamageRelieve * 1 - (skill.Power / 100);
                                        }
                                        PlayerSkill5.Value = sec * i;
                                        TurnCount++;
                                        break;
                                }
                                await Task.Delay(1000);
                            } else {
                                break;
                            }
                        }
                        if (EndCheck == false) {
                            TurnCount = 0;
                            player.DamageRelieve += skill.Power / 100;
                            PlayerSkill5.Value = 0;
                        }
                        break;
                }
            } while (EndCheck == false);
        }

        async void EnemyAction1() {
            if (EnemySkillONOFF == true) {
                var skill = enemy.EquipSkillList[0];
                var converter = new System.Windows.Media.BrushConverter();
                EnemySkill1.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                int TurnCount = 0;
                int damage;
                do {
                    //バーが最大になるまで繰り返す
                    for (int i = 0; i < 101; i++) {
                        if (EndCheck == false) {
                            EnemySkill1.Value = i;
                            await Task.Delay(skill.CD);
                        } else {
                            break;
                        }
                    }
                    if (EndCheck == true) {
                        continue;
                    }
                    switch (skill.Category) {
                        case "即時":
                            EnemySkill1.Value = 0;
                            damage = enemy.PowerSet(skill);
                            tbText.Text = enemy.Name + "の" + skill.Name + "!　" + player.Name + "に" + damage + "のダメージ! (" + PlayerHPBar.Value + "→" + (PlayerHPBar.Value - damage) + ")\r\n" + tbText.Text;
                            PlayerHPBar.Value -= damage;
                            PlayerHPText.Text = PlayerHPBar.Value.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                            PlayerDeadCheck();
                            break;
                        case "持続":
                            tbText.Text = enemy.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                            int sec = 100 / (skill.SustainTime / 10);
                            damage = enemy.PowerSet(skill);
                            EnemySkill1.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                            for (int i = skill.SustainTime / 10; i > 0; i--) {
                                if (EndCheck == false) {
                                    switch (skill.Type) {
                                        case "攻撃":
                                            player.HP = (int)(PlayerHPBar.Value);
                                            player.HP -= damage;
                                            PlayerHPBar.Value -= damage;
                                            PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                            EnemySkill1.Value = sec * i;
                                            PlayerDeadCheck();
                                            break;
                                        case "回復":
                                            enemy.HP = (int)(EnemyHPBar.Value);
                                            enemy.HP += damage;
                                            EnemyHPBar.Value += damage;
                                            if (enemy.HP > EnemyHPBar.Maximum) {
                                                enemy.HP = (int)(EnemyHPBar.Maximum);
                                            }
                                            EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                            EnemySkill1.Value = sec * i;
                                            break;
                                        case "防御":
                                            if (TurnCount == 0) {
                                                enemy.DamageRelieve = enemy.DamageRelieve * 1 - (skill.Power / 100);
                                            }
                                            EnemySkill1.Value = sec * i;
                                            TurnCount++;
                                            break;
                                    }
                                    await Task.Delay(1000);
                                } else {
                                    break;
                                }
                            }
                            if (EndCheck == false) {
                                TurnCount = 0;
                                enemy.DamageRelieve += skill.Power / 100;
                                EnemySkill1.Value = 0;
                            }
                            break;
                    }
                } while (EndCheck == false);
            }
        }

        async void EnemyAction2() {
            if (EnemySkillONOFF == true) {
                var skill = enemy.EquipSkillList[1];
                var converter = new System.Windows.Media.BrushConverter();
                EnemySkill2.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                int TurnCount = 0;
                int damage;
                do {
                    //バーが最大になるまで繰り返す
                    for (int i = 0; i < 101; i++) {
                        if (EndCheck == false) {
                            EnemySkill2.Value = i;
                            await Task.Delay(skill.CD);
                        } else {
                            break;
                        }
                    }
                    if (EndCheck == true) {
                        continue;
                    }
                    switch (skill.Category) {
                        case "即時":
                            EnemySkill2.Value = 0;
                            damage = enemy.PowerSet(skill);
                            tbText.Text = enemy.Name + "の" + skill.Name + "!　" + player.Name + "に" + damage + "のダメージ! (" + PlayerHPBar.Value + "→" + (PlayerHPBar.Value - damage) + ")\r\n" + tbText.Text;
                            PlayerHPBar.Value -= damage;
                            PlayerHPText.Text = PlayerHPBar.Value.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                            PlayerDeadCheck();
                            break;
                        case "持続":
                            tbText.Text = enemy.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                            int sec = 100 / (skill.SustainTime / 10);
                            damage = enemy.PowerSet(skill);
                            EnemySkill2.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                            for (int i = skill.SustainTime / 10; i > 0; i--) {
                                if (EndCheck == false) {
                                    switch (skill.Type) {
                                        case "攻撃":
                                            player.HP = (int)(PlayerHPBar.Value);
                                            player.HP -= damage;
                                            PlayerHPBar.Value -= damage;
                                            PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                            EnemySkill2.Value = sec * i;
                                            PlayerDeadCheck();
                                            break;
                                        case "回復":
                                            enemy.HP = (int)(EnemyHPBar.Value);
                                            enemy.HP += damage;
                                            EnemyHPBar.Value += damage;
                                            if (enemy.HP > EnemyHPBar.Maximum) {
                                                enemy.HP = (int)(EnemyHPBar.Maximum);
                                            }
                                            EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                            EnemySkill2.Value = sec * i;
                                            break;
                                        case "防御":
                                            if (TurnCount == 0) {
                                                enemy.DamageRelieve = enemy.DamageRelieve * 1 - (skill.Power / 100);
                                            }
                                            EnemySkill2.Value = sec * i;
                                            TurnCount++;
                                            break;
                                    }
                                    await Task.Delay(1000);
                                } else {
                                    break;
                                }
                            }
                            if (EndCheck == false) {
                                TurnCount = 0;
                                enemy.DamageRelieve += skill.Power / 100;
                                EnemySkill2.Value = 0;
                            }
                            break;
                    }
                } while (EndCheck == false);
            }
        }

        async void EnemyAction3() {
            if (EnemySkillONOFF == true) {
                var skill = enemy.EquipSkillList[2];
                var converter = new System.Windows.Media.BrushConverter();
                EnemySkill3.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                int TurnCount = 0;
                int damage;
                do {
                    //バーが最大になるまで繰り返す
                    for (int i = 0; i < 101; i++) {
                        if (EndCheck == false) {
                            EnemySkill3.Value = i;
                            await Task.Delay(skill.CD);
                        } else {
                            break;
                        }
                    }
                    if (EndCheck == true) {
                        continue;
                    }
                    switch (skill.Category) {
                        case "即時":
                            EnemySkill3.Value = 0;
                            damage = enemy.PowerSet(skill);
                            tbText.Text = enemy.Name + "の" + skill.Name + "!　" + player.Name + "に" + damage + "のダメージ! (" + PlayerHPBar.Value + "→" + (PlayerHPBar.Value - damage) + ")\r\n" + tbText.Text;
                            PlayerHPBar.Value -= damage;
                            PlayerHPText.Text = PlayerHPBar.Value.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                            PlayerDeadCheck();
                            break;
                        case "持続":
                            tbText.Text = enemy.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                            int sec = 100 / (skill.SustainTime / 10);
                            damage = enemy.PowerSet(skill);
                            EnemySkill3.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                            for (int i = skill.SustainTime / 10; i > 0; i--) {
                                if (EndCheck == false) {
                                    switch (skill.Type) {
                                        case "攻撃":
                                            player.HP = (int)(PlayerHPBar.Value);
                                            player.HP -= damage;
                                            PlayerHPBar.Value -= damage;
                                            PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                            EnemySkill3.Value = sec * i;
                                            PlayerDeadCheck();
                                            break;
                                        case "回復":
                                            enemy.HP = (int)(EnemyHPBar.Value);
                                            enemy.HP += damage;
                                            EnemyHPBar.Value += damage;
                                            if (enemy.HP > EnemyHPBar.Maximum) {
                                                enemy.HP = (int)(EnemyHPBar.Maximum);
                                            }
                                            EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                            EnemySkill3.Value = sec * i;
                                            break;
                                        case "防御":
                                            if (TurnCount == 0) {
                                                enemy.DamageRelieve = enemy.DamageRelieve * 1 - (skill.Power / 100);
                                            }
                                            EnemySkill3.Value = sec * i;
                                            TurnCount++;
                                            break;
                                    }
                                    await Task.Delay(1000);
                                } else {
                                    break;
                                }
                            }
                            if (EndCheck == false) {
                                TurnCount = 0;
                                enemy.DamageRelieve += skill.Power / 100;
                                EnemySkill3.Value = 0;
                            }
                            break;
                    }
                } while (EndCheck == false);
            }
        }

        async void EnemyAction4() {
            if (EnemySkillONOFF == true) {
                var skill = enemy.EquipSkillList[3];
                var converter = new System.Windows.Media.BrushConverter();
                EnemySkill4.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                int TurnCount = 0;
                int damage;
                do {
                    //バーが最大になるまで繰り返す
                    for (int i = 0; i < 101; i++) {
                        if (EndCheck == false) {
                            EnemySkill4.Value = i;
                            await Task.Delay(skill.CD);
                        } else {
                            break;
                        }
                    }
                    if (EndCheck == true) {
                        continue;
                    }
                    switch (skill.Category) {
                        case "即時":
                            EnemySkill4.Value = 0;
                            damage = enemy.PowerSet(skill);
                            tbText.Text = enemy.Name + "の" + skill.Name + "!　" + player.Name + "に" + damage + "のダメージ! (" + PlayerHPBar.Value + "→" + (PlayerHPBar.Value - damage) + ")\r\n" + tbText.Text;
                            PlayerHPBar.Value -= damage;
                            PlayerHPText.Text = PlayerHPBar.Value.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                            PlayerDeadCheck();
                            break;
                        case "持続":
                            tbText.Text = enemy.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                            int sec = 100 / (skill.SustainTime / 10);
                            damage = enemy.PowerSet(skill);
                            EnemySkill4.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                            for (int i = skill.SustainTime / 10; i > 0; i--) {
                                if (EndCheck == false) {
                                    switch (skill.Type) {
                                        case "攻撃":
                                            player.HP = (int)(PlayerHPBar.Value);
                                            player.HP -= damage;
                                            PlayerHPBar.Value -= damage;
                                            PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                            EnemySkill4.Value = sec * i;
                                            PlayerDeadCheck();
                                            break;
                                        case "回復":
                                            enemy.HP = (int)(EnemyHPBar.Value);
                                            enemy.HP += damage;
                                            EnemyHPBar.Value += damage;
                                            if (enemy.HP > EnemyHPBar.Maximum) {
                                                enemy.HP = (int)(EnemyHPBar.Maximum);
                                            }
                                            EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                            EnemySkill4.Value = sec * i;
                                            break;
                                        case "防御":
                                            if (TurnCount == 0) {
                                                enemy.DamageRelieve = enemy.DamageRelieve * 1 - (skill.Power / 100);
                                            }
                                            EnemySkill4.Value = sec * i;
                                            TurnCount++;
                                            break;
                                    }
                                    await Task.Delay(1000);
                                } else {
                                    break;
                                }
                            }
                            if (EndCheck == false) {
                                TurnCount = 0;
                                enemy.DamageRelieve += skill.Power / 100;
                                EnemySkill4.Value = 0;
                            }
                            break;
                    }
                } while (EndCheck == false);
            }
        }

        async void EnemyAction5() {
            if (EnemySkillONOFF == true) {
                var skill = enemy.EquipSkillList[4];
                var converter = new System.Windows.Media.BrushConverter();
                EnemySkill5.Foreground = (Brush)converter.ConvertFromString("#1E90FF");
                int TurnCount = 0;
                int damage;
                do {
                    //バーが最大になるまで繰り返す
                    for (int i = 0; i < 101; i++) {
                        if (EndCheck == false) {
                            EnemySkill5.Value = i;
                            await Task.Delay(skill.CD);
                        } else {
                            break;
                        }
                    }
                    if (EndCheck == true) {
                        continue;
                    }
                    switch (skill.Category) {
                        case "即時":
                            EnemySkill5.Value = 0;
                            damage = enemy.PowerSet(skill);
                            tbText.Text = enemy.Name + "の" + skill.Name + "!　" + player.Name + "に" + damage + "のダメージ! (" + player.HP + "→" + (player.HP - damage) + ")\r\n" + tbText.Text;
                            PlayerHPBar.Value -= damage;
                            PlayerHPText.Text = PlayerHPBar.Value.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                            PlayerDeadCheck();
                            break;
                        case "持続":
                            tbText.Text = enemy.Name + "の" + skill.Name + "!\r\n" + tbText.Text;
                            int sec = 100 / (skill.SustainTime / 10);
                            damage = enemy.PowerSet(skill);
                            EnemySkill5.Foreground = (Brush)converter.ConvertFromString("#FFD700");
                            for (int i = skill.SustainTime / 10; i > 0; i--) {
                                if (EndCheck == false) {
                                    switch (skill.Type) {
                                        case "攻撃":
                                            player.HP = (int)(PlayerHPBar.Value);
                                            player.HP -= damage;
                                            PlayerHPBar.Value -= damage;
                                            PlayerHPText.Text = player.HP.ToString() + "/" + PlayerHPBar.Maximum.ToString();
                                            EnemySkill5.Value = sec * i;
                                            PlayerDeadCheck();
                                            break;
                                        case "回復":
                                            enemy.HP = (int)(EnemyHPBar.Value);
                                            enemy.HP += damage;
                                            EnemyHPBar.Value += damage;
                                            if (enemy.HP > EnemyHPBar.Maximum) {
                                                enemy.HP = (int)(EnemyHPBar.Maximum);
                                            }
                                            EnemyHPText.Text = enemy.HP.ToString() + "/" + EnemyHPBar.Maximum.ToString();
                                            EnemySkill5.Value = sec * i;
                                            break;
                                        case "防御":
                                            if (TurnCount == 0) {
                                                enemy.DamageRelieve = enemy.DamageRelieve * 1 - (skill.Power / 100);
                                            }
                                            EnemySkill5.Value = sec * i;
                                            TurnCount++;
                                            break;
                                    }
                                    await Task.Delay(1000);
                                } else {
                                    break;
                                }
                            }
                            if (EndCheck == false) {
                                TurnCount = 0;
                                enemy.DamageRelieve += skill.Power / 100;
                                EnemySkill5.Value = 0;
                            }
                            break;
                    }
                } while (EndCheck == false);
            }
        }

        public void PlayerDeadCheck() {
            if (EndCheck == false) {
                if (EnemyHPBar.Value <= 0 && PlayerHPBar.Value <= 0) {
                    timer.Stop();
                    EndCheck = true;
                    tbText.Text = enemy.Name + "は倒れた!\r\n" + tbText.Text;
                    tbText.Text = player.Name + "は倒れた!\r\n" + tbText.Text;
                    MessageBox.Show("引き分け!");
                }
                if (Time <= 0) {
                    timer.Stop();
                    EndCheck = true;
                    TimeText.Text = "0";
                    MessageBox.Show("制限時間超過により、あなたの勝利!");
                }
                if (EnemyHPBar.Value <= 0) {
                    timer.Stop();
                    EndCheck = true;
                    tbText.Text = enemy.Name + "は倒れた!\r\n" + tbText.Text;
                    MessageBox.Show("あなたの勝利!");
                }
                if (PlayerHPBar.Value <= 0) {
                    timer.Stop();
                    EndCheck = true;
                    tbText.Text = player.Name + "は倒れた!\r\n" + tbText.Text;
                    MessageBox.Show("あなたの負け!");
                }
            }
            if (EndCheck == true) {
                SkillSetButton.IsEnabled = true;
                ShopButton.IsEnabled = true;
                ResetButton.IsEnabled = true;
            }
        }

        private void SkillSetButton_Click(object sender, RoutedEventArgs e) {
            statusSetting.ParentWindow = this;
            statusSetting.Update(player);
            statusSetting.ShowDialog();
            player = statusSetting.player;
            PlayerSkillSet();
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e) {
            shop.ShowDialog();
            player = shop.player;
            PlayerSkillSet();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            tbText.Text = "";
            PlayerSkill1.Value = 0;
            PlayerSkill2.Value = 0;
            PlayerSkill3.Value = 0;
            PlayerSkill4.Value = 0;
            PlayerSkill5.Value = 0;
            EnemySkill1.Value = 0;
            EnemySkill2.Value = 0;
            EnemySkill3.Value = 0;
            EnemySkill4.Value = 0;
            EnemySkill5.Value = 0;
            StartButton.IsEnabled = true;
            ResetButton.IsEnabled = false;
            enemy.HP = (int)(EnemyHPBar.Maximum);
            EnemyHPBar.Maximum = enemy.HP;
            EnemyHPBar.Value = EnemyHPBar.Maximum;
            EnemyHPText.Text = EnemyHPBar.Value.ToString() + "/" + EnemyHPBar.Maximum.ToString();

            player.HP = (int)(PlayerHPBar.Maximum);
            PlayerHPBar.Maximum = player.HP;
            PlayerHPBar.Value = PlayerHPBar.Maximum;
            PlayerHPText.Text = PlayerHPBar.Value.ToString() + "/" + PlayerHPBar.Maximum.ToString();
        }

        private void Window_Closed(object sender, EventArgs e) {
            statusSetting.Close();
            shop.Close();
            startMenu.Close();
        }

        private void tbEnemySkill_Click(object sender, RoutedEventArgs e) {
            if (EnemySkillONOFF == true) {
                EnemySkillONOFF = false;
                tbEnemySkill.Content = "敵攻撃:OFF";
            } else {
                EnemySkillONOFF = true;
                tbEnemySkill.Content = "敵攻撃:ON";
            }
        }
    }
}
