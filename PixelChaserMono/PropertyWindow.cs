using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PixelChaser
{
    public partial class PropertyWindow : Form
    {
        public PropertyWindow()
        {
            InitializeComponent();
        }
        public PropertyWindow(object targetObject)
        {
            InitializeComponent();
            LoadObject(targetObject);
        }

        public void LoadObject(object targetObject)
        {
            PropertyBox.SelectedObject = targetObject;
        }
    }
}
