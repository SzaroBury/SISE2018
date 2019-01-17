using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad2
{
    class Program
    {
        public static int MaxEpochs = 1000;
        public static int neuronsInHiddenLayer = 8;
        public static float sigmoidSteepnessFactor = 1f;
        public static bool getInfoInConsole = true;
        public static List<KeyValuePair<int, double>> trainingData = new List<KeyValuePair<int, double>>();
        public static List<List<KeyValuePair<int, double>>> testingData = new List<List<KeyValuePair<int, double>>>();
        public static List<double> mins = new List<double>() { int.MaxValue, int.MaxValue, int.MaxValue } ;

        static void Main(string[] args)
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            GenerateTrainingData();
            LoadTrainingData();
            LoadTestingData();

            Neuron[] hiddenLayer = new Neuron[neuronsInHiddenLayer];
            Neuron[] outputLayer = new Neuron[1];
            for (int i = 0; i < hiddenLayer.Length; i++)
            {
                hiddenLayer[i] = new Neuron(1, 1);
                hiddenLayer[i].RandomValues();
            }
            outputLayer[0] = new Neuron(neuronsInHiddenLayer, 2);
            outputLayer[0].RandomValues();

            #region Training
            if (getInfoInConsole) { Console.WriteLine("Training started..."); Console.ReadKey(); }
            for (int i = 0; i < MaxEpochs; i++)
            {
                List<int> indexes = GetSequenceOfNumbers(trainingData.Count);
                if (getInfoInConsole) { Console.WriteLine("Epoch: " + i + "/" + MaxEpochs); }
                for (int j = 0; j < trainingData.Count; j++)
                {
                    int index = random.Next(0, indexes.Count);
                    indexes.RemoveAt(index);

                    double[] hiddenLayerInputs = new double[] { trainingData[index].Key };
                    double[] outputLayerInputs = new double[neuronsInHiddenLayer];

                    foreach (Neuron n in hiddenLayer)
                        n.Inputs = hiddenLayerInputs;

                    for (int k = 0; k < hiddenLayer.Length; k++)
                        outputLayerInputs[k] = hiddenLayer[k].Output();

                    outputLayer[0].Inputs = outputLayerInputs;

                    double diffrence = 0;
                    diffrence = trainingData[index].Value - outputLayer[0].Output();
                    
                    outputLayer[0].Error = diffrence;

                    for (int k = 0; k < hiddenLayer.Length; k++)
                    {
                        hiddenLayer[k].Error = SigmaDerivative(hiddenLayer[k].Output()) * outputLayer[0].Error * outputLayer[0].Weights[k];
                        hiddenLayer[k].UpdateWeights();
                    }
                    outputLayer[0].UpdateWeights();
                }

                int iterator = 0;
                foreach (var testData in testingData)
                {
                    double TestingMSE = 0;
                    for (int j = 0; j < testData.Count; j++)
                    {
                        double[] hiddenLayerInputs = new double[] { testData[j].Key };
                        double[] outputLayerInputs = new double[neuronsInHiddenLayer];

                        foreach (Neuron n in hiddenLayer)
                            n.Inputs = hiddenLayerInputs;

                        for (int k = 0; k < hiddenLayer.Length; k++)
                            outputLayerInputs[k] = hiddenLayer[k].Output();

                        outputLayer[0].Inputs = outputLayerInputs;

                        TestingMSE += Math.Pow(testData[j].Value - outputLayer[0].Output(), 2);
                        if (getInfoInConsole && i == 1000)
                        {
                            Console.WriteLine("x: " + testData[j].Key + Environment.NewLine +
                                              "square root of x: " + testData[j].Value.ToString() + Environment.NewLine +
                                              "From network: " + outputLayer[0].Output() + Environment.NewLine +
                                              "MSE: " + TestingMSE);
                            Console.ReadKey();
                        }
                    }
                    if (getInfoInConsole)
                    {
                        Console.WriteLine("TestingMSE: " + TestingMSE + Environment.NewLine +
                                          "testData.Count: " + testData.Count + Environment.NewLine +
                                          "mins: " + mins[iterator] + Environment.NewLine);
                        //Console.ReadKey();
                    }
                    if (TestingMSE / testData.Count < mins[iterator])
                        mins[iterator] = TestingMSE / testData.Count;

                    if (i < 1000)
                    {
                        stringBuilder.Append((TestingMSE / testData.Count).ToString());
                        stringBuilder.Append("\t");
                    }
                    iterator++;
                }
                stringBuilder.Append("\n");


            }

            string errorsFName = hiddenLayer.Length + "n_" + Neuron.LearningFactor + "lf_" + Neuron.MomentumFactor + "mf_errors.txt";
            string minFName = hiddenLayer.Length + "n_" + Neuron.LearningFactor + "lf_" + Neuron.MomentumFactor + "mf_mins.txt";

            using (StreamWriter streamWriter = new StreamWriter(errorsFName))
                streamWriter.Write(stringBuilder.ToString());

            using (StreamWriter streamWriter = new StreamWriter(minFName))
                for(int i = 0; i < mins.Count; i++)
                    streamWriter.Write(mins[i] + "\t");      
            #endregion
        }

        public static double SigmaDerivative(double x)
        {
            return sigmoidSteepnessFactor * x * (1 - x);
        }

        public static List<int> GetSequenceOfNumbers(int range)
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < range; i++)
                numbers.Add(i);

            return numbers;
        }

        #region DataManagement
        public static void GenerateTrainingData()
        {
            using (StreamWriter streamWriter = new StreamWriter("../../trainingSet.txt"))
            {
                Random random = new Random();
                SortedSet<int> numbers = new SortedSet<int>();
                do numbers.Add(random.Next(1, 100));
                while (numbers.Count != 25);

                foreach (int value in numbers)
                    streamWriter.WriteLine(value.ToString() + " " + Math.Sqrt(value).ToString());
            }
            if (getInfoInConsole) { Console.WriteLine("Finished generating."); Console.ReadKey(); }
        }

        public static void LoadTrainingData()
        {
            StreamReader streamReader = new StreamReader("../../trainingSet.txt");
            string[] fileStrings = streamReader.ReadToEnd().Replace('\n', ' ').Split(' ');
            int value = 0;
            double sqrt = 0;

            for (int i = 0; i < fileStrings.Length - 1; i++)
            {
                if (i % 2 == 1)
                {
                    sqrt = Convert.ToDouble(fileStrings[i]);
                    trainingData.Add(new KeyValuePair<int, double>(value, sqrt));
                }
                else
                    value = Convert.ToInt32(fileStrings[i]);
            }
            if (getInfoInConsole) { Console.WriteLine("Training data loaded."); Console.ReadKey(); }
        }


        public static void LoadTestingData()
        {
            string[] fileNames = Directory.GetFiles("../..").Where(p => p.Contains("testData")).ToArray();
            if (fileNames.Length > 0)
            {
                foreach (string fileName in fileNames)
                {
                    testingData.Add(new List<KeyValuePair<int, double>>());
                    int currentIndex = testingData.Count - 1;

                    StreamReader streamReader = new StreamReader(fileName);
                    string[] fileStrings = streamReader.ReadToEnd().Replace('\n', ' ').Split(' ');
                    int value = 0;
                    double sqrt = 0;

                    for (int i = 0; i < fileStrings.Length - 1; i++)
                    {
                        if (i % 2 == 1)
                        {
                            sqrt = Convert.ToDouble(fileStrings[i]);
                            testingData[currentIndex].Add(new KeyValuePair<int, double>(value, sqrt));
                        }
                        else
                            value = Convert.ToInt32(fileStrings[i]);
                    }
                }
                if (getInfoInConsole) { Console.WriteLine("Testing data loaded."); Console.ReadKey(); }
            }
            else if (getInfoInConsole) { Console.WriteLine("Test data file not founded."); Console.ReadKey(); }
        }
        #endregion
    }
}
