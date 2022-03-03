using System;

namespace _0_9MNIST
{
    class Neuron
    {
        private double[] weights;
        private double bias;
        private Neuron[] previousLayerNeurons;
        private double value;


        public Neuron(){}

        public double Value { get => value; set => this.value = value; }
        public double[] Weights { get => weights; set => weights = value; }
        public double Bias { get => bias; set => bias = value; }
        

        public void initilizeNeuron(Neuron[] prevLayers)
        {
            previousLayerNeurons = prevLayers;
            Random rand = new Random();
            double[] tempWeights = new double[previousLayerNeurons.Length];
            double tempBias = (.02 * rand.NextDouble() - .01);

            for (int i = 0; i < previousLayerNeurons.Length; i++)
            {
                //see if their is some baisc optimization we can do with the initial biases
                
                tempWeights[i] = (.02 * rand.NextDouble() - .01);
            }
            weights = tempWeights;
            bias = tempBias;

        }

        public double commputeValue()
        {
            double sum = 0;

            for(int i = 0; i < previousLayerNeurons.GetLength(0); i++)
            {
                sum += (weights[i] * previousLayerNeurons[i].value);
            }
            sum += bias;

            value = LogSigmoid(sum);
            return value;
        }

        //this is no longer a sigmoid funciton it is now a leaky ReLu, I was just too lazy to rename it
        public double LogSigmoid(double x)
        {
            if (x < 0)
            {
                return x / 100;
            }else if(x > 1)
            {
                return 1;
            }
            else return x;
        }
    }
}
