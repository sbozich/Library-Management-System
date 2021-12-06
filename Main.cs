using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_management_system
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            book b = new book();
            b.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            publisher p= new publisher();
            p.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            author a = new author();
            a.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lendbook lend = new lendbook();
            lend.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            returnbook r = new returnbook();
            r.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Category c = new Category();
            c.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            author a = new author();
            a.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            publisher p = new publisher();
            p.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            book b = new book();
            b.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Member m = new Member();
            m.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            lendbook l = new lendbook();
            l.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            returnbook r = new returnbook();
            r.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
