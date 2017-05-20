using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RoadReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void ShowStripText(string display)
        {
            for (long i = 1; i < 30; i++)
            {
                Thread.Sleep(50);
                Application.DoEvents();
                statusLabel.Text = display;
            }
            statusLabel.Text = "";
        }

        private void button1_Select_Click(object sender, EventArgs e)
        {
            ShowStripText("Select Autocad Objects!");
            dataSet = null;
            dataGrid1.DataSource = null;

            List<Datas> Report = mReport.GetReport();
            if (Report == null)
                ShowStripText("No valid Objects found.");
            else
            {
                dataSet = CreateDataSet(Report);
                dataGrid1.DataSource = dataSet;
                statusLabel.Text = string.Format("{0} Objects found.", Report.Count);
            }
        }

        /// <summary>
        /// Usage example:
        /// var listOfCars = new List<Car>() { new Car { Make = "Nissan", Model = "Maxima", CarYear = 2011 } };
        /// var dataSet = CreateDataSet(listOfCars);

        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataSet CreateDataSet<T>(List<T> list)
        {
            if (list == null || list.Count == 0) { return null; }

            var obj = list[0].GetType();
            var properties = obj.GetProperties();

            if (properties.Length == 0) { return null; }

            var dataSet = new DataSet();
            var dataTable = new DataTable();

            var columns = new DataColumn[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columns[i] = new DataColumn(properties[i].Name, properties[i].PropertyType);
            }

            dataTable.Columns.AddRange(columns);

            foreach (var item in list)
            {
                var dataRow = dataTable.NewRow();
                var itemProperties = item.GetType().GetProperties();

                for (int i = 0; i < itemProperties.Length; i++)
                {
                    dataRow[i] = itemProperties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(dataRow);
            }

            dataSet.Tables.Add(dataTable);
            return dataSet;
        }

     }
}
