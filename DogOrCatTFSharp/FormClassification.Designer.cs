namespace DogOrCatTFSharp
{
    partial class FormClassification
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
            this.buttonClassify = new System.Windows.Forms.Button();
            this.pictureBoxImageToClassify = new System.Windows.Forms.PictureBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonPaste = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageToClassify)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClassify
            // 
            this.buttonClassify.Location = new System.Drawing.Point(202, 368);
            this.buttonClassify.Name = "buttonClassify";
            this.buttonClassify.Size = new System.Drawing.Size(110, 33);
            this.buttonClassify.TabIndex = 0;
            this.buttonClassify.Text = "Classify";
            this.buttonClassify.UseVisualStyleBackColor = true;
            this.buttonClassify.Click += new System.EventHandler(this.buttonClassify_Click);
            // 
            // pictureBoxImageToClassify
            // 
            this.pictureBoxImageToClassify.BackColor = System.Drawing.Color.White;
            this.pictureBoxImageToClassify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxImageToClassify.Image = global::DogOrCatTFSharp.Properties.Resources.doggo;
            this.pictureBoxImageToClassify.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxImageToClassify.Name = "pictureBoxImageToClassify";
            this.pictureBoxImageToClassify.Size = new System.Drawing.Size(497, 341);
            this.pictureBoxImageToClassify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxImageToClassify.TabIndex = 1;
            this.pictureBoxImageToClassify.TabStop = false;
            // 
            // labelResult
            // 
            this.labelResult.BackColor = System.Drawing.Color.Black;
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.ForeColor = System.Drawing.Color.White;
            this.labelResult.Location = new System.Drawing.Point(13, 13);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(122, 23);
            this.labelResult.TabIndex = 2;
            // 
            // buttonPaste
            // 
            this.buttonPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPaste.Location = new System.Drawing.Point(404, 352);
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.Size = new System.Drawing.Size(105, 23);
            this.buttonPaste.TabIndex = 3;
            this.buttonPaste.Text = "Paste Image";
            this.buttonPaste.UseVisualStyleBackColor = true;
            this.buttonPaste.Click += new System.EventHandler(this.buttonPaste_Click);
            // 
            // FormClassification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 422);
            this.Controls.Add(this.buttonPaste);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.pictureBoxImageToClassify);
            this.Controls.Add(this.buttonClassify);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormClassification";
            this.Text = "Dog or cat?";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageToClassify)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClassify;
        private System.Windows.Forms.PictureBox pictureBoxImageToClassify;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Button buttonPaste;
    }
}

