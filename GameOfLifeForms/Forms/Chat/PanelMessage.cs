using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeForms.Forms.Chat
{
    public partial class PanelMessage : Panel
    {
        public PanelMessage(string message)
        {
            InitializeComponent();
            labelMessage.Text = message;
        }

        public PanelMessage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
