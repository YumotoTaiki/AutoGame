using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGameTest {
    public abstract class Skill {

        public string OriginalName { get; set; }
        public string Name { get; set; }
        public double Power { get; set; }
        public int CD { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public int SustainTime { get; set; }
        public string Ability { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int STRNum { get; set; } //強化回数
    }
}
