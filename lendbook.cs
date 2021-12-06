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
    public partial class lendbook : Form
    {
        public lendbook()
        {
            InitializeComponent();
            load();
            book();
        }

        SqlConnection con = new SqlConnection("Data source=DESKTOP-N5IF2SJ\\SQLEXPRESS; Initial Catalog=slibrary; Integrated Security=true;");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        string sql;
        bool Mode = true;
        string id;

        public void load()
        {
            sql = "select * from issuebook";
            cmd = new SqlCommand(sql, con);
            con.Open();

            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);

            }

            con.Close();
        }


        public void book()
        {
            string query = "select * from book";
            cmd = new SqlCommand(query, con);
            con.Open();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            txtbook.DataSource = ds.Tables[0];
            txtbook.DisplayMember = "bname";
            txtbook.ValueMember = "id";
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtmid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                cmd = new SqlCommand("select * from member where id='"+txtmid.Text  +  "'  ",con);
                con.Open();
                dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    txtmname.Text = dr["name"].ToString();

                }

                else
                {
                    MessageBox.Show("Member not found");
                }
                con.Close();



            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mid = txtmid.Text;
            string mname = txtmname.Text;
            string book = txtbook.Text;
            string issuedate = txtidate.Value.ToString("yyy-MM-dd");
            string returndate = txtrdate.Value.ToString("yyy-MM-dd");

            if (Mode == true)
            {
                sql = "insert into issuebook(memberid,book,issuedate,returndate) values (@memberid,@book,@issuedate,@returndate)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@memberid", mid);
                cmd.Parameters.AddWithValue("@book", book);
                cmd.Parameters.AddWithValue("@issuedate", issuedate);
                cmd.Parameters.AddWithValue("@returndate", returndate);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book issued");
                con.Close();
            }


            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update issuebook set memberid=@memberid, book=@book, issuedate=@issuedate, returndate=@returndate where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@memberid", mid);
                cmd.Parameters.AddWithValue("@book", book);
                cmd.Parameters.AddWithValue("@issuedate", issuedate);
                cmd.Parameters.AddWithValue("@returndate", returndate);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Lending updated");
                txtmid.Clear();
                //txtstatus.Items.Clear();
                //txtstatus.SelectedIndex = -1;
                txtmid.Focus();
                con.Close();


            }



        }


        public void getid(string id)
        {
            sql = "select * from issuebook where id= '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtmid.Text = dr[1].ToString();
                txtbook.Text = dr[2].ToString();
                txtidate.Text = dr[3].ToString();
                txtrdate.Text = dr[4].ToString();

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

                sql = "delete from issuebook where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted");
                txtmid.Clear();
                //txtmname.Clear();
                //txtbook.Clear();
                //txtstatus.Items.Clear();
                // txtstatus.SelectedIndex = -1;
                txtmid.Focus();
                con.Close();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.load();
        }

        private void lendbook_Load(object sender, EventArgs e)
        {

        }
    }
}
