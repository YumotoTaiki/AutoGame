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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoGameTest {
    /// <summary>
    /// Shop.xaml の相互作用ロジック
    /// </summary>
    public partial class Shop : Window {

        public Random rand = new Random();
        private Skill[] productList = new Skill[5];
        public Skill[] ProductList { get => productList; set => productList = value; }
        public Person player = new Person("あああ", 1);

        public Shop(Person person) {
            InitializeComponent();
            player = person;
            ProductSet();
            TextSet();
        }

        private void TextSet() {
            TxProductName1.Text = productList[0].Name;
            TxProductName2.Text = productList[1].Name;
            TxProductName3.Text = productList[2].Name;
            TxProductName4.Text = productList[3].Name;
            TxProductName5.Text = productList[4].Name;
            TxProductEffect1.Text = productList[0].Text;
            TxProductEffect2.Text = productList[1].Text;
            TxProductEffect3.Text = productList[2].Text;
            TxProductEffect4.Text = productList[3].Text;
            TxProductEffect5.Text = productList[4].Text;
            TxProductPrice1.Text = productList[0].Price.ToString() + " G";
            TxProductPrice2.Text = productList[1].Price.ToString() + " G";
            TxProductPrice3.Text = productList[2].Price.ToString() + " G";
            TxProductPrice4.Text = productList[3].Price.ToString() + " G";
            TxProductPrice5.Text = productList[4].Price.ToString() + " G";
        }

        private void ProductSet() {
            for (int i = 0; i < productList.Length; i++) {
                InstanceSkill skill = new InstanceSkill(rand.Next(7));
                productList[i] = skill;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e) {
            TxProductName1.Text = "";
            TxProductEffect1.Text = "うりきれ！";
            TxProductPrice1.Text = "";
            DuplicateCheck(productList[0]);
        }

        private void Button_Click2(object sender, RoutedEventArgs e) {
            TxProductName2.Text = "";
            TxProductEffect2.Text = "うりきれ！";
            TxProductPrice2.Text = "";
            DuplicateCheck(productList[1]);
        }

        private void Button_Click3(object sender, RoutedEventArgs e) {
            TxProductName3.Text = "";
            TxProductEffect3.Text = "うりきれ！";
            TxProductPrice3.Text = "";
            DuplicateCheck(productList[2]);
        }

        private void Button_Click4(object sender, RoutedEventArgs e) {
            TxProductName4.Text = "";
            TxProductEffect4.Text = "うりきれ！";
            TxProductPrice4.Text = "";
            DuplicateCheck(productList[3]);
        }

        private void Button_Click5(object sender, RoutedEventArgs e) {
            TxProductName5.Text = "";
            TxProductEffect5.Text = "うりきれ！";
            TxProductPrice5.Text = "";
            DuplicateCheck(productList[4]);
        }

        private void DuplicateCheck(Skill skill) {
            foreach (var item in player.EquipSkillList.Where(x => x.OriginalName == skill.Name)) {
                item.STRNum++;
                item.Name = item.OriginalName + "+" + item.STRNum;
            }
            foreach (var item in player.GetSkillList.Where(x => x.OriginalName == skill.Name)) {
                item.STRNum++;
                item.Name = item.OriginalName + "+" + item.STRNum;
            }
        }

        private void btProductChange_Click(object sender, RoutedEventArgs e) {
            ProductSet();
            TextSet();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e) {
            this.Hide();
        }
    }
}
