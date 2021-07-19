using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PixelChaser
{ 
    public partial class DebugWindow : Form
    {
        public Game1 Data { get; private set; }
        private StringBuilder _sb = new StringBuilder();
        private PixelWorld _w { get { return Data.World; } }
        private Chaser _c { get { return Data.Chaser; } }

        public DebugWindow()
        {
            InitializeComponent();
        }
        public DebugWindow(Game1 data)
        {
            InitializeComponent();
            LoadGameData(data);
        }

        public void LoadGameData(Game1 data)
        {
            Data = data;
            _w.MovedDown += WorldTicked;


            ShowWorldInfo();

        }

        private void WorldTicked(object sender, EventArgs e)
        {
            ShowWorldInfo();
        }

        private void ShowWorldInfo()
        {
            _sb.Clear();
            _sb.AppendLine($"");
            _sb.AppendLine($"Size:  {_w.Width} x {_w.Height}");
            _sb.AppendLine($"Ticks: {_w.MoveCounter}");
            _sb.AppendLine($"Pixels: {_w.PixelUnits.Count}");
            _sb.AppendLine($"Chaser velocity:  [{_c.Velocity.X}, {_c.Velocity.Y}]     max: {_c.Velocity.MaxLimit}");


        }


        private void MaxLimitBox_ValueChanged(object sender, EventArgs e)
        {
          //  Data.World.Generator.MaxSpeed = (int)MaxLimitBox.Value;
        }

        private void MaxSizeBox_ValueChanged(object sender, EventArgs e)
        {
         //   Data.World.Generator.MaxSize = (int)MaxSizeBox.Value;
        }

        private void WorldPropertiesBtn_Click(object sender, EventArgs e)
        {
            PropertyWindow win = new PropertyWindow(Data.World);
            win.Show();
        }

        private void GeneratorPropertiesBtn_Click(object sender, EventArgs e)
        {
            PropertyWindow win = new PropertyWindow(Data.World.Generator);
            win.Show();

        }

        private void ChaserPropertiesBtn_Click(object sender, EventArgs e)
        {

            PropertyWindow win = new PropertyWindow(Data.Chaser);
            win.Show();
        }

        private void GamePropertiesBtn_Click(object sender, EventArgs e)
        {

            PropertyWindow win = new PropertyWindow(Data);
            win.Show();
        }
    }
}
