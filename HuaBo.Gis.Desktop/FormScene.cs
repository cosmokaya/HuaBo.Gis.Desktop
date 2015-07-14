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
            m_sceneControl = new SceneControl();
            m_sceneControl.Dock = DockStyle.Fill;
            m_sceneControl.IsAlwaysUpdate = true;
            m_sceneControl.Action = Action3D.Pan2;
            this.Controls.Add(m_sceneControl);

            this.OperateChanged += (m, n) =>
            {
                if (n.NewOperateType == OperateType.Pan2)
                {
                    m_sceneControl.Action = Action3D.Pan2;
                }
            };
            this.OperateType = OperateType.Pan2;
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

        public event EventHandler<OperateChangedEventArgs> OperateChanged;
        private OperateType m_operateType;
        public OperateType OperateType
        {
            get { return m_operateType; }
            set
            {
                if (m_operateType != value)
                {
                    OperateChanged(this, new OperateChangedEventArgs(m_operateType, value));
                    m_operateType = value;
                }
            }
        }

        private void FormScene_Load(object sender, EventArgs e)
        {

        }


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