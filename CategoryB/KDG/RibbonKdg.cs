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

    private void btnMinMax_Click(object sender, RibbonControlEventArgs e) 
    {
      Excel.Range r = (Excel.Range)Globals.ThisAddIn.Application.Selection.Cells;

      for (int i = 1; i <= r.Rows.Count; i++) 
      {
        for (int j = 1; j <= r.Columns.Count; j++) 
        {
          Excel.Range cell = (Excel.Range)r.get_Item(i, j);

          double number;
          if (Double.TryParse(cell.Text, out number)) 
          {
            cell.Interior.ColorIndex = Color.RED;
            if (number >= 0.0) cell.Interior.ColorIndex = Color.GREEN;            
          }
        }
      }
    }

  }
}
