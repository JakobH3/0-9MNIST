using System;
using System.IO;
using System.Linq;

namespace _0_9MNIST
{
    class Program
    {
        /*
        static void Main(string[] args)
        {


            //Parsing the Mnist data set file
            int trainingSetLength = 30000;

            DigitImage[] trainingSet = new DigitImage[trainingSetLength];
            try
            {
                Console.WriteLine("\nBegin\n");
                FileStream ifsLabels =
                 new FileStream(@"C:\Users\jhaeh\source\repos\0-9MNIST\train-labels.idx1-ubyte",
                 FileMode.Open); // test labels
                FileStream ifsImages =
                 new FileStream(@"C:\Users\jhaeh\source\repos\0-9MNIST\train-images.idx3-ubyte",
                 FileMode.Open); // test images

                BinaryReader brLabels =
                 new BinaryReader(ifsLabels);
                BinaryReader brImages =
                 new BinaryReader(ifsImages);

                int magic1 = brImages.ReadInt32(); // discard
                int numImages = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();

                int magic2 = brLabels.ReadInt32();
                int numLabels = brLabels.ReadInt32();

                byte[][] pixels = new byte[28][];
                for (int i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[28];

                // each test image
                for (int di = 0; di < trainingSetLength; ++di)
                {
                    for (int i = 0; i < 28; ++i)
                    {
                        for (int j = 0; j < 28; ++j)
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }
                    }

                    byte lbl = brLabels.ReadByte();

                    DigitImage dImage = new DigitImage(pixels, lbl);
                    trainingSet[di] = dImage;


                } // each image

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabels.Close();

                Console.WriteLine("\nEnd\n");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("shits fucked");
                Console.ReadLine();
            }

            Console.WriteLine("Training network");

            //Making the Network
            Layers testNetwork = new Layers();
            int numOfInputNeurons = 28 * 28;
            int numOfHiddenLayers = 2;
            int numOfNeuronsInHiddenLayer = 16;
            int numOfOutputNeurons = 10;
            testNetwork.initializeNetwork(numOfInputNeurons, numOfHiddenLayers, numOfNeuronsInHiddenLayer, numOfOutputNeurons);

            //teaching the network

            for(int i = 0; i < trainingSet.Length; i++)
            {
                testNetwork.oneStep(inputs(trainingSet[i].pixels), expectedOutputs(trainingSet[i].label));
            }
            Console.WriteLine("network trained");

            Console.ReadLine();

            //test network
            double score = 0;

            for(int i = 0; i < 1000; i++)
            {
                double[] output = testNetwork.computeNetwork(inputs(trainingSet[i].pixels));
                double maxValue = output.Max();
                int index = output.ToList().IndexOf(maxValue);
                if(index == trainingSet[i].label)
                {
                    score += 1.0;
                }

            }

            Console.WriteLine("final score: " + score / 1000.0);

            






        }
        */
        static byte[] To1DArray(byte[][] input)
        {
            // Step 1: get total size of 2D array, and allocate 1D array.
            int size = input.Length * input[1].Length;
            byte[] result = new byte[size];

            // Step 2: copy 2D array elements into a 1D array.
            int write = 0;
            while(write < size)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    for (int z = 0; z < input[i].Length; z++)
                    {
                        result[write++] = input[z][i];
                    }
                }
            }
            
            // Step 3: return the new array.
            return result;
        }
        static double[] expectedOutputs(byte Lable)
        {
            int lab = (int)Lable;
            double[] output = new double[10];
            switch (lab)
            {
                case 0:
                   output =  new double[] {1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0};
                   break;
                case 1:
                    output = new double[] { 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
                    break;
                case 2:
                    output = new double[] { 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
                    break;
                case 3:
                    output = new double[] { 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
                    break;
                case 4:
                    output = new double[] { 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
                    break;
                case 5:
                    output = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0 };
                    break;
                case 6:
                    output = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0 };
                    break;
                case 7:
                    output = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0 };
                    break;
                case 8:
                    output = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0 };
                    break;
                case 9:
                    output = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0 };
                    break;
            }
            return output;
        }
        static double[] inputs(byte[][] pixels)
        {
            byte[] tempInputs = To1DArray(pixels);
            double[] output = new double[tempInputs.Length];
            for(int i = 0; i < output.Length; i++)
            {
                output[i] = ((double)tempInputs[i]) / 255.0;
            }
            return output;

        }
    }    
}
