﻿using STROOP.Controls;
using STROOP.Managers;
using STROOP.Models;
using STROOP.Structs.Configurations;
using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace STROOP.Structs
{
    public static class WatchVariableSelectionUtilities
    {

        public static ContextMenuStrip CreateSelectionContextMenuStrip(WatchVariableFlowLayoutPanel panel)
        {
            Action<WatchVariableControlSettings> apply =
                (WatchVariableControlSettings settings) => panel.ApplySettingsToSelected(settings);

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem itemHighlight = new ToolStripMenuItem("Highlight...");
            ControlUtilities.AddDropDownItems(
                itemHighlight,
                new List<string>() { "Set Highlighted", "Set Not Highlighted" },
                new List<Action>()
                {
                    () => apply(new WatchVariableControlSettings(changeHighlighted: true, newHighlighted: true)),
                    () => apply(new WatchVariableControlSettings(changeHighlighted: true, newHighlighted: false)),
                });

            ToolStripMenuItem itemLock = new ToolStripMenuItem("Lock...");
            ControlUtilities.AddDropDownItems(
                itemLock,
                new List<string>() { "Set Locked", "Set Not Locked" },
                new List<Action>()
                {
                    () => apply(new WatchVariableControlSettings(changeLocked: true, newLocked: true)),
                    () => apply(new WatchVariableControlSettings(changeLocked: true, newLocked: false)),
                });

            ToolStripMenuItem itemCopy = new ToolStripMenuItem("Copy");
            ToolStripMenuItem itemPaste = new ToolStripMenuItem("Paste");

            ToolStripMenuItem itemRoundTo = new ToolStripMenuItem("Round to...");
            ToolStripMenuItem itemRoundToDefault = new ToolStripMenuItem("Default");
            itemRoundToDefault.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeRoundingLimit: true, changeRoundingLimitToDefault: true));
            ToolStripMenuItem itemRoundToNoRounding = new ToolStripMenuItem("No Rounding");
            itemRoundToNoRounding.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeRoundingLimit: true, newRoundingLimit: -1));
            List<ToolStripMenuItem> itemsRoundToNumDecimalPlaces = new List<ToolStripMenuItem>();
            for (int i = 0; i <= 10; i++)
            {
                int index = i;
                itemsRoundToNumDecimalPlaces.Add(new ToolStripMenuItem(index + " decimal place(s)"));
                itemsRoundToNumDecimalPlaces[index].Click += (sender, e) =>
                    apply(new WatchVariableControlSettings(
                        changeRoundingLimit: true, newRoundingLimit: index));
            }
            itemRoundTo.DropDownItems.Add(itemRoundToDefault);
            itemRoundTo.DropDownItems.Add(itemRoundToNoRounding);
            itemsRoundToNumDecimalPlaces.ForEach(setAllRoundingLimitsNumberItem =>
            {
                itemRoundTo.DropDownItems.Add(setAllRoundingLimitsNumberItem);
            });

            ToolStripMenuItem itemDisplayAsHex = new ToolStripMenuItem("Display as Hex...");
            ControlUtilities.AddDropDownItems(
                itemDisplayAsHex,
                new List<string>() { "Default", "Hex", "Decimal" },
                new List<Action>()
                {
                    () => apply(new WatchVariableControlSettings(changeDisplayAsHex: true, changeDisplayAsHexToDefault: true)),
                    () => apply(new WatchVariableControlSettings(changeDisplayAsHex: true, newDisplayAsHex: true)),
                    () => apply(new WatchVariableControlSettings(changeDisplayAsHex: true, newDisplayAsHex: false)),
                });

            ToolStripMenuItem itemAngleSigned = new ToolStripMenuItem("Angle: Signed...");
            ToolStripMenuItem itemAngleSignedDefault = new ToolStripMenuItem("Default");
            itemAngleSignedDefault.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeAngleSigned: true, changeAngleSignedToDefault: true));
            ToolStripMenuItem itemAngleSignedUnsigned = new ToolStripMenuItem("Unsigned");
            itemAngleSignedUnsigned.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeAngleSigned: true, newAngleSigned: false));
            ToolStripMenuItem itemAngleSignedSigned = new ToolStripMenuItem("Signed");
            itemAngleSignedSigned.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeAngleSigned: true, newAngleSigned: true));
            itemAngleSigned.DropDownItems.Add(itemAngleSignedDefault);
            itemAngleSigned.DropDownItems.Add(itemAngleSignedUnsigned);
            itemAngleSigned.DropDownItems.Add(itemAngleSignedSigned);

            ToolStripMenuItem itemAngleUnits = new ToolStripMenuItem("Angle: Units...");
            ToolStripMenuItem itemAngleUnitsDefault = new ToolStripMenuItem("Default");
            itemAngleUnitsDefault.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeAngleUnits: true, changeAngleUnitsToDefault: true));
            List<ToolStripMenuItem> itemsAngleUnitsValue = new List<ToolStripMenuItem>();
            foreach (AngleUnitType angleUnitType in Enum.GetValues(typeof(AngleUnitType)))
            {
                AngleUnitType angleUnitTypeFixed = angleUnitType;
                ToolStripMenuItem itemAngleUnitsValue = new ToolStripMenuItem(angleUnitTypeFixed.ToString());
                itemAngleUnitsValue.Click += (sender, e) =>
                    apply(new WatchVariableControlSettings(
                        changeAngleUnits: true, newAngleUnits: angleUnitTypeFixed));
                itemsAngleUnitsValue.Add(itemAngleUnitsValue);
            }
            itemAngleUnits.DropDownItems.Add(itemAngleUnitsDefault);
            itemsAngleUnitsValue.ForEach(setAllAngleUnitsValuesItem =>
            {
                itemAngleUnits.DropDownItems.Add(setAllAngleUnitsValuesItem);
            });

            ToolStripMenuItem itemAngleTruncateToMultipleOf16 = new ToolStripMenuItem("Angle: Truncate to Multiple of 16...");
            ToolStripMenuItem itemAngleConstrainToOneRevolution = new ToolStripMenuItem("Angle: Constrain to One Revolution...");

            ToolStripMenuItem itemAngleDisplayAsHex = new ToolStripMenuItem("Angle: Display as Hex...");
            ToolStripMenuItem itemAngleDisplayAsHexDefault = new ToolStripMenuItem("Default");
            itemAngleDisplayAsHexDefault.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeAngleHex: true, changeAngleHexToDefault: true));
            ToolStripMenuItem itemAngleDisplayAsHexHex = new ToolStripMenuItem("Hex");
            itemAngleDisplayAsHexHex.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeAngleHex: true, newAngleHex: true));
            ToolStripMenuItem itemAngleDisplayAsHexDecimal = new ToolStripMenuItem("Decimal");
            itemAngleDisplayAsHexDecimal.Click += (sender, e) =>
                apply(new WatchVariableControlSettings(
                    changeAngleHex: true, newAngleHex: false));
            itemAngleDisplayAsHex.DropDownItems.Add(itemAngleDisplayAsHexDefault);
            itemAngleDisplayAsHex.DropDownItems.Add(itemAngleDisplayAsHexHex);
            itemAngleDisplayAsHex.DropDownItems.Add(itemAngleDisplayAsHexDecimal);

            ToolStripMenuItem itemMove = new ToolStripMenuItem("Move");
            ToolStripMenuItem itemDelete = new ToolStripMenuItem("Delete");
            ToolStripMenuItem itemOpenController = new ToolStripMenuItem("Open Controller");
            ToolStripMenuItem itemAddToCustomTab = new ToolStripMenuItem("Add to Custom Tab");

            contextMenuStrip.Items.Add(itemHighlight);
            contextMenuStrip.Items.Add(itemLock);
            contextMenuStrip.Items.Add(itemCopy);
            contextMenuStrip.Items.Add(itemPaste);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(itemRoundTo);
            contextMenuStrip.Items.Add(itemDisplayAsHex);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(itemAngleSigned);
            contextMenuStrip.Items.Add(itemAngleUnits);
            contextMenuStrip.Items.Add(itemAngleTruncateToMultipleOf16);
            contextMenuStrip.Items.Add(itemAngleConstrainToOneRevolution);
            contextMenuStrip.Items.Add(itemAngleDisplayAsHex);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(itemMove);
            contextMenuStrip.Items.Add(itemDelete);
            contextMenuStrip.Items.Add(itemOpenController);
            contextMenuStrip.Items.Add(itemAddToCustomTab);

            return contextMenuStrip;
        }
        
    }
}