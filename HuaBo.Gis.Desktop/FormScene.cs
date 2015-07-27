using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HuaBo.Gis.Interfaces;
using SuperMap.UI;


namespace HuaBo.Gis.Desktop
{
    public partial class FormScene : DevExpress.XtraEditors.XtraForm, IFormScene
    {
        public FormScene()
        {
            InitializeComponent();
            DistanceUnit = Unit.Meter;
            AreaUnit = Unit.Meter;
            VolumnUnit = Unit.Meter;

            m_sceneControl = new SceneControl();
            m_sceneControl.Name = this.Text;
            m_sceneControl.Dock = DockStyle.Fill;
            m_sceneControl.IsAlwaysUpdate = true;
            m_sceneControl.Action = Action3D.Pan2;
            this.Controls.Add(m_sceneControl);

            this.ToolChanged += (m, n) =>
            {
                if (n.OldTool != null)
                {
                    n.OldTool.UnRegisterEvent();
                }
                if (n.NewTool != null)
                {
                    n.NewTool.RegisterEvent();
                    m_sceneControl.Action = n.NewTool.Action;
                }
                if (n.NewTool == null)
                {
                    m_sceneControl.Action = Action3D.Pan2;
                }
            };
            this.CurrentTool = null;

            this.TextChanged += FormScene_TextChanged;
        }

        void FormScene_TextChanged(object sender, EventArgs e)
        {
            m_sceneControl.Name = this.Text;
        }


        private SceneControl m_sceneControl;
        public SuperMap.UI.SceneControl SceneControl
        {
            get
            {
                return this.m_sceneControl;
            }
            set
            {
                m_sceneControl = value;
            }
        }


        public event EventHandler<ToolChangedEventArgs> ToolChanged;
        private ITool m_currentTool;
        public ITool CurrentTool
        {
            get { return m_currentTool; }
            set
            {
                if (m_currentTool != value)
                {
                    ToolChanged(this, new ToolChangedEventArgs(m_currentTool, value));
                    m_currentTool = value;
                }
            }
        }

        public Unit DistanceUnit { get; set; }
        public Unit AreaUnit { get; set; }
        public Unit VolumnUnit { get; set; }



        


        private void FormScene_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_sceneControl != null)
            {
                m_sceneControl.Dispose();
            }
        }

        private void FormScene_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否需要保存？", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.m_sceneControl.Scene.Dispose();
                //this.Save();
            }
            else if (result == DialogResult.No)
            {
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}