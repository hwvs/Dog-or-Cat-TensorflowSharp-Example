using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;

namespace DogOrCatTFSharp
{
    class Detection
    {
        TFSession session;
        TFGraph graph;

        string[] labels;

        const int W = 128;
        const int H = 128;
        const float Mean = 128f;
        const float Scale = 128f; // --input_std, should be 128 for mobilenet, don't use 1 or it won't work

        public Detection()
        {
            //Load labels
            labels = File.ReadAllLines("tfdata/output_labels.txt");

            // Construct an in-memory graph from the serialized form.
            graph = new TFGraph();
            // Load the serialized GraphDef from a file.
            byte[] model = File.ReadAllBytes("tfdata/output_graph.pb");
            graph.Import(model, "");


            TFSessionOptions TFOptions = new TFSessionOptions();

            // This code helps with using the GPU version of tensorflowlib on Windows to avoid eating all of your RAM 
            unsafe
            {
                // These bytes represent the following settings:
                // config.gpu_options.allow_growth = True
                // config.gpu_options.per_process_gpu_memory_fraction = 0.3
                byte[] GPUConfig = new byte[] { 0x32, 0x0b, 0x09, 0x33, 0x33, 0x33, 0x33, 0x33, 0x33, 0xd3, 0x3f, 0x20, 0x01 };

                fixed (void* ptr = &GPUConfig[0])
                {
                    TFOptions.SetConfig(new IntPtr(ptr), GPUConfig.Length);
                }
            }

            // Set the session
            session = new TFSession(graph, TFOptions);
        }

        private byte[] ImageToByteArray(System.Drawing.Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png); // Encode as a png
                return ms.ToArray();
            }
        }

        private byte[] ImageToByteArray(Bitmap image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png); // Encode as a png
                return ms.ToArray();
            }
        }

        // Mini struct to return name and confidence :)
        public struct ClassificationResult
        {
            public string Name;
            public float Confidence;

            public ClassificationResult(string n, float conf)
            {
                Name = n;
                Confidence = conf;
            }
        }
        public ClassificationResult ClassifyImage(byte[] contents)
        {
            var tensor = TFTensor.CreateString(contents);
            tensor = CreateTensor(tensor, TFDataType.Float);

            var runner = session.GetRunner();

            runner.AddInput(graph["input"][0], tensor).Fetch(graph["final_result"][0]);
            var output = runner.Run();

            var result = output[0];
            bool jagged = true;
            var bestIdx = 0;
            float p = 0, best = 0;

            if (jagged)
            {
                var probabilities = ((float[][])result.GetValue(jagged: true))[0];
                for (int i = 0; i < probabilities.Length; i++)
                {
                    if (probabilities[i] > best)
                    {
                        bestIdx = i;
                        best = probabilities[i];
                    }
                }

            }
            else
            {
                var val = (float[,])result.GetValue(jagged: false);

                // Result is [1,N], flatten array
                for (int i = 0; i < val.GetLength(1); i++)
                {
                    if (val[0, i] > best)
                    {
                        bestIdx = i;
                        best = val[0, i];
                    }
                }
            }

            return new ClassificationResult(labels[bestIdx], best);
        }

        public ClassificationResult ClassifyImage(System.Drawing.Image toClassifyImage)
        {
            return ClassifyImage(ImageToByteArray(toClassifyImage));
        }

        public ClassificationResult ClassifyImage(Bitmap toClassifyImage)
        {
            return ClassifyImage(ImageToByteArray(toClassifyImage));
        }

        // Convert the image in filename to a Tensor suitable as input to the Inception model.
        public static TFTensor CreateTensorFromImageFile(string file, TFDataType destinationDataType = TFDataType.Float)
        {
            var contents = File.ReadAllBytes(file);
            var tensor = TFTensor.CreateString(contents);

            return CreateTensor(tensor, destinationDataType);
        }

        public static TFTensor CreateTensor(TFTensor tensor, TFDataType destinationDataType = TFDataType.Float)
        {
            TFGraph graph;
            TFOutput input, output;

            // Construct a graph to normalize the image
            ConstructGraphToNormalizeImage(out graph, out input, out output, destinationDataType);

            // Execute that graph to normalize this one image
            using (var session = new TFSession(graph))
            {
                var normalized = session.Run(
                         inputs: new[] { input },
                         inputValues: new[] { tensor },
                         outputs: new[] { output });

                return normalized[0];
            }
        }
        
        private static void ConstructGraphToNormalizeImage(out TFGraph graph, out TFOutput input, out TFOutput output, TFDataType destinationDataType = TFDataType.Float)
        {
            graph = new TFGraph();
            input = graph.Placeholder(TFDataType.String);

            output = graph.Cast(graph.Div(
                x: graph.Sub(
                    x: graph.ResizeBilinear(
                        images: graph.ExpandDims(
                            input: graph.Cast(
                                graph.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
                            dim: graph.Const(0, "make_batch")),
                        size: graph.Const(new int[] { W, H }, "size")),
                    y: graph.Const(Mean, "mean")),
                y: graph.Const(Scale, "scale")), destinationDataType);
        }
    }
}
