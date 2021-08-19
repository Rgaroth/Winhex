using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winhex.Admin.GUI
{
    public partial class SetNoteForm : Form
    {
        public event Action<string> OnGetNote;
        public SetNoteForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnGetNote?.Invoke(textBox1.Text);
            Close();
        }
    }
}
