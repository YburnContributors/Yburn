using System.Windows.Forms;

namespace Yburn.QQonFire.UI
{
    public class MenuItemQQonFire : ToolStripMenuItem
    {
        public MenuItemQQonFire()
            : base()
        {
            InitializeComponent();
        }

        public ToolStripMenuItem MenuItemBinBounds;
        public ToolStripMenuItem MenuItemDirectPionDecayWidths;
        public ToolStripMenuItem MenuItemSuppression;
        public ToolStripMenuItem MenuItemMakeSnapshots;
        public ToolStripMenuItem MenuItemSnapshots;
        public ToolStripMenuItem MenuItemShowSnapshots;
        public ToolStripMenuItem MenuItemShowGamma;
        public ToolStripMenuItem MenuItemShowBranchingRatio;
        public ToolStripMenuItem MenuItemShowCumulativeMatrix;
        public ToolStripMenuItem MenuItemShowInverseCumulativeMatrix;
        public ToolStripMenuItem MenuItemShowInitialQQPopulations;
        public ToolStripMenuItem MenuItemShowProtonProtonDimuonDecays;
        public ToolStripMenuItem MenuItemShowFeedDown;
        public ToolStripMenuItem MenuItemShowSnapsX;
        public ToolStripMenuItem MenuItemShowSnapsY;
        public ToolStripMenuItem MenuItemShowSnapsXY;

        private void InitializeComponent()
        {
            this.MenuItemBinBounds = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDirectPionDecayWidths = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSuppression = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSnapshots = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMakeSnapshots = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowSnapshots = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowSnapsX = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowSnapsY = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowSnapsXY = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowGamma = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowBranchingRatio = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowCumulativeMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowInverseCumulativeMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowInitialQQPopulations = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowProtonProtonDimuonDecays = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemShowFeedDown = new System.Windows.Forms.ToolStripMenuItem();
            //
            // MenuItemBinBounds
            //
            this.MenuItemBinBounds.Name = "MenuItemBinBounds";
            this.MenuItemBinBounds.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MenuItemBinBounds.Size = new System.Drawing.Size(361, 24);
            this.MenuItemBinBounds.Text = "C&alculate centrality bin boundaries";
            //
            // MenuItemDirectPionDecayWidths
            //
            this.MenuItemDirectPionDecayWidths.Name = "MenuItemDirectPionDecayWidths";
            this.MenuItemDirectPionDecayWidths.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.MenuItemDirectPionDecayWidths.Size = new System.Drawing.Size(361, 24);
            this.MenuItemDirectPionDecayWidths.Text = "Calculate &direct pion decay widths";
            //
            // MenuItemSuppression
            //
            this.MenuItemSuppression.Name = "MenuItemSuppression";
            this.MenuItemSuppression.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.MenuItemSuppression.Size = new System.Drawing.Size(361, 24);
            this.MenuItemSuppression.Text = "Calculate &Y suppression";
            //
            // MenuItemSnapshots
            //
            this.MenuItemSnapshots.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemMakeSnapshots,
            this.MenuItemShowSnapshots});
            this.MenuItemSnapshots.Name = "MenuItemSnapshots";
            this.MenuItemSnapshots.Size = new System.Drawing.Size(361, 24);
            this.MenuItemSnapshots.Text = "&Snapshots";
            //
            // MenuItemMakeSnapshots
            //
            this.MenuItemMakeSnapshots.Name = "MenuItemMakeSnapshots";
            this.MenuItemMakeSnapshots.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.MenuItemMakeSnapshots.Size = new System.Drawing.Size(236, 24);
            this.MenuItemMakeSnapshots.Text = "&Make snapshots";
            //
            // MenuItemShowSnapshots
            //
            this.MenuItemShowSnapshots.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemShowSnapsX,
            this.MenuItemShowSnapsY,
            this.MenuItemShowSnapsXY});
            this.MenuItemShowSnapshots.Name = "MenuItemShowSnapshots";
            this.MenuItemShowSnapshots.Size = new System.Drawing.Size(236, 24);
            this.MenuItemShowSnapshots.Text = "Show S&napshots";
            //
            // MenuItemShowSnapsX
            //
            this.MenuItemShowSnapsX.Name = "MenuItemShowSnapsX";
            this.MenuItemShowSnapsX.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.X)));
            this.MenuItemShowSnapsX.Size = new System.Drawing.Size(351, 24);
            this.MenuItemShowSnapsX.Text = "Show snaps in the &X-plane";
            //
            // MenuItemShowSnapsY
            //
            this.MenuItemShowSnapsY.Name = "MenuItemShowSnapsY";
            this.MenuItemShowSnapsY.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.Y)));
            this.MenuItemShowSnapsY.Size = new System.Drawing.Size(351, 24);
            this.MenuItemShowSnapsY.Text = "Show snaps in the &Y-plane";
            //
            // MenuItemShowSnapsXY
            //
            this.MenuItemShowSnapsXY.Name = "MenuItemShowSnapsXY";
            this.MenuItemShowSnapsXY.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.Z)));
            this.MenuItemShowSnapsXY.Size = new System.Drawing.Size(351, 24);
            this.MenuItemShowSnapsXY.Text = "Show snaps in the XY-&plane";
            //
            // MenuItemShowGamma
            //
            this.MenuItemShowGamma.Name = "MenuItemShowGamma";
            this.MenuItemShowGamma.Size = new System.Drawing.Size(361, 24);
            this.MenuItemShowGamma.Text = "Show &gamma from input file";
            //
            // MenuItemShowBranchingRatio
            //
            this.MenuItemShowBranchingRatio.Name = "MenuItemShowBranchingRatio";
            this.MenuItemShowBranchingRatio.Size = new System.Drawing.Size(361, 24);
            this.MenuItemShowBranchingRatio.Text = "Show &branching ratio matrix";
            //
            // MenuItemShowCumulativeMatrix
            //
            this.MenuItemShowCumulativeMatrix.Name = "MenuItemShowCumulativeMatrix";
            this.MenuItemShowCumulativeMatrix.Size = new System.Drawing.Size(361, 24);
            this.MenuItemShowCumulativeMatrix.Text = "Show &cumulative matrix";
            //
            // MenuItemShowInverseCumulativeMatrix
            //
            this.MenuItemShowInverseCumulativeMatrix.Name = "MenuItemShowInverseCumulativeMatrix";
            this.MenuItemShowInverseCumulativeMatrix.Size = new System.Drawing.Size(361, 24);
            this.MenuItemShowInverseCumulativeMatrix.Text = "Show &inverse cumulative matrix";
            //
            // MenuItemShowInitialQQPopulations
            //
            this.MenuItemShowInitialQQPopulations.Name = "MenuItemShowInitialQQPopulations";
            this.MenuItemShowInitialQQPopulations.Size = new System.Drawing.Size(361, 24);
            this.MenuItemShowInitialQQPopulations.Text = "Show initial QQ &populations";
            //
            // MenuItemShowProtonProtonDimuonDecays
            //
            this.MenuItemShowProtonProtonDimuonDecays.Name = "MenuItemShowProtonProtonDimuonDecays";
            this.MenuItemShowProtonProtonDimuonDecays.Size = new System.Drawing.Size(361, 24);
            this.MenuItemShowProtonProtonDimuonDecays.Text = "Show pp dimuon deca&ys";
            //
            // MenuItemShowFeedDown
            //
            this.MenuItemShowFeedDown.Name = "MenuItemShowFeedDown";
            this.MenuItemShowFeedDown.Size = new System.Drawing.Size(361, 24);
            this.MenuItemShowFeedDown.Text = "Show Y1S &feed down";
            //
            //
            this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemBinBounds,
            this.MenuItemDirectPionDecayWidths,
            this.MenuItemSuppression,
            this.MenuItemSnapshots,
            this.MenuItemShowGamma,
            this.MenuItemShowBranchingRatio,
            this.MenuItemShowCumulativeMatrix,
            this.MenuItemShowInverseCumulativeMatrix,
            this.MenuItemShowProtonProtonDimuonDecays,
            this.MenuItemShowInitialQQPopulations,
            this.MenuItemShowFeedDown});
            this.Size = new System.Drawing.Size(84, 24);
            this.Text = "&QQonFire";
        }
    }
}