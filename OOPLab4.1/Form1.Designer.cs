namespace OOPLab4._1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PaintBox = new System.Windows.Forms.Panel();
            this.checkBoxCtrl = new System.Windows.Forms.CheckBox();
            this.checkBoxCollision = new System.Windows.Forms.CheckBox();
            this.setFigure = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // PaintBox
            // 
            this.PaintBox.Location = new System.Drawing.Point(68, 58);
            this.PaintBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PaintBox.Name = "PaintBox";
            this.PaintBox.Size = new System.Drawing.Size(495, 234);
            this.PaintBox.TabIndex = 0;
            this.PaintBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBox_Paint);
            this.PaintBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PaintBox_MouseClick);
            // 
            // checkBoxCtrl
            // 
            this.checkBoxCtrl.AutoSize = true;
            this.checkBoxCtrl.Location = new System.Drawing.Point(69, 24);
            this.checkBoxCtrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxCtrl.Name = "checkBoxCtrl";
            this.checkBoxCtrl.Size = new System.Drawing.Size(84, 19);
            this.checkBoxCtrl.TabIndex = 1;
            this.checkBoxCtrl.Text = "Ctrl Button";
            this.checkBoxCtrl.UseVisualStyleBackColor = true;
            this.checkBoxCtrl.CheckedChanged += new System.EventHandler(this.checkBoxCtrl_CheckedChanged);
            // 
            // checkBoxCollision
            // 
            this.checkBoxCollision.AutoSize = true;
            this.checkBoxCollision.Location = new System.Drawing.Point(164, 24);
            this.checkBoxCollision.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxCollision.Name = "checkBoxCollision";
            this.checkBoxCollision.Size = new System.Drawing.Size(117, 19);
            this.checkBoxCollision.TabIndex = 2;
            this.checkBoxCollision.Text = "Multiple collision";
            this.checkBoxCollision.UseVisualStyleBackColor = true;
            this.checkBoxCollision.CheckedChanged += new System.EventHandler(this.checkBoxCollision_CheckedChanged);
            // 
            // setFigure
            // 
            this.setFigure.FormattingEnabled = true;
            this.setFigure.Location = new System.Drawing.Point(481, 12);
            this.setFigure.Name = "setFigure";
            this.setFigure.Size = new System.Drawing.Size(121, 23);
            this.setFigure.TabIndex = 3;
            this.setFigure.SelectedIndexChanged += new System.EventHandler(this.setFigure_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 338);
            this.Controls.Add(this.setFigure);
            this.Controls.Add(this.checkBoxCollision);
            this.Controls.Add(this.checkBoxCtrl);
            this.Controls.Add(this.PaintBox);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel PaintBox;
        private CheckBox checkBoxCtrl;
        private CheckBox checkBoxCollision;
        private ComboBox setFigure;
    }
}