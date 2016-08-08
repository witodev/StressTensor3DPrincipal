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
                values = new List<double>(new double[] { 3, 2, -1, 0, 0, 0 });
            }

            var tensor = new Tensor();

            tensor.read(values);
            var principals = tensor.toPrincipal();

            MessageBox.Show(string.Join(", ", principals.ToArray()));
        }
    }
}
