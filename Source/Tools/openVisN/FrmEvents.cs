﻿using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using openVisN.Components;

namespace openVisN
{
    public partial class FrmEvents : Form
    {
        public static string DefaultFile = @"C:\Unison\GPA\Demo\Events.txt";

        private readonly VisualizationFramework m_framework;

        public FrmEvents(VisualizationFramework framework)
        {
            InitializeComponent();
            m_framework = framework;
        }

        private void FrmEvents_Load(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Time", typeof(DateTime));
            DT.Columns.Add("Location", typeof(string));
            DT.Columns.Add("Cause", typeof(string));

            string[] lines = File.ReadAllLines(DefaultFile);
            for (int x = 1; x < lines.Length; x++)
            {
                DT.Rows.Add(lines[x].Split('\t'));
            }

            dataGridView1.DataSource = DT;
            dataGridView1.Columns["Time"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["Time"].Width = 150;
            dataGridView1.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Location"].FillWeight = 10;
            dataGridView1.Columns["Cause"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Cause"].FillWeight = 10;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dataGridView1, new object[] {true});
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DateTime date = (DateTime)dataGridView1[0, e.RowIndex].Value;
                m_framework.Framework.ChangeDateRange(date.AddMinutes(-2), date.AddMinutes(2));
            }
        }
    }
}