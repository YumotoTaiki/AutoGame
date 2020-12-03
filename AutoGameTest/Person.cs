using AutoGameTest.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGameTest {
    public class Person {

        public MainWindow MainWindowObj;
        public Random rand = new Random();

        public bool PlayerCheck { get; set; } //プレイヤーか敵か
        public string Name { get; set; }
        public int HP { get; set; }  //体力
        public int ATK { get; set; } //攻撃力
        public int DEF { get; set; } //防御力
        public int AGI { get; set; } //敏捷
        public int STR { get; set; } //筋力
        public int DEX { get; set; } //技術
        public int INT { get; set; } //魔力
        public int FAI { get; set; } //信仰
        private Skill[] EquipskillList = new Skill[5];
        public Skill[] EquipSkillList { get => EquipskillList; set => EquipskillList = value; }
        public List<Skill> GetSkillList = new List<Skill> { };
        public double DamageRelieve = 1;

        public Person(string name, int num) {
            switch (num) {
                case 0:
                    Name = name;
                    HP = 100;
                    ATK = 10;
                    AGI = 100;
                    STR = 10;
                    DEX = 10;
                    INT = 10;
                    FAI = 10;
                    for (int i = 0; i < 5; i++) {
                        InstanceSkill skill = new InstanceSkill(-1);
                        EquipskillList[i] = skill;
                    }
                    for (int i = -1; i < 6; i++) {
                        InstanceSkill skill = new InstanceSkill(i);
                        GetSkillList.Add(skill);
                    }
                    for (int i = 0; i < 4; i++) {
                        TimerSkill skill = new TimerSkill(i);
                        GetSkillList.Add(skill);
                    }
                    break;
                case 1:
                    Name = name;
                    HP = 100;
                    ATK = 10;
                    AGI = 100;
                    STR = 10;
                    DEX = 10;
                    INT = 10;
                    FAI = 10;
                    for (int i = 0; i < 5; i++) {
                        InstanceSkill skill = new InstanceSkill(i);
                        EquipskillList[i] = skill;
                    }
                    break;
            }
        }

        internal int PowerSet(Skill skill) {
            int num = 100;
            switch (skill.Ability) {
                case "なし":
                    num = 100;
                    break;
                case "筋力":
                    num = STR;
                    break;
                case "技術":
                    num = DEX;
                    break;
                case "魔力":
                    num = INT;
                    break;
                case "信仰":
                    num = FAI;
                    break;
            }
            return (int)(num * (skill.Power / 100));
        }
    }
}
