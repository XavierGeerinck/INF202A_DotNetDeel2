using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;

namespace KDG
{
    public partial class RibbonKdg
    {
      private Excel.Application applicationObject;
        private void RibbonKdg_Load(object sender, RibbonUIEventArgs e)
        {
          applicationObject = new Excel.Application();
        }

        private void RIBNrowFiller_Click(object sender, RibbonControlEventArgs e)
        {
          Globals.ThisAddIn.Application.ActiveCell.Value2 = "hallo";
        }
    }
}
