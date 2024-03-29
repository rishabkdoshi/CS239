﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using System;
using System.Linq;
using static System.Math;

namespace Microsoft.Quantum.Samples.SimpleAlgorithms
{
    class Driver
    {
        public static void Pause()
        {
            System.Console.WriteLine("\n\nPress any key to continue...\n\n");
            System.Console.ReadKey();
        }

        static void Main(string[] args)
        {
            #region Setup

            // We begin by defining a quantum simulator to be our target
            // machine.
            var sim = new QuantumSimulator(throwOnReleasingQubitsNotInZeroState: true);

            #endregion

            #region Parity Sampling with the Bernstein–Vazirani Algorithm
            // Consider a function 𝑓(𝑥⃗) on bitstrings 𝑥⃗ = (𝑥₀, …, 𝑥ₙ₋₁) of the
            // form
            //
            //     𝑓(𝑥⃗) ≔ Σᵢ 𝑥ᵢ 𝑟ᵢ
            //
            // where 𝑟⃗ = (𝑟₀, …, 𝑟ₙ₋₁) is an unknown bitstring that determines
            // the parity of 𝑓.

            // The Bernstein–Vazirani algorithm allows determining 𝑟 given a
            // quantum operation that implements
            //
            //     |𝑥〉|𝑦〉 ↦ |𝑥〉|𝑦 ⊕ 𝑓(𝑥)〉.
            //
            // In SimpleAlgorithms.qs, we implement this algorithm as the
            // operation BernsteinVaziraniTestCase. This operation takes an
            // integer whose bits describe 𝑟, then uses those bits to
            // construct an appropriate operation, and finally measures 𝑟.

            // We call that operation here, ensuring that we always get the
            // same value for 𝑟 that we provided as input.


/***************************UNCOMMENT FOR CHECKING DIFFERENT UF************************************/
            // const int nQubits = 4;

            // System.Diagnostics.Stopwatch sw;
            // sw = System.Diagnostics.Stopwatch.StartNew();
            // Console.WriteLine(sw.ElapsedMilliseconds);


            // foreach (var parity in Enumerable.Range(0, 1 << nQubits))
            // {
            //     sw = System.Diagnostics.Stopwatch.StartNew();

            //     var measuredParity = BernsteinVaziraniTestCase.Run(sim, nQubits, parity).Result;
            //     if (measuredParity != parity)
            //     {
            //         throw new Exception($"Measured parity {measuredParity}, but expected {parity}.");
            //     }
            //     Console.WriteLine(sw.ElapsedMilliseconds);
            // }
            // sw.Stop();
            // System.Console.WriteLine("All parities measured successfully!");
            // Pause();
/**************************************************************************************************/



/***************************UNCOMMENT FOR CHECKING DIFFERENT NQUBIT************************************/
            

            System.Diagnostics.Stopwatch sw;
            sw = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(sw.ElapsedMilliseconds);

            foreach (var nQubits in Enumerable.Range(1, 25))
            {
            	var parity = 1;
                sw = System.Diagnostics.Stopwatch.StartNew();
                System.Console.WriteLine("nQubits="+nQubits);
                var measuredParity = BernsteinVaziraniTestCase.Run(sim, nQubits, parity).Result;
                
                if (measuredParity != parity)
                {
                    throw new Exception($"Measured parity {measuredParity}, but expected {parity}.");
                }
                Console.WriteLine(sw.ElapsedMilliseconds);
            }
            sw.Stop();
            System.Console.WriteLine("All parities measured successfully!");
            Pause();
            
/**************************************************************************************************/
			#endregion
            #region Constant versus Balanced Functions with the Deutsch–Jozsa Algorithm
            // A Boolean function is a function that maps bitstrings to a
            // bit,
            //
            //     𝑓 : {0, 1}^n → {0, 1}.
            //
            // We say that 𝑓 is constant if 𝑓(𝑥⃗) = 𝑓(𝑦⃗) for all bitstrings
            // 𝑥⃗ and 𝑦⃗, and that 𝑓 is balanced if 𝑓 evaluates to true (1) for
            // exactly half of its inputs.

            // If we are given a function 𝑓 as a quantum operation 𝑈 |𝑥〉|𝑦〉
            // = |𝑥〉|𝑦 ⊕ 𝑓(𝑥)〉, and are promised that 𝑓 is either constant or
            // is balanced, then the Deutsch–Jozsa algorithm decides between
            // these cases with a single application of 𝑈.

            // In SimpleAlgorithms.qs, we implement this algorithm as
            // DeutschJozsaTestCase, following the pattern above.
            // This time, however, we will pass an array to Q# indicating
            // which elements of 𝑓 are marked; that is, should result in true.
            // We check by ensuring that DeutschJozsaTestCase returns true
            // for constant functions and false for balanced functions.

            sw = System.Diagnostics.Stopwatch.StartNew();
            System.Console.WriteLine(sw.ElapsedMilliseconds);

            sw = System.Diagnostics.Stopwatch.StartNew();
            System.Console.WriteLine("Balanced - 2");
            var balancedTestCase = new long[] { 1, 2 };
            if (DeutschJozsaTestCase.Run(sim, 2, new QArray<long>(balancedTestCase)).Result)
            {
                throw new Exception("Measured that test case {1, 2} was constant!");
            }

            System.Console.WriteLine(sw.ElapsedMilliseconds);

            sw = System.Diagnostics.Stopwatch.StartNew();
            System.Console.WriteLine("Balanced - 3 Type 1");
            balancedTestCase = new long[] { 1, 2, 5,6 };
            if (DeutschJozsaTestCase.Run(sim, 3, new QArray<long>(balancedTestCase)).Result)
            {
                throw new Exception("Measured that test case { 1, 2, 5,6 } was constant!");
            }

            System.Console.WriteLine(sw.ElapsedMilliseconds);


            sw = System.Diagnostics.Stopwatch.StartNew();
            System.Console.WriteLine("Balanced - 3 Type 2");
            balancedTestCase = new long[] { 2, 3, 4 , 5 };
            if (DeutschJozsaTestCase.Run(sim, 3, new QArray<long>(balancedTestCase)).Result)
            {
                throw new Exception("Measured that test case { 2, 3, 4 , 5 } was constant!");
            }

            System.Console.WriteLine(sw.ElapsedMilliseconds);



            sw = System.Diagnostics.Stopwatch.StartNew();
            System.Console.WriteLine("Balanced - 3 Type 3");
            balancedTestCase = new long[] { 1, 3 , 5,6 };
            if (DeutschJozsaTestCase.Run(sim, 3, new QArray<long>(balancedTestCase)).Result)
            {
                throw new Exception("Measured that test case { 1, 3 , 5,6 } was constant!");
            }

            System.Console.WriteLine(sw.ElapsedMilliseconds);

            System.Console.WriteLine("Trying for Balanced n");



            sw = System.Diagnostics.Stopwatch.StartNew();
            System.Console.WriteLine("Balanced - 3 Type 4");
            balancedTestCase = new long[] { 1, 2, 3 , 4 };
            if (DeutschJozsaTestCase.Run(sim, 3, new QArray<long>(balancedTestCase)).Result)
            {
                throw new Exception("Measured that test case { 1, 2, 3 , 4 } was constant!");
            }

            System.Console.WriteLine(sw.ElapsedMilliseconds);


            foreach (var numqubit in Enumerable.Range(4,16))
            {
            sw = System.Diagnostics.Stopwatch.StartNew();
            System.Console.WriteLine("Balanced",numqubit);
            //var balancedTestCase = new long[] { 1, 2, 3 , 4 };
            balancedTestCase = Enumerable.Range(1, (int)(Pow(2,numqubit-1))+1).ToList().Select(item => (long)item).ToArray();
            if (DeutschJozsaTestCase.Run(sim, numqubit, new QArray<long>(balancedTestCase)).Result)
            {
                throw new Exception("Measured that test case was constant!");
            }

            System.Console.WriteLine(sw.ElapsedMilliseconds);
            }


            System.Console.WriteLine("Constant - 3 All 1s");
            sw = System.Diagnostics.Stopwatch.StartNew();
            var constantTestCase = new long[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            if (!DeutschJozsaTestCase.Run(sim, 3, new QArray<long>(constantTestCase)).Result)
            {
                throw new Exception("Measured that test case {0, 1, 2, 3, 4, 5, 6, 7} was balanced!");
            }
            System.Console.WriteLine("Both constant and balanced functions measured successfully!");
            System.Console.WriteLine(sw.ElapsedMilliseconds);


            System.Console.WriteLine("Constant - 3 All 0s");
            sw = System.Diagnostics.Stopwatch.StartNew();
            constantTestCase = new long[] { };
            if (!DeutschJozsaTestCase.Run(sim, 3, new QArray<long>(constantTestCase)).Result)
            {
                throw new Exception("Measured that test case {} was balanced!");
            }
            System.Console.WriteLine("Both constant and balanced functions measured successfully!");
            System.Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Stop();
            #endregion

            // #region Finding Hidden Shifts of Bent Functions with the Roetteler Algorithm
            // // Finally, we consider the case of finding a hidden shift 𝑠
            // // between two Boolean functions 𝑓(𝑥) and 𝑔(𝑥) = 𝑓(𝑥 ⊕ 𝑠).
            // // This problem can be solved on a quantum computer with one call
            // // to each of 𝑓 and 𝑔 in the special case that both functions are
            // // bent; that is, that they are as far from linear as possible.

            // // Here, we run the test case HiddenShiftBentCorrelationTestCase
            // // defined in the matching Q# source code, and ensure that it
            // // correctly finds each hidden shift for a family of bent
            // // functions defined by the inner product.
            // foreach (var shift in Enumerable.Range(0, 1 << nQubits))
            // {
            //     var measuredShift = HiddenShiftBentCorrelationTestCase.Run(sim, shift, nQubits / 2).Result;
            //     if (measuredShift != shift)
            //     {
            //         throw new Exception($"Measured shift {measuredShift}, but expected {shift}.");
            //     }
            // }
            // System.Console.WriteLine("Measured hidden shifts successfully!");

            // #endregion

            System.Console.WriteLine("\n\nPress Enter to continue...\n\n");
            System.Console.ReadLine();

        }
    }
}
