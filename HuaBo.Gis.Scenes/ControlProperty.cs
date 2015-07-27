using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel.Composition;
using SuperMap.UI;
using SuperMap.Realspace;

namespace HuaBo.Gis.Scenes
{
    public partial class ControlProperty : DevExpress.XtraEditors.XtraUserControl
    {
        private SceneControl m_sceneControl;
        public ControlProperty(SceneControl sceneControl)
        {
            InitializeComponent();
            m_sceneControl = sceneControl;
            InitialControl();
        }

        public void InitialControl()
        {
            m_textSceneName.Text = m_sceneControl.Name;
            m_textSceneType.Text = m_sceneControl.Scene.Type == SceneType.Globe ? "球面" : "平面";
            m_textSceneFov.Text = m_sceneControl.Scene.CameraFOV + "";
            //立体设置
            if (m_sceneControl.Scene.IsStereoEnabled == false)
            {
                m_cmbStereoMode.SelectedIndex = 0;
                m_cmbParallaxMode.Enabled = false;
                m_textStereoEyeSeparation.Enabled = false;
                m_textStereoEyeAngle.Enabled = false;
            }
            else
            {
                if (m_sceneControl.Scene.StereoMode == StereoMode.Anaglyphic)
                {
                    m_cmbStereoMode.SelectedIndex = 1;
                }
                else if (m_sceneControl.Scene.StereoMode == StereoMode.HorizontalSplit)
                {
                    m_cmbStereoMode.SelectedIndex = 2;
                }
                else if (m_sceneControl.Scene.StereoMode == StereoMode.QuadBuffer)
                {
                    m_cmbStereoMode.SelectedIndex = 3;
                }
                else if (m_sceneControl.Scene.StereoMode == StereoMode.VerticalSplit)
                {
                    m_cmbStereoMode.SelectedIndex = 4;
                }
                m_cmbParallaxMode.Enabled = true;
                m_textStereoEyeSeparation.Enabled = true;
                m_textStereoEyeAngle.Enabled = true;
            }
            if (m_sceneControl.Scene.ParallaxMode == ParallaxMode.NegativeParallax)
            { m_cmbParallaxMode.SelectedIndex = 0; }
            else
            { m_cmbParallaxMode.SelectedIndex = 1; }
            m_textStereoEyeSeparation.Text = m_sceneControl.Scene.StereoEyeSeparation + "";
            m_textStereoEyeAngle.Text = m_sceneControl.Scene.StereoEyeAngle + "";
            //其他设置
            //1.罗盘
            if (m_sceneControl.NavigationControl.IsVisible)
            { m_checkNavigationControl.Checked = true; }
            else
            { m_checkNavigationControl.Checked = false; }
            //2.状态条
            if (m_sceneControl.IsStatusBarVisible)
            { m_checkStatusBar.Checked = true; }
            else
            { m_checkStatusBar.Checked = false; }
            //3.Fps
            if (m_sceneControl.IsFPSVisible)
            { m_checkFps.Checked = true; }
            else
            { m_checkFps.Checked = false; }
            //4.比例尺
            if (m_sceneControl.Scene.IsScaleLegendVisible)
            { m_checkScaleLegend.Checked = true; }
            else
            { m_checkScaleLegend.Checked = false; }
            //5.海洋
            if (m_sceneControl.Scene.Ocean.IsVisible)
            { m_checkOcean.Checked = true; }
            else
            { m_checkOcean.Checked = false; }
            //6.天气
            if (m_sceneControl.Scene.Atmosphere.IsVisible)
            { m_checkAtmosphere.Checked = true; }
            else
            { m_checkAtmosphere.Checked = false; }
            //7.经纬网
            if (m_sceneControl.Scene.LatLonGrid.IsVisible)
            { m_checkLatLonGrid.Checked = true; m_checkLatLonGridText.Enabled = true; }
            else
            { m_checkLatLonGrid.Checked = false; m_checkLatLonGridText.Enabled = false; }
            //8.经纬网标签
            if (m_sceneControl.Scene.LatLonGrid.IsTextVisible)
            { m_checkLatLonGridText.Checked = true; }
            else
            { m_checkLatLonGridText.Checked = false; }
            //9总是刷新
            if (m_sceneControl.IsAlwaysUpdate)
            { m_checkAllwaysUpdate.Checked = true; }
            else
            { m_checkAllwaysUpdate.Checked = false; }
        }

        //应该只能输入数字加上下限值
        private void m_textSceneFov_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double result = Convert.ToDouble(m_textSceneFov.Text);
                m_sceneControl.Scene.CameraFOV = result;
            }
            catch (Exception)
            {
                m_textSceneFov.Text = m_sceneControl.Scene.CameraFOV + "";
                XtraMessageBox.Show("提醒", "输入值有误！");
            }
        }

        private void m_cmbStereoMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_cmbStereoMode.SelectedIndex == 0)
            {
                m_sceneControl.Scene.IsStereoEnabled = false;
                m_cmbParallaxMode.Enabled = false;
                m_textStereoEyeSeparation.Enabled = false;
                m_textStereoEyeAngle.Enabled = false;
            }
            else
            {
                m_sceneControl.Scene.IsStereoEnabled = true;
                m_cmbParallaxMode.Enabled = true;
                m_textStereoEyeSeparation.Enabled = true;
                m_textStereoEyeAngle.Enabled = true;
                if (m_cmbStereoMode.SelectedIndex == 1)
                {
                    m_sceneControl.Scene.StereoMode = StereoMode.Anaglyphic;
                }
                else if (m_cmbStereoMode.SelectedIndex == 2)
                {
                    m_sceneControl.Scene.StereoMode = StereoMode.HorizontalSplit;
                }
                else if (m_cmbStereoMode.SelectedIndex == 3)
                {
                    m_sceneControl.Scene.StereoMode = StereoMode.QuadBuffer;
                }
                else if (m_cmbStereoMode.SelectedIndex == 4)
                {
                    m_sceneControl.Scene.StereoMode = StereoMode.VerticalSplit;
                }
            }
        }

        private void m_cmbParallaxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_cmbParallaxMode.SelectedIndex == 0)
            {
                m_sceneControl.Scene.ParallaxMode = ParallaxMode.NegativeParallax;
            }
            else if (m_cmbParallaxMode.SelectedIndex == 1)
            {
                m_sceneControl.Scene.ParallaxMode = ParallaxMode.PositiveParallax;
            }
        }

        private void m_textStereoEyeSeparation_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double result = Convert.ToDouble(m_textStereoEyeSeparation.Text);
                m_sceneControl.Scene.StereoEyeSeparation = result;
            }
            catch (Exception)
            {
                m_textStereoEyeSeparation.Text = m_sceneControl.Scene.StereoEyeSeparation + "";
                XtraMessageBox.Show("提醒", "输入值有误！");
            }
        }

        private void m_textStereoEyeAngle_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double result = Convert.ToDouble(m_textStereoEyeAngle.Text);
                m_sceneControl.Scene.StereoEyeAngle = result;
            }
            catch (Exception)
            {
                m_textStereoEyeAngle.Text = m_sceneControl.Scene.StereoEyeAngle + "";
                XtraMessageBox.Show("提醒", "输入值有误！");
            }
        }

        private void m_checkNavigationControl_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkNavigationControl.Checked)
            {
                m_sceneControl.NavigationControl.IsVisible = true;
            }
            else
            {
                m_sceneControl.NavigationControl.IsVisible = false;
            }
        }

        private void m_checkStatusBar_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkStatusBar.Checked)
            {
                m_sceneControl.IsStatusBarVisible = true;
            }
            else
            {
                m_sceneControl.IsStatusBarVisible = false;
            }
        }

        private void m_checkFps_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkFps.Checked)
            {
                m_sceneControl.IsFPSVisible = true;
            }
            else
            {
                m_sceneControl.IsFPSVisible = false;
            }
        }

        private void m_checkScaleLegend_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkScaleLegend.Checked)
            {
                m_sceneControl.Scene.IsScaleLegendVisible = true;
            }
            else
            {
                m_sceneControl.Scene.IsScaleLegendVisible = false;
            }
        }

        private void m_checkOcean_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkOcean.Checked)
            {
                m_sceneControl.Scene.Ocean.IsVisible = true;
            }
            else
            {
                m_sceneControl.Scene.Ocean.IsVisible = false;
            }
        }

        private void m_checkAtmosphere_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkAtmosphere.Checked)
            {
                m_sceneControl.Scene.Atmosphere.IsVisible = true;
            }
            else
            {
                m_sceneControl.Scene.Atmosphere.IsVisible = false;
            }
        }

        private void m_checkLatLonGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkLatLonGrid.Checked)
            {
                m_sceneControl.Scene.LatLonGrid.IsVisible = true;
                m_checkLatLonGridText.Enabled = true;
            }
            else
            {
                m_sceneControl.Scene.LatLonGrid.IsVisible = false;
                m_checkLatLonGridText.Enabled = false;
            }
        }

        private void m_checkLatLonGridText_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkLatLonGridText.Checked)
            {
                m_sceneControl.Scene.LatLonGrid.IsTextVisible = true;
            }
            else
            {
                m_sceneControl.Scene.LatLonGrid.IsTextVisible = false;
            }
        }

        private void m_checkAllwaysUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (m_checkAllwaysUpdate.Checked)
            {
                m_sceneControl.IsAlwaysUpdate = true;
            }
            else
            {
                m_sceneControl.IsAlwaysUpdate = false;
            }
        }
    }
}
