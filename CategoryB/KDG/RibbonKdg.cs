﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace KDG
{
    public partial class RibbonKdg
    {
        private void RibbonKdg_Load(object sender, RibbonUIEventArgs e)
        {
            
        }

        private void RIBNrowFiller_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.ActiveCell.FillRight();
        }
    }
}
