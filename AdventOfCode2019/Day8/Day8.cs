using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2019.Day8 {



    public class BiosCracker {
       
        public static int WIDTH = 25;
        public static int HEIGHT = 6;


        public static void Day7() {
            var filePath = CurrentDirectory.ToString() + "/Day8/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);


            List<int> instructions = lines.First().Select(char.ToString).Select(int.Parse).ToList();

            var layers = GetLayers(instructions);
            Console.WriteLine("Day 8: Problem 1: " + 0);

            
            Console.WriteLine("Day 8: Problem 2: " + 0);
        }

        public static Dictionary<(int, int), List<int>> GetLayers(
            List<int> instructions,
            int yStart = 0,
            int xStart = 0,
            int width = 25,
            int height = 6
        ) {
            var layers = new Dictionary<(int, int), List<int>>();

            foreach(var instruction in instructions) {
                List<int> pixel;
                if (layers.TryGetValue((xStart, yStart), out pixel)) {
                    pixel.Add(instruction);
                   
                } else {
                    layers.Add((xStart, yStart), new List<int>() { instruction });
                }

                xStart++;
                if (xStart >= width) {
                    yStart++;
                    xStart = 0;
                }

                if(yStart >= height) {
                    yStart = 0;
                }
                
            }

            return layers;

        }
    }
}