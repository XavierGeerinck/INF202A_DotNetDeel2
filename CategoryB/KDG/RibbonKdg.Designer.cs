namespace KDG
{
    partial class RibbonKdg : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonKdg()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.tab1 = this.Factory.CreateRibbonTab();
      this.MinMax = this.Factory.CreateRibbonGroup();
      this.btnMinMax = this.Factory.CreateRibbonButton();
      this.tab1.SuspendLayout();
      this.MinMax.SuspendLayout();
      // 
      // tab1
      // 
      this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
      this.tab1.Groups.Add(this.MinMax);
      this.tab1.Label = "TabAddIns";
      this.tab1.Name = "tab1";
      // 
      // MinMax
      // 
      this.MinMax.Items.Add(this.btnMinMax);
      this.MinMax.Label = "Numbers";
      this.MinMax.Name = "MinMax";
      // 
      // btnMinMax
      // 
      this.btnMinMax.Label = "Min - Max";
      this.btnMinMax.Name = "btnMinMax";
      this.btnMinMax.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnMinMax_Click);
      // 
      // RibbonKdg
      // 
      this.Name = "RibbonKdg";
      this.RibbonType = "Microsoft.Excel.Workbook";
      this.Tabs.Add(this.tab1);
      this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonKdg_Load);
      this.tab1.ResumeLayout(false);
      this.tab1.PerformLayout();
      this.MinMax.ResumeLayout(false);
      this.MinMax.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        //internal Microsoft.Office.Tools.Ribbon.RibbonDropDown drdColor;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup MinMax;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnMinMax;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonKdg RibbonKdg
        {
            get { return this.GetRibbon<RibbonKdg>(); }
        }
    }
}
