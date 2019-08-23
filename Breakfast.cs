using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Coffee
    {

    }

    class Egg
    {

    }

    class Toast
    {

    }

    class Bacon
    {

    }

    class Breakfast
    {
        private Coffee PourCoffee()
        {
            return new Coffee();
        }

        private async Task<Egg> FryEggsAsync(int num)
        {
            Thread.Sleep(10000);
            Console.WriteLine("Frying Egg");
            return await Task.FromResult<Egg>(null);
        }

        private async Task<Bacon> FryBaconAsync(int num)
        {
            Console.WriteLine("Frying Bacon");
            return await Task.FromResult<Bacon>(null);
        }

        private async Task<Toast> ToastBreadAsync(int num)
        {
            Thread.Sleep(5000);
            Console.WriteLine("Toasting Bread");
            return await Task.FromResult<Toast>(null);
        }


        public async Task PrepareBreakfast()
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            var eggsTask = Task.Run(() => FryEggsAsync(2));
            var baconTask = Task.Run(() => FryBaconAsync(3));
            var toastTask = Task.Run(() => MakeToastWithButterAndJamAsync(2));

            var allTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (allTasks.Any())
            {
                Task finished = await Task.WhenAny(allTasks);
                if (finished == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finished == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finished == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                allTasks.Remove(finished);
            }
            Console.WriteLine("Breakfast is ready!");

            async Task<Toast> MakeToastWithButterAndJamAsync(int number)
            {
                var toast = await ToastBreadAsync(number);
                ApplyButter(toast);
                ApplyJam(toast);
                return toast;
            }
        }

        private void ApplyButter(Toast toast)
        {
            Console.WriteLine("Applying butter");
        }

        private void ApplyJam(Toast toast)
        {
            Console.WriteLine("Applying Jam");
        }
    }
}
