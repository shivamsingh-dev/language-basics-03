using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 },
                new[] { 2, 8 },
                new[] { 5, 2 },
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 },
                new[] { 2, 8, 5, 1 },
                new[] { 5, 2, 4, 4 },
                new[] { "tFc", "tF", "Ftc" },
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }


        public static int calculate_individual_diet(int[] protein, int[] carbs, int[] fat, string individual_diet)
        {
            int[] calorie = new int[fat.Length];
            for (int i = 0; i < fat.Length; i++)
            {
                calorie[i] = fat[i] * 9 + (carbs[i] + protein[i]) * 5;
            }
            List<int> index = new List<int>();
            for (int i = 0; i < fat.Length; i++)
            {
                index.Add(i);
            }

            foreach (char ch in individual_diet)
            {
                if (ch >= 'a' && ch <= 'z')
                {

                    if (ch == 'p') index = Calculate_min(index, protein);
                    if (ch == 'c') index = Calculate_min(index, carbs);
                    if (ch == 'f') index = Calculate_min(index, fat);
                    if (ch == 't') index = Calculate_min(index, calorie);

                }
                else
                {
                    if (ch == 'P') index = Calculate_max(index, protein);
                    if (ch == 'C') index = Calculate_max(index, carbs);
                    if (ch == 'F') index = Calculate_max(index, fat);
                    if (ch == 'T') index = Calculate_max(index, calorie);
                }

                if (index.Count == 1) return index[0];
            }

            return index[0];
        }

        public static List<int> Calculate_max(List<int> _index, int[] array)
        {
            var max_meal = array[_index[0]];
            for (int i = 1; i < _index.Count; i++)
                max_meal = Math.Max(max_meal, array[_index[i]]);

            List<int> remove_smaller = new List<int>();
            foreach (var i in _index)
            {
                if (array[i] != max_meal)
                    remove_smaller.Add(i);
            }

            foreach (var i in remove_smaller)
                _index.Remove(i);

            return _index;
        }

        public static List<int> Calculate_min(List<int> _index, int[] array)
        {
            var min_meal = array[_index[0]];
            for (int i = 1; i < _index.Count; i++)
                min_meal = Math.Min(min_meal, array[_index[i]]);

            List<int> remove_smaller = new List<int>();
            foreach (var i in _index)
            {
                if (array[i] != min_meal)
                    remove_smaller.Add(i);
            }

            foreach (var i in remove_smaller)
                _index.Remove(i);

            return _index;
        }
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int length = carbs.Length;
            int[] ans = new int[dietPlans.Length];
            for (int i = 0; i < dietPlans.Length; i++)
            {
                var individual_diet = dietPlans[i];
                ans[i] = calculate_individual_diet(protein, carbs, fat, individual_diet);
            }
            return ans;
        }
    }
}
