using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDocker.Model
{
    public class DirectorShip
    {
        private int dir_id;
        private string dir_userName;
        private string dir_pwd;
        private string dir_role;

        public DirectorShip() { }
        public DirectorShip(int dir_id, string dir_userName, string dir_pwd, string dir_role)
        {
            this.Dir_id = dir_id;
            this.Dir_userName = dir_userName;
            this.Dir_pwd = dir_pwd;
            this.Dir_role = dir_role;
        }

        public int Dir_id { get => dir_id; set => dir_id = value; }
        public string Dir_userName { get => dir_userName; set => dir_userName = value; }
        public string Dir_pwd { get => dir_pwd; set => dir_pwd = value; }
        public string Dir_role { get => dir_role; set => dir_role = value; }
        
    }
}
