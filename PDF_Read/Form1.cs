using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;

namespace PDF_Read
{
    public partial class PDF_Read_Form : Form
    {
        public PDF_Read_Form()
        {
            InitializeComponent();

            this.btnFileOpen.Click += BtnFileOpen_Click;
        }


        private void BtnFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();                   // File Open
            fd.ShowDialog();

            if(fd.FileName != "")                                       // File Name Check
            {
                string str = Ex(fd.FileName);
                {
                    string[] arr = str.Split("가1");                    // split
                    string splstrF = arr[1].ToString().Substring(3);

                    this.textBox1.Text = splstrF;                       // Result Input
                }
            }
        }

        private string Ex(string sPath)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                using (Stream newpdfStream = new FileStream(sPath, FileMode.Open, FileAccess.Read))
                {
                    PdfReader pdfReader = new PdfReader(newpdfStream);

                    for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                    {
                        result.Append(PdfTextExtractor.GetTextFromPage(pdfReader, i, new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy())).Append("\r\n\r\n\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result.ToString();

        }
    }
}
