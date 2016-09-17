using SMSBL;
using SMSBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StudentManager sm = new StudentManager();
            List<Student> ls = sm.SelectAllStudents();

            
            string strStudents = string.Empty;


            foreach (Student s in ls)
            {
                strStudents = strStudents + "\n" + s.ToString();
            }

            label1.Text = strStudents;
        }
    }
}
