using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;

namespace SvgDemoWinForms
{
    public partial class SvgViewForm : Form
    {
        public SvgViewForm()
            => InitializeComponent();

        public void Update(DocumentModel documentModel)
            => Update(documentModel.ToSvg());

        public void Update(SvgDocument svgDocument)
            => Update(svgDocument.GetXML());

        public void Update(string text)
            => richTextBox1.Text = text;

    }
}
