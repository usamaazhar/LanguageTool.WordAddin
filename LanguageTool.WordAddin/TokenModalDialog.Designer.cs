namespace LanguageTool.WordAddin
{
    partial class TokenModalDialog
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
            this.modalPictureBox = new System.Windows.Forms.PictureBox();
            this.closeBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.modalPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // modalPictureBox
            // 
            this.modalPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modalPictureBox.Image = global::LanguageTool.WordAddin.Properties.Resources.addin_instr;
            this.modalPictureBox.Location = new System.Drawing.Point(0, 0);
            this.modalPictureBox.Name = "modalPictureBox";
            this.modalPictureBox.Size = new System.Drawing.Size(594, 430);
            this.modalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.modalPictureBox.TabIndex = 0;
            this.modalPictureBox.TabStop = false;
            // 
            // closeBTN
            // 
            this.closeBTN.BackColor = System.Drawing.Color.Gainsboro;
            this.closeBTN.Location = new System.Drawing.Point(512, 400);
            this.closeBTN.Name = "closeBTN";
            this.closeBTN.Size = new System.Drawing.Size(83, 31);
            this.closeBTN.TabIndex = 1;
            this.closeBTN.Text = "&Close";
            this.closeBTN.UseVisualStyleBackColor = false;
            this.closeBTN.Click += new System.EventHandler(this.closeBTN_Click);
            // 
            // TokenModalDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(594, 430);
            this.ControlBox = false;
            this.Controls.Add(this.closeBTN);
            this.Controls.Add(this.modalPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "TokenModalDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.modalPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox modalPictureBox;
        private System.Windows.Forms.Button closeBTN;
    }
}