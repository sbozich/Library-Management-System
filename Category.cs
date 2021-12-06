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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
            load();
        }

        SqlConnection con = new SqlConnection("Data source=DESKTOP-N5IF2SJ\\SQLEXPRESS; Initial Catalog=slibrary; Integrated Security=true;");
        SqlCommand cmd;
        SqlDataReader dr;
        string sql;
        bool Mode = true;
        string id;


        public void load()
        {
            sql = "select * from category";
            cmd = new SqlCommand(sql, con);
            con.Open();

            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while(dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);
            }

            con.Close();
        }

        public void getid(string id)
        {
            sql = "select * from category where id= '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                txtname.Text = dr[1].ToString();
                txtstatus.Text = dr[2].ToString();

            }
            con.Close();
        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                string catname = txtname.Text;
                //string status = txtstatus.SelectedItem.ToString();
                string status = txtstatus.Text;

                //id = dataGridView1.CurrentRow.Cells[0].Value.ToString();


            if (Mode == true)
                {
                
                sql = "insert into category(catname,status) values(@catname, @status)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@catname", catname);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category created");
                    txtname.Clear();
                    //txtstatus.Items.Clear();
                    txtstatus.SelectedIndex = -1;
                    txtname.Focus();
                    con.Close();

                }

                else
                {
                    sql = "update category set catname=@catname, status=@status where id=@id";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@catname", catname);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category updated");
                    txtname.Clear();
                    //txtstatus.Items.Clear();
                    txtstatus.SelectedIndex = -1;
                    txtname.Focus();
                    con.Close();
                }
            



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==dataGridView1.Columns["edit"].Index && e.RowIndex>=0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getid(id);

            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from category where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category deleted");
                txtname.Clear();
                //txtstatus.Items.Clear();
                txtstatus.SelectedIndex = -1;
                txtname.Focus();
                con.Close();


            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
