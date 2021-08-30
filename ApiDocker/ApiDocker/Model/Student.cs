using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiDocker.Model
{
    public class Student
    {
        private int alu_id;
        private string alu_nm;
        private string alu_nr_tel;
        private DateTime alu_dt_nascimento;

        public Student() { }
        public Student(int alu_id, string alu_nm, string alu_nr_tel, DateTime alu_dt_nascimento)
        {
            this.Alu_id = alu_id;
            this.Alu_nm = alu_nm;
            this.Alu_nr_tel = alu_nr_tel;
            this.Alu_dt_nascimento = alu_dt_nascimento;
        }

        public int Alu_id { get => alu_id; set => alu_id = value; }
        public string Alu_nm { get => alu_nm; set => alu_nm = value; }
        public string Alu_nr_tel { get => alu_nr_tel; set => alu_nr_tel = value; }
        public DateTime Alu_dt_nascimento { get => alu_dt_nascimento; set => alu_dt_nascimento = value; }
    }
}
