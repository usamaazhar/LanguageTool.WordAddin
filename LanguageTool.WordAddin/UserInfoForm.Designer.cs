﻿namespace LanguageTool.WordAddin
{
    partial class UserInfoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.userIDTB = new System.Windows.Forms.TextBox();
            this.FetchBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID";
            // 
            // userIDTB
            // 
            this.userIDTB.Location = new System.Drawing.Point(115, 56);
            this.userIDTB.Name = "userIDTB";
            this.userIDTB.Size = new System.Drawing.Size(257, 22);
            this.userIDTB.TabIndex = 1;
            // 
            // FetchBTN
            // 
            this.FetchBTN.Location = new System.Drawing.Point(309, 111);
            this.FetchBTN.Name = "FetchBTN";
            this.FetchBTN.Size = new System.Drawing.Size(101, 34);
            this.FetchBTN.TabIndex = 2;
            this.FetchBTN.Text = "Fetch";
            this.FetchBTN.UseVisualStyleBackColor = true;
            this.FetchBTN.Click += new System.EventHandler(this.FetchBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter User ID to fetch latest templates";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(112, 81);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(165, 17);
            this.errorLabel.TabIndex = 4;
            this.errorLabel.Text = "*user id cannot be empty";
            this.errorLabel.Visible = false;
            // 
            // CancelBTN
            // 
            this.CancelBTN.Location = new System.Drawing.Point(12, 111);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(101, 34);
            this.CancelBTN.TabIndex = 5;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
            // 
            // UserInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 157);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FetchBTN);
            this.Controls.Add(this.userIDTB);
            this.Controls.Add(this.label1);
            this.Name = "UserInfoForm";
            this.Text = "UserInfoForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userIDTB;
        private System.Windows.Forms.Button FetchBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button CancelBTN;
    }
}