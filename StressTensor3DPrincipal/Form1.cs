using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StressTensor3DPrincipal
{
    public partial class Form1 : Form
    {
        private List<double> values;

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public Form1()
        {
            InitializeComponent();
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                values = textBox1.Text.Split(' ').Select(Double.Parse).ToList();
            }
            catch
            {
                values = new List<double>(new double[] { 40, -80, 60, -20, 5, 10 });
            }

            var tensor = new Tensor(values);
            var principals = tensor.toPrincipal();

            MessageBox.Show(string.Join(", ", principals.ToArray()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            values = new List<double>(new double[] { 40, -80, 60, -20, 5, 10 });
            var m = values.ToArray();
            var tensor = new Tensor(values);
            double[][] mat = new double[3][] {
                new double[]{1,2,0},
                new double[]{1,1,0},
                new double[]{0,0,1}
            };
            //double[][] mat = new double[2][] {
            //    new double[]{1,2},
            //    new double[]{1,1},
            //};

            mat = tensor.toArray();

            var p = tensor.toPrincipal();
            var det = tensor.CalculateDet(mat);
            var det2 = tensor.det;

        }
    }
}
