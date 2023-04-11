using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvgDemoWinForms
{
    public partial class JsonViewForm : Form
    {
        public JsonViewForm()
            => InitializeComponent();
        
        public void Update(DocumentModel doc)
            => richTextBox1.Text = doc.ToJson();

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
