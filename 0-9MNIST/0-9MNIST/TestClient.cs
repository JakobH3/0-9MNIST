using System;
using System.Collections.Generic;
using System.Text;

namespace _0_9MNIST
{
    class TestClient
    {

        static void Main(string[] args)
        {

            double[] input1 = { 0, 0, 1 };
            double[] input2 = { 0, 1, 1 };
            double[] input3 = { 1, 0, 1 };
            double[] input4 = { 1, 1, 1 };

            double[] expectedOut1 = { 0 };
            double[] expectedOut2 = { 1 };
            double[] expectedOut3 = { 1 };
            double[] expectedOut4 = { 0 };

            double[] testOutput1;
            double[] testOutput2;
            double[] testOutput3;
            double[] testOutput4;


            Layers test = new Layers();

            test.initializeNetwork(3, 1, 2, 1);

            testOutput1 = test.computeNetwork(input1);
            testOutput2 = test.computeNetwork(input2);
            testOutput3 = test.computeNetwork(input3);
            testOutput4 = test.computeNetwork(input4);

            Console.WriteLine("Expected out: 0 Actual out: " + testOutput1[0]);
            Console.WriteLine("Expected out: 1 Actual out: " + testOutput2[0]);
            Console.WriteLine("Expected out: 1 Actual out: " + testOutput3[0]);
            Console.WriteLine("Expected out: 0 Actual out: " + testOutput4[0]);
            Console.WriteLine();

            for (int i = 0; i < 100; i++)
            {

                test.oneStep(input1, expectedOut1);
                test.oneStep(input2, expectedOut2);
                test.oneStep(input3, expectedOut3);
                test.oneStep(input4, expectedOut4);


                testOutput1 = test.computeNetwork(input1);
                testOutput2 = test.computeNetwork(input2);
                testOutput3 = test.computeNetwork(input3);
                testOutput4 = test.computeNetwork(input4);

                Console.WriteLine("iteration: " + i);
                Console.WriteLine("Expected out: 0 Actual out: " + testOutput1[0]);
                Console.WriteLine("Expected out: 1 Actual out: " + testOutput2[0]);
                Console.WriteLine("Expected out: 1 Actual out: " + testOutput3[0]);
                Console.WriteLine("Expected out: 0 Actual out: " + testOutput4[0]);
                Console.WriteLine();

            }

            testOutput1 = test.computeNetwork(input1);
            testOutput2 = test.computeNetwork(input2);
            testOutput3 = test.computeNetwork(input3);
            testOutput4 = test.computeNetwork(input4);

            Console.WriteLine("Expected out: 0 Actual out: " + testOutput1[0]);
            Console.WriteLine("Expected out: 1 Actual out: " + testOutput2[0]);
            Console.WriteLine("Expected out: 1 Actual out: " + testOutput3[0]);
            Console.WriteLine("Expected out: 0 Actual out: " + testOutput4[0]);



        }




    }
}
