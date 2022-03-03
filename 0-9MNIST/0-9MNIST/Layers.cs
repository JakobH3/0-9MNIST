using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace _0_9MNIST
{
    class Layers
    {
        private Neuron[] inputNeurons, outputNeurons;
        private Neuron[,] hiddenNeurons;
        

        public Layers(){}

        public Neuron[] getInputNeurons() { return inputNeurons; }

        public Neuron[,] getHiddenNeurons() { return hiddenNeurons; }

        public Neuron[] getOutputNeurons() { return outputNeurons; }

        public void oneStep(double[] inputs, double[] expectedOutputs)
        {
            double learningRate = 0.02; 
            double[] outputs = computeNetwork(inputs);
            double[] prevLayerInputVotes;
            double[] currLayerInputVotes;
            
            currLayerInputVotes = new double[outputNeurons.Length];
            //doing back prob on the first layer
            for(int i = 0; i < outputNeurons.Length; i++)
            {
                double slope = 1.0;
                //checking to see if the output is less than 0 and if it is chaning the slope
                if (outputNeurons[i].Value < 0)
                    slope = .01;
                //computing the input vote
                //                      output vote
                double outputVote = (expectedOutputs[i] - outputs[i]);
                //Console.WriteLine(outputVote);
                double inputVote = outputVote * slope;
                double adjustment = inputVote * learningRate;
                outputNeurons[i].Bias += adjustment;
                
                for (int j = 0; j < outputNeurons[i].Weights.Length; j++)
                {
                    outputNeurons[i].Weights[j] += adjustment * hiddenNeurons[j, hiddenNeurons.GetLength(1) - 1].Value;                   
                }
                currLayerInputVotes[i] = inputVote;
            }

            prevLayerInputVotes = currLayerInputVotes;
            for (int i = 0; i < hiddenNeurons.GetLength(0); i++)
            {
                double slope = 1.0;
                //checking to see if the output is less than 0 and if it is chaning the slope

           
                //computing the output vote for the the layer
                double outputVote = 0;
                for (int h = 0; h < prevLayerInputVotes.Length; h++)
                {
                        outputVote += prevLayerInputVotes[h] * outputNeurons[i].Weights[h];
                }
                

                double inputVote = outputVote * slope;
                double adjustment = inputVote * learningRate;
                hiddenNeurons[i, hiddenNeurons.GetLength(1) - 1].Bias += adjustment;

                    for (int h = 0; h < hiddenNeurons[i, hiddenNeurons.GetLength(1) - 1].Weights.Length; h++)
                    {
                        hiddenNeurons[i, hiddenNeurons.GetLength(1) - 1].Weights[h] += adjustment * inputNeurons[h].Value;
                    }
               

                currLayerInputVotes[i] = inputVote;
            }


            //layer 
            for (int k = hiddenNeurons.GetLength(1) - 2; k >= 0; k--)
            {
                prevLayerInputVotes = currLayerInputVotes;
                currLayerInputVotes = new double[hiddenNeurons.GetLength(0)];
                //neuron

                // do the first layer on its own dont do that weird if statement shit

                for(int i = 0; i < hiddenNeurons.GetLength(0); i++)
                {
                    double slope = 1.0;
                    //checking to see if the output is less than 0 and if it is chaning the slope
                    if(k == hiddenNeurons.GetLength(1))
                    {
                        if (outputNeurons[i].Value < 0)
                            slope = .01;
                    }
                    else
                    {
                        if (hiddenNeurons[i, k].Value < 0)
                            slope = .01;
                    }
                    
                    //computing the output vote for the the layer
                    double outputVote = 0;
                    if(k == hiddenNeurons.GetLength(1))
                    {
                        for (int h = 0; h < prevLayerInputVotes.Length; h++)
                        {
                            outputVote += prevLayerInputVotes[h] * outputNeurons[i].Weights[h];
                        }
                    }
                    else
                    {
                        for (int h = 0; h < prevLayerInputVotes.Length; h++)
                        {
                            outputVote += prevLayerInputVotes[h] * hiddenNeurons[i,k].Weights[h];
                        }
                    }
                    
                    double inputVote = outputVote * slope;
                    double adjustment = inputVote * learningRate;
                    hiddenNeurons[i, k].Bias += adjustment;

                    if(k == 0)
                    {
                        for (int h = 0; h < hiddenNeurons[i, k].Weights.Length; h++)
                        {
                            hiddenNeurons[i, k].Weights[h] += adjustment * inputNeurons[h].Value;
                        }
                    }
                    else
                    {
                        for (int h = 0; h < hiddenNeurons[i, k].Weights.Length; h++)
                        {
                            hiddenNeurons[i, k].Weights[h] += adjustment * hiddenNeurons[h, 1].Value;
                        }
                    }
                    
                    currLayerInputVotes[i] = inputVote;
                }

            }
        }
        
        public void initializeNetwork(int numOfInput, int numOfHiddenLayers, int numOfNeuronsInLayer, int numOfOutput)
        {
     
            // initializes input neurons
            Neuron[] inputNeuronstemp = new Neuron[numOfInput];
            for (int i = 0; i < numOfInput; i++)
            {
                Neuron temp = new Neuron();
                temp.Value = 0.0;
                inputNeuronstemp[i] = temp;
            }
            inputNeurons = inputNeuronstemp;


            // initializes hidden layer neurons
            //                           the column is the layer and the row is the specific neuron
            //                                      Rows                    columns
            //                                      y Coordinate        x Coordinate
            Neuron[,] hiddenLayers = new Neuron[numOfNeuronsInLayer, numOfHiddenLayers];

            for (int y = 0; y < numOfNeuronsInLayer; y++)
            {
                
                Neuron temp1 = new Neuron();
                temp1.initilizeNeuron(inputNeurons);
                hiddenLayers[y, 0] = temp1;

            }
            for (int x = 1; x < numOfHiddenLayers; x++)
            {
                for (int y = 0; y < numOfNeuronsInLayer; y++)
                {
                    Neuron temp = new Neuron();
                    Neuron[] prevlayer = getHiddenLayer(hiddenLayers, x - 1);
                    temp.initilizeNeuron(prevlayer);
                    hiddenLayers[y, x] = temp;
                }
                
            }
            hiddenNeurons = hiddenLayers;

            // initializes output neurons
            Neuron[] outputNeuronstemp = new Neuron[numOfOutput];
            for (int k = 0; k < numOfOutput; k++)
            {
                Neuron temp = new Neuron();
                temp.initilizeNeuron(getHiddenLayer(hiddenLayers, numOfHiddenLayers - 1));
                outputNeuronstemp[k] = temp;
            }
            outputNeurons = outputNeuronstemp;

        }

        public double[] computeNetwork(double[] inputs)
        {
            double[] output = new double[outputNeurons.GetLength(0)];
            for (int j = 0; j < inputNeurons.GetLength(0); j++)
            {
                inputNeurons[j].Value = inputs[j];
            }
            for (int j = 0; j < hiddenNeurons.GetLength(1); j++)
            {
                for (int k = 0; k < hiddenNeurons.GetLength(0); k++)
                {
                    hiddenNeurons[k, j].commputeValue();
                }
            }
            for (int j = 0; j < outputNeurons.GetLength(0); j++)
            {
                output[j] = outputNeurons[j].commputeValue();
            }

            return output;
        }

        //computes the cost of a network given a set of inputs and expected outputs
        public double computeCost(double[] inputs, double[] expectedOutputs)
        {
            double[] outputs = computeNetwork(inputs);
            double cost = 0;

            for(int i = 0; i < outputs.Length; i++)
            {
                cost += Math.Pow((outputs[i] - expectedOutputs[i]), 2);
            }

            return cost;
        }

        public int getGradientLength()
        {
            //this might not work i havent acctually tested it
            int totalLength = hiddenNeurons.GetLength(0) * 2 + hiddenNeurons.GetLength(0) * hiddenNeurons[0, 0].Weights.Length * 2 + outputNeurons.GetLength(0) + outputNeurons.GetLength(0) * outputNeurons[0].Weights.Length;
            return totalLength;
        }


        public Neuron[] getNeuronList()
        {
            int totalLength = inputNeurons.GetLength(0) + outputNeurons.GetLength(0) + hiddenNeurons.GetLength(0) * hiddenNeurons.GetLength(1);

            Neuron[] neuronList = new Neuron[totalLength];
            int i = 0;
           
            for (int j = 0; j < inputNeurons.GetLength(0); j++)
            {
                neuronList[i] = inputNeurons[j];
                i++;
            }
            for(int j = 0; j < hiddenNeurons.GetLength(1); j++)
            {
                for(int k = 0; k < hiddenNeurons.GetLength(0); k++)
                {
                    neuronList[i] = hiddenNeurons[k, j];
                    i++;
                }
            }
            for(int j = 0; j < outputNeurons.GetLength(0); j++)
            {
                neuronList[i] = outputNeurons[j];
            }

            

            return neuronList;
        }


        //                   the column is the layer and the row is the specific neuron
        //                              Rows             columns
        //                             y Coordinate   x Coordinate
        //          Start counting from 0!!!!!
        public Neuron[] getHiddenLayer(Neuron[,] input, int layerNumber)
        {
            // create an array with the same lenght as the layer
            Neuron[] output = new Neuron[input.GetLength(0)];

            for(int i = 0; i < output.GetLength(0);i++)
            {
                output[i] = input[i, layerNumber];
            }

            return output;
        }



    }
}
