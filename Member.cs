using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library_management_system
{
    public partial class Member : Form
    {
        public Member()
        {
            InitializeComponent();
            load();
        }

        SqlConnection con = new SqlConnection("Data source=DESKTOP-N5IF2SJ\\SQLEXPRESS; Initial Catalog=slibrary; Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        string sql;
        bool Mode = true;
        string id;

        public void load()
        {
            sql = "select * from member";
            cmd = new SqlCommand(sql, con);
            con.Open();

            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);

            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string address = txtaddress.Text;
            string phone = txtphone.Text;



            if (Mode == true)
            {
                sql = "insert into member(name,address,phone) values(@name, @address,@phone)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Member created");

                txtname.Clear();
                txtaddress.Clear();
                txtphone.Clear();

                txtname.Focus();
                con.Close();

            }

            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update member set name=@name, address=@address, phone=@phone where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Member updated");
                txtname.Clear();
                //txtstatus.Items.Clear();
                //txtstatus.SelectedIndex = -1;
                txtname.Focus();
                con.Close();


            }
        }

        public void getid(string id)
        {
            sql = "select * from member where id= '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtname.Text = dr[1].ToString();
                txtaddress.Text = dr[2].ToString();
                txtphone.Text = dr[3].ToString();

            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getid(id);

            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from member where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Member deleted");
                txtname.Clear();
                //txtstatus.Items.Clear();
                // txtstatus.SelectedIndex = -1;
                txtname.Focus();
                con.Close();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Member_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.load();
        }
    }
}
