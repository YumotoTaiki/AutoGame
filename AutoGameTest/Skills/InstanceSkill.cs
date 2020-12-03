using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGameTest.Skills {
    class InstanceSkill : Skill{

        public InstanceSkill(int id) {
            Category = "即時";
            IndexSearch(id);
        }

        private void IndexSearch(int id) {
            switch (id) {
                case -1:
                    Name = "なし";
                    CD = 0;
                    Power = 0;
                    Text = "";
                    break;
                case 0:
                    Name = "攻撃";
                    CD = 10;
                    Power = 40;
                    Ability = "筋力";
                    Text = "敵に武器を振り降ろし、(筋力の40%)のダメージを与える";
                    break;
                case 1:
                    Name = "回転斬り";
                    CD = 50;
                    Power = 120;
                    Ability = "筋力";
                    Text = "体を回転させた勢いで攻撃し、(筋力の120%)のダメージを与える";
                    break;
                case 2:
                    Name = "溜め斬り";
                    CD = 80;
                    Power = 180;
                    Ability = "筋力";
                    Text = "渾身の力を込めた斬撃を放ち、(筋力の180%)のダメージを与える";
                    break;
                case 3:
                    Name = "ジャンプ斬り";
                    CD = 100;
                    Power = 250;
                    Ability = "筋力";
                    Text = "ジャンプしながら武器を振り降ろし、(筋力の250%)のダメージを与える";
                    break;
                case 4:
                    Name = "袈裟斬り";
                    CD = 150;
                    Power = 320;
                    Ability = "技術";
                    Text = "武器を斜めに振り降ろし、(技術の320%)のダメージを与える";
                    break;
                case 5:
                    Name = "ブレイドダンス";
                    CD = 30;
                    Power = 420;
                    Ability = "技術";
                    Text = "舞うような滑らかな攻撃で、(技術の420%)のダメージを与える";
                    break;
            }
        }
    }
}
