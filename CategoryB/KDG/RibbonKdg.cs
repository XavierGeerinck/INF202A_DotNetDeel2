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
        private void RibbonKdg_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void RIBNrowFiller_Click(object sender, RibbonControlEventArgs e)
        {
          Excel.Range r = (Excel.Range)Globals.ThisAddIn.Application.ActiveCell;
          for (int i = 1; i < 11; i++) {
            for (int j = 1; j < 11; j++) {
              Excel.Range cell = (Excel.Range)r[i, j];
              cell.Interior.Color = 36;
            }
          }
        }
    }
}
