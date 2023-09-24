namespace AdvancedAlgorithms
{
    public class SearchAlgorithms
    {
        // Linear Search
        public static int LinearSearch(int[] arr, int target)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target)
                {
                    return i;
                }
            }
            return -1;
        }

        // Binary Search
        public static int BinarySearch(int[] arr, int target)
        {
            int left = 0, right = arr.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] == target)
                {
                    return mid;
                }
                if (arr[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return -1;
        }

        // Jump Search
        public static int JumpSearch(int[] arr, int target)
        {
            int n = arr.Length;
            int step = (int)Math.Sqrt(n);

            int prev = 0;
            while (arr[Math.Min(step, n) - 1] < target)
            {
                prev = step;
                step += (int)Math.Sqrt(n);
                if (prev >= n)
                {
                    return -1;
                }
            }

            for (int i = prev; i < Math.Min(step, n); i++)
            {
                if (arr[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }

        // Interpolation Search
        public static int InterpolationSearch(int[] arr, int target)
        {
            int left = 0, right = arr.Length - 1;

            while (left <= right && target >= arr[left] && target <= arr[right])
            {
                if (left == right)
                {
                    if (arr[left] == target)
                    {
                        return left;
                    }
                    return -1;
                }

                int pos = left + (((right - left) / (arr[right] - arr[left])) * (target - arr[left]));

                if (arr[pos] == target)
                {
                    return pos;
                }

                if (arr[pos] < target)
                {
                    left = pos + 1;
                }
                else
                {
                    right = pos - 1;
                }
            }
            return -1;
        }

        // Exponential Search
        public static int ExponentialSearch(int[] arr, int target)
        {
            if (arr[0] == target)
            {
                return 0;
            }

            int i = 1;
            while (i < arr.Length && arr[i] <= target)
            {
                i = i * 2;
            }

            return BinarySearch(arr, target, i / 2, Math.Min(i, arr.Length - 1));
        }

        private static int BinarySearch(int[] arr, int target, int left, int right)
        {
            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (arr[mid] == target)
                {
                    return mid;
                }

                if (arr[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }
    }
}
