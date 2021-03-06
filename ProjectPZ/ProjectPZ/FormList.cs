﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectPZ.DAL;
using System.Data.SqlClient;



namespace ProjectPZ
{
    public partial class FormList : Form
    {
        int start;
        int sortById;
        int sortByName;
        int size = 14;
        public FormList()
        {
            InitializeComponent();
        }

        private void FormList_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            start = 0;
            loadData();
            sortById=1;
            sortByName=0;
        }

        private void forwardBtn_Click(object sender, EventArgs e)
        {
            CategoriesDAL c = new CategoriesDAL();
            start = start + size;
            backBtn.Enabled = true;
            if (start >(c.CountCateg()/size)*size )
            {
                start = 0;
            }
            if (sortById == 1)
            {
                dataGridView1.DataSource = c.GetCategoriesSortedById(start, size);
            }
            if (sortByName == 1)
            {
                dataGridView1.DataSource = c.GetCategoriesSortedByName(start, size);
            }

        }
        private void loadData()
        {
            CategoriesDAL c = new CategoriesDAL();
            dataGridView1.DataSource = c.GetCategoriesSortedById(start,size);
            backBtn.Enabled = false;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            start = start - size;
            if (start < 0)
            {
                start = 0;
                backBtn.Enabled = false;
            }
            CategoriesDAL c = new CategoriesDAL();
            if (sortById == 1)
            {
                dataGridView1.DataSource = c.GetCategoriesSortedById(start, size);
            }
            if (sortByName == 1)
            {
                dataGridView1.DataSource = c.GetCategoriesSortedByName(start, size);
            }

        }

        private void sortByIdBtn_Click(object sender, EventArgs e)
        {
            CategoriesDAL c = new CategoriesDAL();
            dataGridView1.DataSource = c.GetCategoriesSortedById(start, size);
            backBtn.Enabled = false;
            sortByName = 0;
            sortById = 1;

        }

        private void sortByNameBtn_Click(object sender, EventArgs e)
        {
            CategoriesDAL c = new CategoriesDAL();
            dataGridView1.DataSource = c.GetCategoriesSortedByName(start, size);
            backBtn.Enabled = false;
            sortByName = 1;
            sortById = 0;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //CategoriesDAL c = new CategoriesDAL();
            SqlConnection con1 = new SqlConnection(@"Data Source=MARICHKA-ПК\SQLEXPRESS;Initial Catalog=DeviceCategory;Integrated Security=True");
            if (comboBox1.Text == "ID")
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT id_categ, categ_name FROM Categories WHERE id_categ LIKE '" + searchTxt.Text + "%'", con1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                //dataGridView1.DataSource = c.GetCategoryByID(textBox1.Text);
            }
            if (comboBox1.Text == "Name")
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT id_categ, categ_name FROM Categories WHERE categ_name LIKE '" + searchTxt.Text + "%'", con1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                //dataGridView1.DataSource = c.GetCategoryByName(searchTxt.Text);
            }
            if (searchTxt.Text == "")
                loadData();
        }

        private void endPageBtn_Click(object sender, EventArgs e)
        {
            start = 0;
            loadData();
        }

        private void endBtn_Click(object sender, EventArgs e)
        {
            CategoriesDAL c = new CategoriesDAL();
            start = (c.CountCateg() / size) * size;
            loadData();
            endBtn.Enabled = true;
        }
    }
}
