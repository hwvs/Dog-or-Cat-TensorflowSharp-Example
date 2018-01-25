using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DogOrCatTFSharp
{
    public partial class FormClassification : Form
    {
        Detection ImageDetectionClassifier = new Detection();

        public FormClassification()
        {
            InitializeComponent();
        }

        public void DoClassification(Image theImage)
        {
            try
            {
                Detection.ClassificationResult result = ImageDetectionClassifier.ClassifyImage(theImage);
                labelResult.Text = $"{result.Name} ({(int)(result.Confidence * 100)}%)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception when classifying image: {ex.Message} at {ex.Source}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoClassification(pictureBoxImageToClassify.Image);
        }

        private void buttonClassify_Click(object sender, EventArgs e)
        {
            DoClassification(pictureBoxImageToClassify.Image);
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                Image imageFromClip = Clipboard.GetImage();
                pictureBoxImageToClassify.Image = imageFromClip;
                DoClassification(imageFromClip);
            }
        }
        
    }
}
