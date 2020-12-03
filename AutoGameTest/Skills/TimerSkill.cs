using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGameTest.Skills {
    class TimerSkill : Skill{

        public TimerSkill(int id) {
            Category = "持続";
            IndexSearch(id);
        }

        private void IndexSearch(int id) {
            switch (id) {
                case 0:
                    Name = "リジェネレーション";
                    CD = 10;
                    Power = 15;
                    SustainTime = 50;
                    Type = "回復";
                    Ability = "信仰";
                    Text = "聖なる光をまとい、5秒の間自分のHPを毎秒(信仰の15%)回復する";
                    break;
                case 1:
                    Name = "発火";
                    CD = 50;
                    Power = 15;
                    SustainTime = 30;
                    Type = "攻撃";
                    Ability = "魔力";
                    Text = "敵を魔法の火で燃やし、3秒の間毎秒(魔力の15%)のダメージを与える";
                    break;
                case 2:
                    Name = "聖光の縛め";
                    CD = 80;
                    Power = 10;
                    SustainTime = 80;
                    Type = "攻撃";
                    Ability = "信仰";
                    Text = "敵を光輪で拘束し、8秒の間毎秒(信仰のの10%)のダメージを与える";
                    break;
                case 3:
                    Name = "不屈の防御";
                    CD = 20;
                    Power = 50;
                    SustainTime = 100;
                    Type = "防御";
                    Ability = "なし";
                    Text = "自らの盾に意思を込め、10秒の間受けるダメージを50%減少させる";
                    break;
                case 4:
                    Name = "和平の布告";
                    CD = 20;
                    Power = 50;
                    SustainTime = 50;
                    Type = "CD操作";
                    Ability = "なし";
                    Text = "5秒の間敵のCDを停止させる";
                    break;
            }
        }
    }
}
