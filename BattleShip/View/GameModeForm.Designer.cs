namespace BattleShip.View
{
    partial class GameModeForm
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
            this.SelectGameModeLabel = new System.Windows.Forms.Label();
            this.PlayerVsAiButton = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectGameModeLabel
            // 
            this.SelectGameModeLabel.AutoSize = true;
            this.SelectGameModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectGameModeLabel.Location = new System.Drawing.Point(195, 38);
            this.SelectGameModeLabel.Name = "SelectGameModeLabel";
            this.SelectGameModeLabel.Size = new System.Drawing.Size(400, 42);
            this.SelectGameModeLabel.TabIndex = 0;
            this.SelectGameModeLabel.Text = "Select the game mode:";
            // 
            // PlayerVsAiButton
            // 
            this.PlayerVsAiButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayerVsAiButton.Location = new System.Drawing.Point(119, 109);
            this.PlayerVsAiButton.Name = "PlayerVsAiButton";
            this.PlayerVsAiButton.Size = new System.Drawing.Size(174, 56);
            this.PlayerVsAiButton.TabIndex = 1;
            this.PlayerVsAiButton.Text = "Sunk in Silence";
            this.PlayerVsAiButton.UseVisualStyleBackColor = true;
            this.PlayerVsAiButton.Click += new System.EventHandler(this.PlayerVsAiButton_Click);
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Start.Location = new System.Drawing.Point(629, 386);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(159, 52);
            this.Start.TabIndex = 3;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(454, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 57);
            this.button1.TabIndex = 4;
            this.button1.Text = "Movable Ships";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(119, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 56);
            this.button2.TabIndex = 5;
            this.button2.Text = "Speedy Rules";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(454, 185);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(174, 56);
            this.button3.TabIndex = 6;
            this.button3.Text = "Salvo";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(119, 256);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(174, 56);
            this.button4.TabIndex = 7;
            this.button4.Text = "FOFB";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(454, 256);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(174, 56);
            this.button5.TabIndex = 8;
            this.button5.Text = "Big Board";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // GameModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.PlayerVsAiButton);
            this.Controls.Add(this.SelectGameModeLabel);
            this.Name = "GameModeForm";
            this.Text = "Battleship";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SelectGameModeLabel;
        private System.Windows.Forms.Button PlayerVsAiButton;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}