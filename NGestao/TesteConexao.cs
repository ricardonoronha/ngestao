using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGestao
{
    public partial class TesteConexao : Form
    {
        public TesteConexao()
        {
            InitializeComponent();
        }

        public TesteConexao(string resultLocal, string resultRedmine)
        {
            InitializeComponent();

            txtConexaoLocal.Text = resultLocal;
            txtConexaoRedmine.Text = resultRedmine;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
