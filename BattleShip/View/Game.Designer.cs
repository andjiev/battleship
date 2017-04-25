namespace BattleShip
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPlayer = new System.Windows.Forms.DataGridView();
            this.dgvComputer = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlayer
            // 
            this.dgvPlayer.AllowUserToResizeColumns = false;
            this.dgvPlayer.AllowUserToResizeRows = false;
            this.dgvPlayer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayer.ColumnHeadersVisible = false;
            this.dgvPlayer.Location = new System.Drawing.Point(28, 28);
            this.dgvPlayer.MultiSelect = false;
            this.dgvPlayer.Name = "dgvPlayer";
            this.dgvPlayer.ReadOnly = true;
            this.dgvPlayer.RowHeadersVisible = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgvPlayer.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlayer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvPlayer.Size = new System.Drawing.Size(303, 303);
            this.dgvPlayer.TabIndex = 0;
            this.dgvPlayer.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPlayer_CellMouseDoubleClick);
            this.dgvPlayer.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPlayer_CellMouseDown);
            this.dgvPlayer.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPlayer_CellMouseMove);
            this.dgvPlayer.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPlayer_CellMouseUp);
            this.dgvPlayer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvPlayer_MouseUp);
            // 
            // dgvComputer
            // 
            this.dgvComputer.AllowUserToResizeColumns = false;
            this.dgvComputer.AllowUserToResizeRows = false;
            this.dgvComputer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComputer.ColumnHeadersVisible = false;
            this.dgvComputer.Location = new System.Drawing.Point(496, 28);
            this.dgvComputer.MultiSelect = false;
            this.dgvComputer.Name = "dgvComputer";
            this.dgvComputer.ReadOnly = true;
            this.dgvComputer.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgvComputer.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvComputer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvComputer.Size = new System.Drawing.Size(303, 303);
            this.dgvComputer.TabIndex = 1;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 583);
            this.Controls.Add(this.dgvComputer);
            this.Controls.Add(this.dgvPlayer);
            this.DoubleBuffered = true;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvPlayer;
        private System.Windows.Forms.DataGridView dgvComputer;
    }
}