﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using STROOP.Structs;
using STROOP.Utilities;
using System.Xml.Linq;
using STROOP.Structs.Configurations;
using System.Drawing.Drawing2D;
using STROOP.Interfaces;
using STROOP.Controls.Map.Objects;
using STROOP.Controls.Map.Semaphores;

namespace STROOP.Controls.Map.Trackers
{
    public partial class MapTracker : UserControl
    {
        private readonly MapTrackerFlowLayoutPanel _flowLayoutPanel;
        public readonly List<MapIconObject> MapObjectList;
        public readonly List<MapSemaphore> SemaphoreList;

        private bool _visible;
        private static readonly Image ImageEyeOpen = Properties.Resources.image_eye_open2;
        private static readonly Image ImageEyeClosed = Properties.Resources.image_eye_closed2;

        public MapTracker(
            MapTrackerFlowLayoutPanel flowLayoutPanel,
            List<MapIconObject> mapObjectList,
            List<MapSemaphore> semaphoreList)
        {
            InitializeComponent();

            _flowLayoutPanel = flowLayoutPanel;
            MapObjectList = new List<MapIconObject>(mapObjectList);
            SemaphoreList = new List<MapSemaphore>(semaphoreList);
            MapObjectList.ForEach(obj => Config.MapController.AddMapObject(obj));

            _visible = true;
        }

        private void MapTracker_Load(object sender, EventArgs e)
        {
            tableLayoutPanel.BorderWidth = 2;
            tableLayoutPanel.ShowBorder = true;

            comboBoxVisibilityType.DataSource = Enum.GetValues(typeof(MapTrackerVisibilityType));
            comboBoxOrderType.DataSource = Enum.GetValues(typeof(MapTrackerOrderType));
            comboBoxDisplayType.DataSource = Enum.GetValues(typeof(MapTrackerDisplayType));

            ControlUtilities.AddContextMenuStripFunctions(
                pictureBoxCog,
                new List<string>()
                {
                    "Hitbox Cylinder",
                    "Tangibility Radius",
                    "Draw Distance Radius",
                },
                new List<Action>()
                {
                    () => { },
                    () => { },
                    () => { },
                });
            pictureBoxCog.Click += (se, ev) => pictureBoxCog.ContextMenuStrip.Show(Cursor.Position);
        }

        public MapTrackerOrderType GetOrderType()
        {
            return (MapTrackerOrderType) comboBoxOrderType.SelectedItem;
        }

        private void trackBarSize_ValueChanged(object sender, EventArgs e)
        {
            const float minSize = 0.01f;
            const float maxSize = 0.20f;
            MapObjectList.ForEach(icon =>
            {
                icon.Size = minSize + (maxSize - minSize) *
                    (trackBarSize.Value - trackBarSize.Minimum)
                    / (trackBarSize.Maximum - trackBarSize.Minimum);
            });
        }

        private void trackBarOpacity_ValueChanged(object sender, EventArgs e)
        {
            MapObjectList.ForEach(icon =>
            {
                icon.Opacity = (float)(trackBarOpacity.Value - trackBarOpacity.Minimum)
                    / (trackBarOpacity.Maximum - trackBarOpacity.Minimum);
            });
        }

        private void checkBoxRotates_CheckedChanged(object sender, EventArgs e)
        {
            MapObjectList.ForEach(icon =>
            {
                icon.Rotates = checkBoxRotates.Checked;
            });
        }

        private void pictureBoxRedX_Click(object sender, EventArgs e)
        {
            _flowLayoutPanel.RemoveControl(this);
        }

        private void pictureBoxEye_Click(object sender, EventArgs e)
        {
            _visible = !_visible;
            pictureBoxEye.BackgroundImage = _visible ? ImageEyeOpen : ImageEyeClosed;
        }

        private void pictureBoxUpArrow_Click(object sender, EventArgs e)
        {
            _flowLayoutPanel.MoveUpControl(this);
        }

        private void pictureBoxDownArrow_Click(object sender, EventArgs e)
        {
            _flowLayoutPanel.MoveDownControl(this);
        }

        public void UpdateTracker()
        {
            if (SemaphoreList.Any(semaphore => !semaphore.IsUsed))
            {
                _flowLayoutPanel.RemoveControl(this);
            }
        }

        public void CleanUp()
        {
            MapObjectList.ForEach(obj => Config.MapController.RemoveMapObject(obj));
            SemaphoreList.ForEach(semaphore => semaphore.IsUsed = false);
        }
    }
}
