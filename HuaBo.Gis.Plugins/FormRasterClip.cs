using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Analyst.SpatialAnalyst;
using SuperMap.Data;

namespace HuaBo.Gis.Plugins
{
    public partial class FormRasterClip : Form
    {
        private Workspace m_workspace;
        private Dictionary<string, List<string>> datas;
        private List<string> datasources;

        public FormRasterClip(Workspace workspace)
        {
            InitializeComponent();
            m_workspace = workspace;

            datas = new Dictionary<string, List<string>>();
            datasources = new List<string>();

            foreach (Datasource datasource in m_workspace.Datasources)
            {
                List<string> datasets = new List<string>();
                foreach (Dataset dataset in datasource.Datasets)
                {
                    datasets.Add(dataset.Name);
                }
                datas.Add(datasource.Alias, datasets);
                datasources.Add(datasource.Alias);
            }

            m_cmbDataDatasource.DataSource = datas.Select(s => s.Key).ToList();
        }



        private void FormRasterClip_Load(object sender, EventArgs e)
        {

        }

        private void m_cmbDataDatasource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_cmbDataDatasource.Text != "")
            {
                m_cmbDataDataset.DataSource = datas[m_cmbDataDatasource.Text + ""].ToList();
            }
        }

        private void m_btnStart_Click(object sender, EventArgs e)
        {
            Work(backgroundWorker1);
        }

        private void Work(BackgroundWorker bk)
        {
            try
            {
                //x为横的分的数量，y为竖着分
                int xCount = (int)m_numerX.Value;
                int yCount = (int)m_numerY.Value;
                progressBar1.Maximum = xCount * yCount;

                Dataset dv = m_workspace.Datasources[m_cmbDataDatasource.Text].Datasets[m_cmbDataDataset.Text];
                Datasource datasource = m_workspace.Datasources[m_cmbDataDatasource.Text];
                Rectangle2D rec = dv.Bounds;

                double xLength = rec.Right - rec.Left;
                double yLengt = rec.Top - rec.Bottom;
                double xunit = xLength / xCount;
                double yunit = yLengt / yCount;


                for (int i = 0; i < xCount; i++)
                {
                    for (int j = 0; j < yCount; j++)
                    {
                        Rectangle2D recL = new Rectangle2D(rec.Left + i * xunit, rec.Bottom + j * yunit, rec.Left + (i + 1) * xunit, rec.Bottom + (j + 1) * yunit);
                        if (i == xCount - 1)
                        {
                            recL.Right = rec.Right;
                        }
                        if (j == yCount - 1)
                        {
                            recL.Top = rec.Top;
                        }
                        Point2Ds pt2ds = new Point2Ds();
                        pt2ds.AddRange(new Point2D[] {
                    new Point2D(recL.Left,recL.Bottom),
                    new Point2D(recL.Left,recL.Top),
                    new Point2D(recL.Right,recL.Top),
                    new Point2D(recL.Right,recL.Bottom)
                    });
                        GeoRegion region = new GeoRegion(pt2ds);
                        Dataset result = RasterClip.Clip(dv, region, true, false, datasource, "test" + i + "x" + j);
                        Console.WriteLine(DateTime.Now);
                        bk.ReportProgress(i * yCount + j + 1, String.Format("当前值是 {0}", i * yCount + j + 1));
                        if (result == null)
                        {
                            //System.Windows.Forms.MessageBox.Show("Test");
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End!");
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
