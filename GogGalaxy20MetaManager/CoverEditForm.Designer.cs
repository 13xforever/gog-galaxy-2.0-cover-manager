namespace GogGalaxy20MetaManager
{
	partial class CoverEditForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.steamCoverSelect = new System.Windows.Forms.RadioButton();
			this.galaxyCoverSelect = new System.Windows.Forms.RadioButton();
			this.steamCoverPicture = new System.Windows.Forms.PictureBox();
			this.galaxyCoverPicture = new System.Windows.Forms.PictureBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.steamBackgroundSelect = new System.Windows.Forms.RadioButton();
			this.galaxyBackgroundSelect = new System.Windows.Forms.RadioButton();
			this.steamBackgroundPicture = new System.Windows.Forms.PictureBox();
			this.galaxyBackgroundPicture = new System.Windows.Forms.PictureBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.steamIconSelect = new System.Windows.Forms.RadioButton();
			this.galaxyIconSelect = new System.Windows.Forms.RadioButton();
			this.steamIconPicture = new System.Windows.Forms.PictureBox();
			this.galaxyIconPicture = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.steamCoverPicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyCoverPicture)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.steamBackgroundPicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyBackgroundPicture)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.steamIconPicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyIconPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.steamCoverSelect);
			this.groupBox1.Controls.Add(this.galaxyCoverSelect);
			this.groupBox1.Controls.Add(this.steamCoverPicture);
			this.groupBox1.Controls.Add(this.galaxyCoverPicture);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(449, 267);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Cover";
			// 
			// steamCoverSelect
			// 
			this.steamCoverSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.steamCoverSelect.AutoSize = true;
			this.steamCoverSelect.Location = new System.Drawing.Point(252, 19);
			this.steamCoverSelect.Name = "steamCoverSelect";
			this.steamCoverSelect.Size = new System.Drawing.Size(14, 13);
			this.steamCoverSelect.TabIndex = 8;
			this.steamCoverSelect.Tag = "cover";
			this.steamCoverSelect.UseVisualStyleBackColor = true;
			// 
			// galaxyCoverSelect
			// 
			this.galaxyCoverSelect.AutoSize = true;
			this.galaxyCoverSelect.Checked = true;
			this.galaxyCoverSelect.Location = new System.Drawing.Point(184, 19);
			this.galaxyCoverSelect.Name = "galaxyCoverSelect";
			this.galaxyCoverSelect.Size = new System.Drawing.Size(14, 13);
			this.galaxyCoverSelect.TabIndex = 7;
			this.galaxyCoverSelect.TabStop = true;
			this.galaxyCoverSelect.Tag = "cover";
			this.galaxyCoverSelect.UseVisualStyleBackColor = true;
			// 
			// steamCoverPicture
			// 
			this.steamCoverPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.steamCoverPicture.Location = new System.Drawing.Point(272, 19);
			this.steamCoverPicture.Name = "steamCoverPicture";
			this.steamCoverPicture.Size = new System.Drawing.Size(171, 241);
			this.steamCoverPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.steamCoverPicture.TabIndex = 6;
			this.steamCoverPicture.TabStop = false;
			this.steamCoverPicture.Click += new System.EventHandler(this.steamCoverPicture_Click);
			// 
			// galaxyCoverPicture
			// 
			this.galaxyCoverPicture.Location = new System.Drawing.Point(6, 19);
			this.galaxyCoverPicture.Name = "galaxyCoverPicture";
			this.galaxyCoverPicture.Size = new System.Drawing.Size(171, 241);
			this.galaxyCoverPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.galaxyCoverPicture.TabIndex = 5;
			this.galaxyCoverPicture.TabStop = false;
			this.galaxyCoverPicture.Click += new System.EventHandler(this.galaxyCoverPicture_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.steamBackgroundSelect);
			this.groupBox2.Controls.Add(this.galaxyBackgroundSelect);
			this.groupBox2.Controls.Add(this.steamBackgroundPicture);
			this.groupBox2.Controls.Add(this.galaxyBackgroundPicture);
			this.groupBox2.Location = new System.Drawing.Point(12, 285);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(449, 91);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Background";
			// 
			// steamBackgroundSelect
			// 
			this.steamBackgroundSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.steamBackgroundSelect.AutoSize = true;
			this.steamBackgroundSelect.Location = new System.Drawing.Point(252, 19);
			this.steamBackgroundSelect.Name = "steamBackgroundSelect";
			this.steamBackgroundSelect.Size = new System.Drawing.Size(14, 13);
			this.steamBackgroundSelect.TabIndex = 12;
			this.steamBackgroundSelect.Tag = "banner";
			this.steamBackgroundSelect.UseVisualStyleBackColor = true;
			// 
			// galaxyBackgroundSelect
			// 
			this.galaxyBackgroundSelect.AutoSize = true;
			this.galaxyBackgroundSelect.Checked = true;
			this.galaxyBackgroundSelect.Location = new System.Drawing.Point(185, 19);
			this.galaxyBackgroundSelect.Name = "galaxyBackgroundSelect";
			this.galaxyBackgroundSelect.Size = new System.Drawing.Size(14, 13);
			this.galaxyBackgroundSelect.TabIndex = 11;
			this.galaxyBackgroundSelect.TabStop = true;
			this.galaxyBackgroundSelect.Tag = "banner";
			this.galaxyBackgroundSelect.UseVisualStyleBackColor = true;
			// 
			// steamBackgroundPicture
			// 
			this.steamBackgroundPicture.Location = new System.Drawing.Point(283, 19);
			this.steamBackgroundPicture.Name = "steamBackgroundPicture";
			this.steamBackgroundPicture.Size = new System.Drawing.Size(160, 65);
			this.steamBackgroundPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.steamBackgroundPicture.TabIndex = 10;
			this.steamBackgroundPicture.TabStop = false;
			this.steamBackgroundPicture.Click += new System.EventHandler(this.steamBackgroundPicture_Click);
			// 
			// galaxyBackgroundPicture
			// 
			this.galaxyBackgroundPicture.Location = new System.Drawing.Point(6, 19);
			this.galaxyBackgroundPicture.Name = "galaxyBackgroundPicture";
			this.galaxyBackgroundPicture.Size = new System.Drawing.Size(160, 65);
			this.galaxyBackgroundPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.galaxyBackgroundPicture.TabIndex = 9;
			this.galaxyBackgroundPicture.TabStop = false;
			this.galaxyBackgroundPicture.Click += new System.EventHandler(this.galaxyBackgroundPicture_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.cancelButton);
			this.groupBox3.Controls.Add(this.okButton);
			this.groupBox3.Controls.Add(this.steamIconSelect);
			this.groupBox3.Controls.Add(this.galaxyIconSelect);
			this.groupBox3.Controls.Add(this.steamIconPicture);
			this.groupBox3.Controls.Add(this.galaxyIconPicture);
			this.groupBox3.Location = new System.Drawing.Point(12, 382);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(449, 138);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Icon";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(226, 108);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 22;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(145, 108);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 21;
			this.okButton.Text = "Update";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// steamIconSelect
			// 
			this.steamIconSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.steamIconSelect.AutoSize = true;
			this.steamIconSelect.Location = new System.Drawing.Point(252, 19);
			this.steamIconSelect.Name = "steamIconSelect";
			this.steamIconSelect.Size = new System.Drawing.Size(14, 13);
			this.steamIconSelect.TabIndex = 20;
			this.steamIconSelect.Tag = "icon";
			this.steamIconSelect.UseVisualStyleBackColor = true;
			// 
			// galaxyIconSelect
			// 
			this.galaxyIconSelect.AutoSize = true;
			this.galaxyIconSelect.Checked = true;
			this.galaxyIconSelect.Location = new System.Drawing.Point(185, 19);
			this.galaxyIconSelect.Name = "galaxyIconSelect";
			this.galaxyIconSelect.Size = new System.Drawing.Size(14, 13);
			this.galaxyIconSelect.TabIndex = 19;
			this.galaxyIconSelect.TabStop = true;
			this.galaxyIconSelect.Tag = "icon";
			this.galaxyIconSelect.UseVisualStyleBackColor = true;
			// 
			// steamIconPicture
			// 
			this.steamIconPicture.Location = new System.Drawing.Point(331, 19);
			this.steamIconPicture.Name = "steamIconPicture";
			this.steamIconPicture.Size = new System.Drawing.Size(112, 112);
			this.steamIconPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.steamIconPicture.TabIndex = 18;
			this.steamIconPicture.TabStop = false;
			this.steamIconPicture.Click += new System.EventHandler(this.steamIconPicture_Click);
			// 
			// galaxyIconPicture
			// 
			this.galaxyIconPicture.Location = new System.Drawing.Point(6, 19);
			this.galaxyIconPicture.Name = "galaxyIconPicture";
			this.galaxyIconPicture.Size = new System.Drawing.Size(112, 112);
			this.galaxyIconPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.galaxyIconPicture.TabIndex = 17;
			this.galaxyIconPicture.TabStop = false;
			this.galaxyIconPicture.Click += new System.EventHandler(this.galaxyIconPicture_Click);
			// 
			// CoverEditForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(474, 531);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CoverEditForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CoverEditForm_FormClosing);
			this.Load += new System.EventHandler(this.CoverEditForm_Load);
			this.Shown += new System.EventHandler(this.CoverEditForm_Shown);
			this.StyleChanged += new System.EventHandler(this.CoverEditForm_StyleChanged);
			this.SystemColorsChanged += new System.EventHandler(this.CoverEditForm_SystemColorsChanged);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.steamCoverPicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyCoverPicture)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.steamBackgroundPicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyBackgroundPicture)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.steamIconPicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyIconPicture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton steamCoverSelect;
		private System.Windows.Forms.RadioButton galaxyCoverSelect;
		private System.Windows.Forms.PictureBox steamCoverPicture;
		private System.Windows.Forms.PictureBox galaxyCoverPicture;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton steamBackgroundSelect;
		private System.Windows.Forms.RadioButton galaxyBackgroundSelect;
		private System.Windows.Forms.PictureBox steamBackgroundPicture;
		private System.Windows.Forms.PictureBox galaxyBackgroundPicture;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.RadioButton steamIconSelect;
		private System.Windows.Forms.RadioButton galaxyIconSelect;
		private System.Windows.Forms.PictureBox steamIconPicture;
		private System.Windows.Forms.PictureBox galaxyIconPicture;
	}
}