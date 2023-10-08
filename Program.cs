using System;

namespace Laba2_Markaryan
{

    internal class Program
    {
        static double FunctionValue(double x1, double x2)
        {
            return 2 * x1 * x1 + x1 * x2 + x2 * x2;
        }

        static double[] FunctionGradient(double x1, double x2)
        {
            return new double[] { 4 * x1 + x2, x1 + 2 * x2 };
        }

        static double[,] FunctionHessian()
        {
            return new double[,] { { 4, 1 }, { 1, 2 } };
        }

        static double Norm(double[] vec)
        {
            return Math.Sqrt(vec[0] * vec[0] + vec[1] * vec[1]);
        }

        static double[] GradientDescent(double[] x0, double step, double epsilon)
        {
            double[] x = (double[])x0.Clone();
            while (Norm(FunctionGradient(x[0], x[1])) > epsilon)
            {
                var grad = FunctionGradient(x[0], x[1]);
                x[0] -= step * grad[0];
                x[1] -= step * grad[1];
            }
            return x;
        }

        static double[] FastestGradientDescent(double[] x0, double epsilon)
        {
            double[] x = (double[])x0.Clone();
            while (Norm(FunctionGradient(x[0], x[1])) > epsilon)
            {
                var grad = FunctionGradient(x[0], x[1]);
                double alpha = 1.0; // TODO: Implement line search for optimal alpha
                x[0] -= alpha * grad[0];
                x[1] -= alpha * grad[1];
            }
            return x;
        }

        static double[] NewtonMethod(double[] x0, double epsilon)
        {
            double[] x = (double[])x0.Clone();
            var hessian = FunctionHessian();
            while (Norm(FunctionGradient(x[0], x[1])) > epsilon)
            {
                var grad = FunctionGradient(x[0], x[1]);

                // Compute inverse Hessian for 2x2 matrix
                double det = hessian[0, 0] * hessian[1, 1] - hessian[0, 1] * hessian[1, 0];
                double[,] invHessian = {
                    { hessian[1, 1] / det, -hessian[0, 1] / det },
                    { -hessian[1, 0] / det, hessian[0, 0] / det }
                };

                x[0] -= (invHessian[0, 0] * grad[0] + invHessian[0, 1] * grad[1]);
                x[1] -= (invHessian[1, 0] * grad[0] + invHessian[1, 1] * grad[1]);
            }
            return x;
        }

        static void Main()
        {
            double[] x0 = { 0.5, 1 };
            double step = 0.5;
            double epsilon = 0.01;

            Console.WriteLine("Градиентный спуск с постоянным шагом:");
            var res1 = GradientDescent(x0, step, epsilon);
            Console.WriteLine($"x1: {res1[0]}, x2: {res1[1]}");

            Console.WriteLine("\nНаискорейший градиентный спуск:");
            var res2 = FastestGradientDescent(x0, epsilon);
            Console.WriteLine($"x1: {res2[0]}, x2: {res2[1]}");

            Console.WriteLine("\nМетод Ньютона:");
            var res3 = NewtonMethod(x0, epsilon);
            Console.WriteLine($"x1: {res3[0]}, x2: {res3[1]}");
            Console.ReadKey();
        }
    }
}
