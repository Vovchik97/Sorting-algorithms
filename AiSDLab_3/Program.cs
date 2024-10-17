//Количество элементов в массиве N = 5000
//Вероятсностный закон распределения Gumbel

using System;
using System.Diagnostics;

class Progam
{
    static void Main()
    {
        int N = 5000; // размер массива
        double A, B;

        Console.WriteLine("Ввод параметров вероятностного закона Gumbel: ");
        Console.WriteLine("Введите параметр A: ");
        A = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите параметр B: ");
        B = Convert.ToDouble(Console.ReadLine());

        double[] array = GenerateRandomArray(N, A, B);

        // копии массива для 3 простых и 3 сложных сортировок
        double[] arr1 = (double[])array.Clone();
        double[] arr2 = (double[])array.Clone();
        double[] arr3 = (double[])array.Clone();
        double[] arr4 = (double[])array.Clone();
        double[] arr5 = (double[])array.Clone();
        double[] arr6 = (double[])array.Clone();

        // таймер
        Stopwatch stopwatch = new Stopwatch();

        // сортировка пузырьком
        stopwatch.Start();
        BubbleSort(arr1);
        stopwatch.Stop();
        TimeSpan bubleSortTime = stopwatch.Elapsed;
        
        // сортировка простым выбором
        stopwatch.Restart();
        SelectionSort(arr2);
        stopwatch.Stop();
        TimeSpan selectionSortTime = stopwatch.Elapsed;
        
        // сортировка вставками
        stopwatch.Restart();
        InsertionSort(arr3);
        stopwatch.Stop();
        TimeSpan insertionSortTime = stopwatch.Elapsed;
        
        // сортировка Хаара
        stopwatch.Restart();
        QuickSort(arr4, 0, arr4.Length - 1);
        stopwatch.Stop();
        TimeSpan quickSortTime = stopwatch.Elapsed;
        
        // сортировка Шелла
        stopwatch.Restart();
        ShellSort(arr5, arr5.Length);
        stopwatch.Stop();
        TimeSpan shellSortTime = stopwatch.Elapsed;
        
        // сортировка кучи
        stopwatch.Restart();
        HeapSort(arr6, arr6.Length);
        stopwatch.Stop();
        TimeSpan heapSortTime = stopwatch.Elapsed;

        Console.WriteLine("\n\"Оценка качества сортировок\"");
        Console.WriteLine("Метод сортировки\t\tВремя сортировки(мс)\tC/N\t\tM/N");
        Console.WriteLine("Сортировка пузырьком\t\t{0}\t\t\t{1}\t{2}", bubleSortTime.Milliseconds.ToString(),
            ((double)BubbleSortComparisons / 5000).ToString("F3"), ((double)BubbleSortSwaps / 5000).ToString("F3"));
        Console.WriteLine("Сортировка выбором\t\t{0}\t\t\t{1}\t{2}", selectionSortTime.Milliseconds.ToString(),
            ((double)SelectionSortComparisons / 5000).ToString("F3"), ((double)SelectionSortSwaps / 5000).ToString("F3"));
        Console.WriteLine("Сортировка вставками\t\t{0}\t\t\t{1}\t{2}", insertionSortTime.Milliseconds.ToString(),
            ((double)InsertionSortComparisons / 5000).ToString("F3"), ((double)InsertionSortSwaps / 5000).ToString("F3"));
        Console.WriteLine("Сортировка Хоара\t\t{0}\t\t\t{1}\t\t{2}", quickSortTime.Milliseconds.ToString(),
            ((double)QuickSortComparisons / 5000).ToString("F3"), ((double)QuickSortSwaps / 5000).ToString("F3"));
        Console.WriteLine("Сортировка Шелла\t\t{0}\t\t\t{1}\t\t{2}", shellSortTime.Milliseconds.ToString(),
            ((double)ShellSortComparisons / 5000).ToString("F3"), ((double)ShellSortSwaps / 5000).ToString("F3"));
        Console.WriteLine("Сортировка кучей\t\t{0}\t\t\t{1}\t\t{2}", heapSortTime.Milliseconds.ToString(),
            ((double)HeapSortComparisons / 5000).ToString("F3"), ((double)HeapSortSwaps / 5000).ToString("F3"));
    }

    //Генерация чисел массива с помощью вероятностного закона Gumbel
    static double[] GenerateRandomArray(int N, double A, double B)
    {
        double[] array = new double[N];
        Random random = new Random();

        for (int i = 0; i < N; i++)
        {
            double u = random.NextDouble();
            double Gumbel = A - B * Math.Log10(-Math.Log10(u));

            array[i] = Gumbel;
        }

        return array;
    }

    static int BubbleSortComparisons = 0;
    static int BubbleSortSwaps = 0;
    static double[] BubbleSort(double[] arr)
    {
        double temp;
        for (int i = 0; i < arr.Length; i++) 
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] > arr[j])
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    BubbleSortSwaps++;
                }
                BubbleSortComparisons++;
            }
        }
        return arr;
    }

    static int SelectionSortComparisons = 0;
    static int SelectionSortSwaps = 0;
    static double[] SelectionSort(double[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int min = i;
            double temp;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] < arr[i])
                {
                    min = j;
                    temp = arr[min];
                    arr[min] = arr[i];
                    arr[i] = temp;
                    SelectionSortSwaps++;
                }
                SelectionSortComparisons++;
            }
        }
        return arr;
    }

    static int InsertionSortComparisons = 0;
    static int InsertionSortSwaps = 0;
    static double[] InsertionSort(double[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            double temp = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > temp)
            {
                arr[j + 1] = arr[j];
                arr[j] = temp;
                j--;
                InsertionSortComparisons++;
                InsertionSortSwaps++;
            }
        }
        return arr;
    }

    static int QuickSortComparisons = 0;
    static int QuickSortSwaps = 0;
    static double[] QuickSort(double[] arr, int leftIndex, int rightIndex)
    {
        int i = leftIndex;
        int j = rightIndex;
        double pivot = arr[leftIndex];

        while (i <= j)
        {
            while (arr[i] < pivot)
            {
                i++;
                QuickSortComparisons++;
            }

            while (arr[j] > pivot)
            {
                j--;
                QuickSortComparisons++;
            }

            if (i <= j)
            {
                double temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;
                j--;
                QuickSortSwaps++;
            }
        }

        if (leftIndex < j)
        {
            QuickSort(arr, leftIndex, j);
        }
        if (i < rightIndex)
        {
            QuickSort(arr, i, rightIndex);
        }

        return arr;
    }

    static int ShellSortComparisons = 0;
    static int ShellSortSwaps = 0;
    static double[] ShellSort(double[] arr, int size)
    {
        for (int interval = size / 2; interval > 0; interval /= 2)
        {
            for (int i = interval; i < size; i++)
            {
                int k = i;
                double currentKey = arr[i];

                while (k >= interval && arr[k - interval] > currentKey)
                {
                    arr[k] = arr[k - interval];
                    k -= interval;
                    ShellSortSwaps++;
                    ShellSortComparisons++;
                }
                arr[k] = currentKey;
            }
        }

        return arr;
    }

    static int HeapSortComparisons = 0;
    static int HeapSortSwaps = 0;
    static double[] HeapSort(double[] arr, int size)
    {
        if (size <= 1)
        {
            return arr;
        }

        for (int i = size / 2 - 1; i >= 0; i--)
        {
            Heapify(arr, size, i);
        }

        for (int i = size - 1; i >= 0; i--)
        {
            double temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            HeapSortSwaps++;

            Heapify(arr, i, 0);
        }

        return arr;
    }

    static void Heapify(double[] arr, int size, int index)
    {
        int largestIndex = index;
        int leftChild = 2 * index + 1;
        int rightChild = 2 * index + 2;

        if (leftChild < size && arr[leftChild] > arr[largestIndex])
        {
            largestIndex = leftChild;
            HeapSortComparisons++;
        }

        if (rightChild < size && arr[rightChild] > arr[largestIndex])
        {
            largestIndex = rightChild;
            HeapSortComparisons++;
        }

        if (largestIndex != index)
        {
            double temp = arr[index];
            arr[index] = arr[largestIndex];
            arr[largestIndex] = temp;
            HeapSortSwaps++;

            Heapify(arr, size, largestIndex);
        }
    }

}
