using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad2
{
    public class Neuron
    {
        public static float LearningFactor = 0.001f;
        public static float MomentumFactor = 0.001f;
        public static bool UseBias = true;

        #region Definitions
        public double[] Inputs { get; set; }
        public double[] Weights { get; set; }
        public double[] PreviousChanges { get; set; }
        public double PreviousBiasChange { get; set; }
        public double Bias { get; set; }
        public static Random random = new Random();
        public double Error;
        public int ActivationFunction;
        #endregion

        public Neuron(int inputCount, int newActivationFuntion)
        {
            Weights = new double[inputCount];
            PreviousChanges = new double[inputCount];
            ActivationFunction = newActivationFuntion;
        }

        public double Output()
        {
            double result = 0;
            for (int i = 0; i < Weights.Length; i++)
                result += Weights[i] * Inputs[i];

            if (UseBias) result += Bias;
            if (ActivationFunction == 1) result = Sigma(result);    //Sigma
            if (ActivationFunction == 2) result = result * 1.0;     //Linear Function

            return result;
        }

        public static double Sigma(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x * Program.sigmoidSteepnessFactor));
        }

        public void RandomValues()
        {
            Bias = (random.NextDouble() - 0.5) * 2;
            for (int i = 0; i < Weights.Length; i++)
            {
                PreviousChanges[i] = 0;
                PreviousBiasChange = 0;
                Weights[i] = (random.NextDouble() - 0.5) * 2;
            }
        }

        public void UpdateWeights()
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] += Error * Inputs[i] * LearningFactor + MomentumFactor * PreviousChanges[i];
                PreviousChanges[i] = Error * Inputs[i] * LearningFactor + MomentumFactor * PreviousChanges[i];
            }

            if (UseBias)
            {
                Bias += Error * LearningFactor + MomentumFactor * PreviousBiasChange;
                PreviousBiasChange = Error * LearningFactor + MomentumFactor * PreviousBiasChange;
            }
        }
    }
}
