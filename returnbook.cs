using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_management_system
{
    public partial class returnbook : Form
    {
        public returnbook()
        {
            InitializeComponent();
            load();
        }

        SqlConnection con = new SqlConnection("Data source=DESKTOP-N5IF2SJ\\SQLEXPRESS; Initial Catalog=slibrary; Integrated Security=true;");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        string sql;
        bool Mode = true;
        string id;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                cmd = new SqlCommand("select book,issuedate,returndate,DATEDIFF(dd,returndate,GETDATE())as elap from issuebook where memberid='"+ txtmid.Text+ "'",con);
                con.Open();
                dr = cmd.ExecuteReader();
                

                if(dr.Read())
                {
                    txtbname.Text = dr["book"].ToString();
                    txtrdate.Text = dr["returndate"].ToString();
                    string elap = dr["elap"].ToString();

                    int elapp = int.Parse(elap);
                    


                    if (elapp>0)
                    {
                        txtelap.Text = elap;
                        int fine = elapp * 100;
                        txtfine.Text = fine.ToString();

                    }
                    else
                    {
                        txtelap.Text = "0";
                        txtfine.Text = "0";
                    }
                    con.Close();

                }
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mid = txtmid.Text;
            string book = txtbname.Text;
            string rdate = txtrdate.Text;
            string elp = txtelap.Text;
            string fine = txtfine.Text;
            //string returndate = txtrdate.Value.ToString("yyy-MM-dd");

            sql = "insert into returnbook(memberid,book,returndate,elap,fine) values (@memberid,@book,@returndate,@elap,@fine)";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@memberid", mid);
            cmd.Parameters.AddWithValue("@book", book);
            cmd.Parameters.AddWithValue("@returndate", rdate);
            cmd.Parameters.AddWithValue("@elap", elp);
            cmd.Parameters.AddWithValue("@fine", fine);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Book returned");
            con.Close();
        }


        public void load()
        {
            sql = "select * from returnbook";
            cmd = new SqlCommand(sql, con);
            con.Open();

            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);

            }

            con.Close();
        }

        public void getid(string id)
        {
            sql = "select * from returnbook where id= '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtmid.Text = dr[1].ToString();
                txtbname.Text = dr[2].ToString();
                txtrdate.Text = dr[3].ToString();
                txtelap.Text = dr[4].ToString();
                txtfine.Text = dr[5].ToString();

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

                sql = "delete from returnbook where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted");
                txtmid.Clear();
                txtbname.Clear();
                //txtstatus.Items.Clear();
                // txtstatus.SelectedIndex = -1;
                txtmid.Focus();
                con.Close();


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
